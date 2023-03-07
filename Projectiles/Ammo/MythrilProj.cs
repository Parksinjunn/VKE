using System;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.Ammo
{
    public class MythrilProj : AmmoProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 2;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            ArmorPiercingMult = 1.25f;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if(Main.rand.NextBool(20))
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<MythrilProjLaser>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
                SoundEngine.PlaySound(SoundID.Item12 with { Volume = 0.15f }, Projectile.position);
            }
            base.OnHitNPC(n, damage, knockback, crit);
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 6; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(7, 7);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Mythril, Vel.X, Vel.Y, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
    }
}