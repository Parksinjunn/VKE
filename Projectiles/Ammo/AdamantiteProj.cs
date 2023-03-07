using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.Ammo
{
    public class AdamantiteProj : AmmoProjectile, IPixelPrimitiveDrawer
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
            Color ColorFade = Color.Lerp(Color.Red, Color.DarkRed, Color1Fade);
            return Color.Lerp(ColorFade, Color.White, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:LightningTrail"]);
            GameShaders.Misc["VampKnives:LightningTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SafeSetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            ArmorPiercingMult = 1.4f;
            SwitchSpeed = Main.rand.NextFloat(0.01f, 0.05f);
        }
        int Delay;
        public Vector2 TargetPos;
        Vector2 ProjectileInitPos;
        bool GotFirstTarget;
        bool GenNext;
        int NumGens = 1;
        Vector2 NextTarget;
        bool PastTarget;
        float distance;
        public override void SafeAI()
        {
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
            if (!GotFirstTarget)
            {
                NextTarget = TargetPos;
                ProjectileInitPos = Projectile.position;
                GotFirstTarget = true;
            }
            if(GenNext)
            {
                NextTarget = NextTarget + ((TargetPos - ProjectileInitPos) * NumGens);
                NumGens++;
                Delay = 0;
                GenNext = false;
            }
            if (Delay < 7)
            {
                Delay++;
            }
            else
            {
                float shootToX = NextTarget.X - Projectile.position.X;
                float shootToY = NextTarget.Y - Projectile.position.Y;

                distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                if (distance >= 49 && !PastTarget)
                {
                    Vector2 velo = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(NextTarget) * 24f, 0.10f);
                    Projectile.velocity = velo;
                }
                else if (distance < 49 && !PastTarget)
                {
                    GenNext = true;
                }
                Projectile.netUpdate = true;
            }
        }
    }
}