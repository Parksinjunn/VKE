using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.PreProj
{
    public class EnchantedKnivesProj : KnifeProjectile, IPixelPrimitiveDrawer
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
            float baseWidth = Projectile.scale * Projectile.width * 0.3f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            return Color.Lerp(Color.LightBlue, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, (Projectile.Size * 0.5f - Main.screenPosition), 25);
        }
        public override void SafeSetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.scale = 1f;
            CanHaveGravity = true;
            KnifeMomentum = 7f;
        }
        int SpriteRotation = 45;
        public override void SafeAI()
        {
            if (!ZenithActive)
            {
                //this make that the projectile faces the right way 

                if (Main.rand.Next(0, 5) > 2)
                {
                    int DustChance = Main.rand.Next(3);
                    int DustID2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustChance switch
                    {
                        0 => 15,
                        1 => 57,
                        _ => 58,
                    }, 0f, 0f, 100, default(Color), 1f);

                }
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
            }
            Lighting.AddLight(Projectile.Center, 0.3f, 0.4f, 0.5f);
        }
        public override bool SafePreKill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(6, 6);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, Main.rand.Next(3) switch
                {
                    0 => 15,
                    1 => 57,
                    _ => 58,
                }, Vel.X, Vel.Y, newColor: default(Color), Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            return base.SafePreKill(timeLeft); ;
        }
        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(0, 5) == 3)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 173, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            }
            base.SafeOnHitNPC(n, damage, knockback, crit);
        }
    }
}