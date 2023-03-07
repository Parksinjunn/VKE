using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;

namespace VKE.Projectiles.HardProj
{
    public class BloomingTerrorMandible : KnifeProjectile, IPixelPrimitiveDrawer
    {
        public Vector2 TargetPos;
        public int Mode;
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 1;
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
        }
        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 1.2f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            return Color.Lerp(Color.HotPink, Color.ForestGreen, completionRatio) * 0.9f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        float MandibleTopRotation;
        float MandibleBottomRotation;
        public override void SafeSetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 70;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
        }
        bool DefinedRotation;
        short[] BTDusts = new short[] { DustID.PlanteraBulb, DustID.Plantera_Green, DustID.Plantera_Pink, DustID.CursedTorch };
        bool AtTarget;
        bool CloseToTarget;
        public override void SafeAI()
        {
            if (Mode == 1)
                Projectile.velocity *= 0.9f;
            if (Mode == 2 && AtTarget)
                Projectile.velocity *= 0.85f;
            else if (Mode == 2)
            {
                float SpeedMult = 4f; 
                float shootToX = TargetPos.X - Projectile.Center.X;
                float shootToY = TargetPos.Y - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                if (distance < 100f)
                {
                    CloseToTarget = true;
                }
                if (distance < 50f)
                {
                    AtTarget = true;
                    Projectile.timeLeft = 25;
                }
                else if (distance < 2000f && distance > 50f && !AtTarget)
                {
                    distance = SpeedMult / distance;

                    shootToX *= distance * 3;
                    shootToY *= distance * 3;

                    Projectile.velocity.X = shootToX;
                    Projectile.velocity.Y = shootToY;
                }

            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            for (int i = 0; i < 4; i++)
            {
                int DustTrail = Dust.NewDust(Projectile.oldPos[8], Projectile.width - 5, Projectile.height - 5, BTDusts[(i%4)], Scale: Main.rand.NextFloat(0.6f, 1.2f));
                Dust DustObj = Main.dust[DustTrail];
                DustObj.velocity = -(Projectile.velocity);
                Main.dust[DustTrail].noGravity = true;
            }
            if (!DefinedRotation)
            {
                if(Mode == 1)
                {
                    Projectile.timeLeft = 49;
                    MandibleTopRotation = Projectile.rotation + 1.1f;
                    MandibleBottomRotation = Projectile.rotation - 1.1f;
                }    
                DefinedRotation = true;
                if(Mode == 2)
                {
                    MandibleTopRotation = Projectile.rotation + 1.3f;
                    MandibleBottomRotation = Projectile.rotation - 1.3f;
                }
                Player player = Main.player[Projectile.owner];
                if (player.GetModPlayer<VampPlayer>().ExtraProj > 0)
                {
                    Projectile.damage *= 1 + (player.GetModPlayer<VampPlayer>().ExtraProj / 10);
                }
            }
        }
        int HitCount;
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            HitCount++;
            if (HitCount <= 3)
            {
                if (!n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjChanceScale * 1.5f)) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjBossChanceScale * 1.5f)) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
            }
            if (Projectile.timeLeft > 30)
            {
                Projectile.idStaticNPCHitCooldown = 6;
            }
            else if (Projectile.timeLeft > 11)
            {
                Projectile.idStaticNPCHitCooldown = 3;
            }
            else
            {
                Projectile.idStaticNPCHitCooldown = 1;
            }
            Hoods(n);
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(10, 10);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, BTDusts[(j%4)], Vel.X, Vel.Y, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        int EffectTimer;
        public override bool PreDraw(ref Color lightColor)
        {
            EffectTimer++;
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor = new Color(170, 170, 170);
            Color glowColor = new Color(255, 255, 255);
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/BloomingTerrorMandible_Glow").Value, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), glowColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);

            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Color color = Projectile.GetAlpha(drawColor);
            if(CloseToTarget)
            {
                MandibleTopRotation -= 0.035f;
                MandibleBottomRotation += 0.035f;
            }
            if (Mode == 1)
            {
                MandibleTopRotation -= 0.02f;
                MandibleBottomRotation += 0.02f;
            }

            float spread = MathHelper.ToRadians(45);
            float baseSpeed = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2f;
            double deltaAngle = spread / 2f;
            double offsetAngleTop;
            double offsetAngleBottom;
            offsetAngleTop = startAngle + deltaAngle * 4;
            offsetAngleBottom = startAngle + deltaAngle * -2;
            Vector2 drawPosTop = (new Vector2(baseSpeed * (float)Math.Sin(offsetAngleTop), baseSpeed * (float)Math.Cos(offsetAngleTop)) * -5) + (Projectile.oldPos[0] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY));
            Vector2 drawPosBottom = (new Vector2(baseSpeed * (float)Math.Sin(offsetAngleBottom), baseSpeed * (float)Math.Cos(offsetAngleBottom)) * -5) + (Projectile.oldPos[0] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY));

            Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/BloomingTerrorMandibleTop").Value, drawPosTop, null, color, MandibleTopRotation, drawOrigin, 1f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/BloomingTerrorMandibleTop_Glow").Value, drawPosTop, null, glowColor, MandibleTopRotation, drawOrigin, 1f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/BloomingTerrorMandibleBottom").Value, drawPosBottom, null, color, MandibleBottomRotation, drawOrigin, 1f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/BloomingTerrorMandibleBottom_Glow").Value, drawPosBottom, null, glowColor, MandibleBottomRotation, drawOrigin, 1f, SpriteEffects.None, 0);
            return false;
        }
    }
}