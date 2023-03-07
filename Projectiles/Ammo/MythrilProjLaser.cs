using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.Ammo
{
	public class MythrilProjLaser : KnifeProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mythril Laser");
		}
		public override void SetDefaults()
		{
			Projectile.width = 3;
			Projectile.height = 15;
            Projectile.tileCollide = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.friendly = true;
			Projectile.penetrate = 3;
		}
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Hoods(n);
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 6; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(5, 5);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Firework_Green, Vel.X, Vel.Y, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        public override void AI()
		{
			Lighting.AddLight(Projectile.Center, 0.28f, 1.00f, 0.87f);
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
		}
        public override Color? GetAlpha(Color lightColor)
        {
			return new Color(0.28f, 1f, 0.87f);
        }
    }
}