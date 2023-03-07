using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.HardProj
{
    public class TerraKnivesProj : KnifeProjectile, IPixelPrimitiveDrawer
    {

        public PrimDrawer TrailDrawer { get; private set; } = null;
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
            float baseWidth = Projectile.scale * Projectile.width * 6f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            Color Rainbow = Color.ForestGreen;
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.TerraTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        bool HitTile;
        float VelocityFactor = 2f;
        int EffectTimer;

        public override void SafeSetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.scale = 0.7f;
        }

        public override void SafeAI()
        {            
            Lighting.AddLight(Projectile.Center, 0, (Main.DiscoG / 255f), 0);
            if (!ZenithActive)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                if (HitTile)
                {
                    VampPlayer.OvalDust(Projectile.Center, 1, 1, new Color(0,255,0), DustID.MagicMirror, 1.5f);
                    //for (int iteration = 0; iteration < 360; iteration++)
                    //{
                    //    int DustID3 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 15, 0f, 0f, 10, new Color(0, 255, 0), 2f);
                    //    Main.dust[DustID3].noGravity = true;
                    //    Main.dust[DustID3].velocity = new Vector2(VelocityFactor * (float)Math.Cos(iteration / VelocityFactor), VelocityFactor * (float)Math.Sin(iteration / VelocityFactor));
                    //}
                    HitTile = false;
                    EffectTimer++;
                }
            }
        }
        public override bool SafePreKill(int timeLeft)
        {
            if (Projectile.velocity.X == 0 && Projectile.velocity.Y == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Main.rand.NextFloat(-20f, 20f), Main.rand.NextFloat(-20f, 20f), 132, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            }
            else
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 132, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            return base.SafePreKill(timeLeft); ;
        }
        public override void Kill(int timeLeft)
        {
            VampPlayer.OvalDust(Projectile.Center, 2, 2, new Color(0, 255, 0), DustID.MagicMirror, 1.5f);
            base.Kill(timeLeft);
        }
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            HitTile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.velocity = oldVelocity;
            Projectile.timeLeft = 30;
            return false;
        }
    }
}