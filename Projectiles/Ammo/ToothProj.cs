using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.Ammo
{
    public class ToothProj : KnifeProjectile
    {
        public int ran;
        public int ranD;
        public override void SafeSetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Main.projFrames[Projectile.type] = 3;           //this is projectile frames
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.scale = 0.55f;
            Projectile.timeLeft = 200;
        }
        public override void SafeAI()
        {
            if(ranD ==0)
            {
                ran = Main.rand.Next(0, 3);
                ranD = 1;
            }
            //this is projectile dust
            //this make that the projectile faces the right way 
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;

            if (ran == 0)
                Projectile.frame = 0;
            if (ran == 1)
                Projectile.frame = 1;
            if (ran == 2)
                Projectile.frame = 2;
        }

        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Hoods(n);
        }
        public override bool PreKill(int timeLeft)
        {
            ranD = 0;
            return base.PreKill(timeLeft);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig.WithVolumeScale(0.5f), Projectile.position);
            return true;
        }
    }
}