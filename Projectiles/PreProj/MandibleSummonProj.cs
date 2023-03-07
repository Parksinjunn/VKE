using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PreProj
{
    public class MandibleSummonProj : KnifeProjectile
    {
        public int tileCollide;
        public override void SafeSetDefaults()
        {
            Projectile.Name = "buggzy";
            Projectile.width = 40;
            Projectile.height = 62;
            Projectile.scale = 0.5f;
            Main.projFrames[Projectile.type] = 6;           //this is projectile frames
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            //aiType = ProjectileID.DeadlySphere;
            //projectile.aiStyle = 66;
        }
        public bool hit = false;
        float velocityXStored;
        float velocityYStored;
        float distance;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (hit == false)
            {
                int ran1 = Main.rand.Next(50,201);
                int ran2 = ran1 - 49;
                for (int i = ran2; i < ran1; i++)
                {

                    Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                    NPC target = Main.npc[i];
                    //If the npc is hostile
                    if (target.friendly == false)
                    {
                        //Get the shoot trajectory from the projectile and target
                        float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                        float shootToY = target.position.Y - Projectile.Center.Y;
                        distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                        //If the distance between the live targeted npc and the projectile is less than 480 pixels
                        if (distance < 500f && !target.friendly && target.active && distance >= target.width / 2)
                        {
                            if (distance < target.width / 2)
                                goto hitNPC;
                            //Divide the factor, 3f, which is the desired velocity
                            distance = 3f / distance;

                            //Multiply the distance by a multiplier if you wish the projectile to have go faster
                            shootToX *= distance * 5;
                            shootToY *= distance * 5;

                            //Set the velocities to the shoot values
                            Projectile.velocity.X = shootToX;
                            Projectile.velocity.Y = shootToY;
                            velocityXStored = Projectile.velocity.X;
                            velocityYStored = Projectile.velocity.Y;
                        }
                    }
                }
            }
            hitNPC:
            if (hit == true)
            {
                Projectile.velocity.X = velocityXStored;
                Projectile.velocity.Y = Math.Abs(velocityYStored) * -1;
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            }
            if (Projectile.velocity.Y == 0)
                Projectile.velocity.Y = -25;
        }
        //public int healamnt;
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            hit = true;
            Projectile.velocity.X = velocityXStored;
            Projectile.velocity.Y = Math.Abs(velocityYStored) * -1;
        }
        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 20) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 5) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            return true;
        }
    }
}