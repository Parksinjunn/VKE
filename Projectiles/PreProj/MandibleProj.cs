using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PreProj
{
    public class MandibleProj : KnifeProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
        }

        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if(Main.rand.Next(1,5) == 3)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y + Main.rand.Next(1000, 1300), 0, -25, Mod.Find<ModProjectile>("MandibleSummonProj").Type, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            }
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 3) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 3) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            return true;
        }
    }
}