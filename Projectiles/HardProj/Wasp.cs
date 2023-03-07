using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.HardProj
{
	public class Wasp : KnifeProjectile
	{
		public override void SafeSetDefaults()
		{
			Projectile.Name = "Wasp";
            Projectile.friendly = true;
            Projectile.penetrate = -1;                      
            Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
            Projectile.timeLeft = 60;
            //projectile.CloneDefaults(ProjectileID.Bee);
            //projectile.aiStyle = ProjectileID.Bee;
            Projectile.width = 34;
            Projectile.height = 34;
            Main.projFrames[Projectile.type] = 6;       
        }

        public override void SafeAI()
        {
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                //If the npc is hostile
                if (!target.friendly)
                {
                    //Get the shoot trajectory from the projectile and target
                    float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                    float shootToY = target.position.Y - Projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                    //If the distance between the live targeted npc and the projectile is less than 480 pixels
                    if (distance < 800f && !target.friendly && target.active)
                    {
                        //Divide the factor, 3f, which is the desired velocity
                        distance = 3f / distance;

                        //Multiply the distance by a multiplier if you wish the projectile to have go faster
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;

                        //Set the velocities to the shoot values
                        Projectile.velocity.X = shootToX;
                        Projectile.velocity.Y = shootToY;
                    }
                }
            }
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            n.AddBuff(20, 600);
            n.AddBuff(ModContent.BuffType<Buffs.PenetratingPoison>(), 600);
        }

        public override bool SafeOnTileCollide(Vector2 OldVelocity)
        {
            Projectile.velocity *= -1;
            return false;
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 5) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
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