using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;
using Terraria.GameContent;

namespace VKE.Projectiles.HardProj
{
    public class TrueExcaliburProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        int PrimColor;
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Excalibur Knife");
            ProjectileID.Sets.TrailingMode[Type] = 1;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 1.3f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        Color Rainbow;
        public Color ColorFunction(float completionRatio)
        {
            if(PrimColor == 0)
            {
                Rainbow = Color.Violet;
            }
            else if (PrimColor == 1)
            {
                Rainbow = Color.RoyalBlue;
            }
            else if (PrimColor == 2)
            {
                Rainbow = Color.SkyBlue;
            }
            else
            {
                Rainbow = Color.BlueViolet;
            }
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:LightningTrail"]);
            GameShaders.Misc["VampKnives:LightningTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.RainbowTorch, Vel.X, Vel.Y, newColor: Color.Transparent, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].color = Rainbow;
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
            Color drawColor = new Color(255, 255, 255);
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);

            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos = Projectile.oldPos[0] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(drawColor) * 0.33f;

            Color color2 = Projectile.GetAlpha(drawColor) * 0.16f;

            if (EffectTimer >= 1 && EffectTimer < 5)
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/TrueExcaliburProj").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.3f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= 5 && EffectTimer < 10)
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/TrueExcaliburProj").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.3f, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/TrueExcaliburProj").Value, drawPos, null, color2, Projectile.rotation, drawOrigin, 1.5f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= 10 && EffectTimer < 15)
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/TrueExcaliburProj").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.3f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= 15)
            {
                EffectTimer = -5;
            }
            return false;
        }
        public override void SafeSetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            PrimColor = Main.rand.Next(0, 5);
        }

        public override void SafeAI()
        {
            if (!ZenithActive)
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Lighting.AddLight(Projectile.Center, (Main.DiscoR / 255f), (Main.DiscoG / 255f), (Main.DiscoB / 255f));
            CollisionTimer++;
            if (CollisionTimerStart && CollisionTimer < CollisionTimerMax)
            {
                CollisionTimer++;
            }
            else if (CollisionTimer >= CollisionTimerMax)
            {
                Projectile.tileCollide = true;
            }
        }
        int CollisionTimer;
        int CollisionTimerMax = 28;
        bool CollisionTimerStart;
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
        public override bool SafePreKill(int timeLeft)
        {
            if (Projectile.velocity.X == 0 && Projectile.velocity.Y == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Main.rand.NextFloat(-20f, 20f), Main.rand.NextFloat(-20f, 20f), 156, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            }
            else
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, 156, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            return base.SafePreKill(timeLeft); ;
        }
    }
}