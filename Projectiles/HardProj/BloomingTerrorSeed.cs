using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.HardProj
{
    public class BloomingTerrorSeed : KnifeProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.Name = "Terror Seed";
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 170;
            AIType = ProjectileID.ThornBall;
            Projectile.aiStyle = 14;
            Projectile.scale = Main.rand.NextFloat(0.5f, 1.1f);
        }
        short[] BTDusts = new short[] { DustID.PlanteraBulb, DustID.Plantera_Green, DustID.Plantera_Pink, DustID.CursedTorch };
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, BTDusts[(j % 4)], Vel.X, Vel.Y, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {

        }
    }
}