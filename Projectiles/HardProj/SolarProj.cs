using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;
using VKE.NPCs;
using Terraria.Graphics.Shaders;

namespace VKE.Projectiles.HardProj
{
    public class SolarProj : KnifeProjectile, IPixelPrimitiveDrawer
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
            return Color.Lerp(Color.Orange, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.CorkscrewTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        int MaxPen = 3;
        int PenCount;
        public override void SafeSetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 3;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(3, 3);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.SolarFlare, Vel.X, Vel.Y, newColor: Color.Orange, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        public override void AI()
        {
            //this is projectile dust
            //int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width - 3, projectile.height - 3, 244, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 10, Color.LightGreen, 1.8f);
            //Main.dust[DustID2].noGravity = false;
            //this make that the projectile faces the right way 
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
        }

        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), n.Center,new Vector2(0,0), 612, (int)(Projectile.damage * 0.50), 8, owner.whoAmI);
        }
    }
}