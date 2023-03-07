using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.PreProj
{
    public class HarpyProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 1;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 0.7f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        float SwitchSpeed;
        float Color1Fade;
        bool Color1Switch;
        public Color ColorFunction(float completionRatio)
        {
            Color ColorFade = Color.Lerp(Color.DeepSkyBlue, Color.LightSkyBlue, Color1Fade);
            return Color.Lerp(ColorFade, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:LightningTrail"]);
            GameShaders.Misc["VampKnives:LightningTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SafeSetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 110;
            SwitchSpeed = Main.rand.NextFloat(0.01f, 0.05f);
        }
        public override void AI()
        {
            //if (Main.rand.NextBool(7))
            //{
            //    Dust dust;
            //    Vector2 position = Main.LocalPlayer.Center;
            //    dust = Main.dust[Terraria.Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, DustID.ManaRegeneration, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 0, Color.SkyBlue, 0.8f)];
            //    dust.noGravity = false;
            //}
            if (!Color1Switch)
            {
                Color1Fade += SwitchSpeed;
                if (Color1Fade >= 1f)
                    Color1Switch = true;
            }
            else if (Color1Switch)
            {
                Color1Fade -= SwitchSpeed;
                if (Color1Fade <= 0f)
                    Color1Switch = false;
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(5, 5);
                int dust1 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.ManaRegeneration, Vel.X, Vel.Y, 10, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
    }
}