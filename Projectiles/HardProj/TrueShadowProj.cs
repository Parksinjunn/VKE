using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.HardProj
{
    public class TrueShadowProj : KnifeProjectile, IPixelPrimitiveDrawer
    {

        public PrimDrawer TrailDrawer { get; private set; } = null;
        int TrailLength = 5;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 1;
            ProjectileID.Sets.TrailCacheLength[Type] = 12;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 0.5f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            Color Rainbow = Color.LimeGreen;
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:LightningTrail"]);
            GameShaders.Misc["VampKnives:LightningTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SafeSetDefaults()
        {
            Projectile.Name = "True Shadow Knives";
            Projectile.width = 36;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.penetrate = 3;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 210;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            ReturnTime = Main.rand.Next(147, 155);
        }
        int CollisionTimer;
        int CollisionTimerMax = 28;
        bool CollisionTimerStart;
        int ReturnTime;
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!CollisionTimerStart)
            {
                CollisionTimerStart = true;
                Projectile.tileCollide = false;
                Projectile.velocity = oldVelocity;
                return false;
            }
            else
            {
                return base.OnTileCollide(oldVelocity);
            }
        }
        bool NoDust;
        public override void AI()
        {
            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 61, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.Green, 1f);
            Main.dust[DustID2].noGravity = true;

            SpriteRotFix = Projectile.direction == 1 ? 0.784f : -0.784f;
            if (Projectile.timeLeft > ReturnTime + 10)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (1.57f - SpriteRotFix);
            }
            else if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= (20 * (1f / (float)Projectile.timeLeft));
            }
            else if(Projectile.spriteDirection == 1)
            {
                Projectile.rotation += (20 * (1f / (float)Projectile.timeLeft));
            }

            if (CollisionTimerStart && CollisionTimer < CollisionTimerMax)
            {
                CollisionTimer++;
            }
            else if (CollisionTimer >= CollisionTimerMax)
            {
                Projectile.timeLeft = ReturnTime;
                CollisionTimer = 0;
                CollisionTimerStart = false;
            }

            if (Projectile.timeLeft < ReturnTime)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center) * 16f, 0.03f);
                if (Main.myPlayer == Projectile.owner)
                {
                    Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                    Rectangle value12 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
                    if (rectangle.Intersects(value12))
                    {
                        NoDust = true;
                        Projectile.Kill();
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            if(!NoDust)
            {
                for (int j = 0; j < 5; j++)
                {
                    Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                    int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.GreenTorch, Vel.X, Vel.Y, newColor: Color.Green, Scale: 1.5f);
                    Main.dust[dust1].noGravity = true;
                }
            }
            base.Kill(timeLeft);
        }
        bool HealSpawned;
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if (HealSpawned == false)
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
                HealSpawned = true;
            }
            Hoods(n);
        }
        int EffectTimer;
        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            EffectTimer++;
            Projectile.spriteDirection = Projectile.direction;
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor;
            Color color;
            Color color2;
            Color color3;
            Color color4;
            if (Main.dayTime)
            {
                drawColor = new Color(53, 0, 140);
                color = drawColor;
                color2 = drawColor;
                color3 = drawColor;
                color4 = drawColor;
            }
            else
            {
                drawColor = new Color(57, 255, 20);
                color = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - 1)) / (float)Projectile.oldPos.Length);
                color2 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 1)))) / (float)Projectile.oldPos.Length);
                color3 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 2)))) / (float)Projectile.oldPos.Length);
                color4 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 3)))) / (float)Projectile.oldPos.Length);
            }

            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);

            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawOrigin2 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawOrigin3 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawOrigin4 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);

            Vector2 drawPos = Projectile.oldPos[(TrailLength - 1)] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Vector2 drawPos2 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength - 1) / 4) * 1)))] - Main.screenPosition + drawOrigin2 + new Vector2(0f, Projectile.gfxOffY);
            Vector2 drawPos3 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength - 1) / 4) * 2)))] - Main.screenPosition + drawOrigin3 + new Vector2(0f, Projectile.gfxOffY);
            Vector2 drawPos4 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength - 1) / 4) * 3)))] - Main.screenPosition + drawOrigin4 + new Vector2(0f, Projectile.gfxOffY);

            if (EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 3))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/TrueShadowProj").Value, drawPos, null, color * 0.1f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            if (EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 2))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/TrueShadowProj").Value, drawPos2, null, color2 * 0.2f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            if (EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 1))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/TrueShadowProj").Value, drawPos3, null, color3 * 0.4f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            if (EffectTimer > (TrailLength - 1))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/TrueShadowProj").Value, drawPos4, null, color4 * 0.6f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}