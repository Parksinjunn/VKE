using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SteelSeries.GameSense;
using System;
using System.Net.Security;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;
using Terraria.Graphics.Shaders;

namespace VKE.Projectiles.PreProj
{
    public class WeakVampireProj : KnifeProjectile, IPixelPrimitiveDrawer
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
            float baseWidth = Projectile.scale * Projectile.width * 1.3f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            return Color.Lerp(Color.DarkRed, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SafeSetDefaults()
        {
            KnifeMomentum = 100f;
            CanHaveGravity = true;
            Projectile.Name = "Weak Vampire Proj";
            Projectile.width = 14;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                    
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                    
            Projectile.tileCollide = true;                
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;

        }
        //public void DoTrailCreation(TrailManager tManager)
        //{
        //    tManager.CreateTrail(Projectile, new GradientTrail(new Color(95, 220, 214), new Color(34, 78, 146)), new RoundCap(), new DefaultTrailPosition(), 100f, 260f, new ImageShader(ModContent.Request<Texture2D>("vke/Effects/Trails/Trail_1", AssetRequestMode.ImmediateLoad).Value, 0.03f, 1f, 1f));
        //}
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(2, 2);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.BloodWater, Vel.X, Vel.Y, newColor: Color.Red, Scale: 1.5f);
                Main.dust[dust1].noGravity = false;
            }
            base.Kill(timeLeft);
        }
        public override void SafeAI()
        {

            //this is projectile dust
            //int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 244, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.DarkRed, 1.8f);
            //Main.dust[DustID2].noGravity = true;
            //this make that the projectile faces the right way 
            //Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        //public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 0);
    }
}