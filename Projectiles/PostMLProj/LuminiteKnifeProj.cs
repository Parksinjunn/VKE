using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PostMLProj
{
    public class LuminiteKnifeProj : KnifeProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.Name = "Luminite Knife";
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
        }
        int DustID2;
        public override void AI()
        {
            //this is projectile dust
            DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 45, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.LightBlue, 1.8f);
            Main.dust[DustID2].noGravity = true;
            //this make that the projectile faces the right way 
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            //projectile.light = .04f;
            //Dust
            Projectile.alpha = (int)Projectile.localAI[0] * 2;

        }

        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            n.AddBuff(169, 300); //Bleeding! debuff for 5 seconds
            n.AddBuff(164, 60); //Distorted for 1 second
        }
        //int hit = 0;
        //public override bool OnTileCollide(Vector2 velocityChange)
        //{
        //    hit++;
        //    if (hit >= 1)
        //    {
        //        Main.dust[DustID2].alpha = 0;
        //        projectile.Kill();
        //    }
        //    return true;
        //}

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