using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SteelSeries.GameSense;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Projectiles.HardProj;

namespace VKE.Projectiles.HardProj
{
	public class WaspKnifeProj : KnifeProjectile
	{
        // The amplitude of the sinusoidal movement
        public float amplitude;

        // The frequency of the sinusoidal movement
        public float frequency;

        // The elapsed time since the projectile was launched
        private float elapsedTime;
        bool neg;
        int SpawnChance;
        public override void SafeSetDefaults()
		{
			Projectile.Name = "Wasp Knife";
			Projectile.width = 16;
			Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
            Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
			Projectile.ignoreWater = true;
            Projectile.timeLeft = 120;
            amplitude = Main.rand.Next(2, 6);
            frequency = Main.rand.Next(5, 12);
            if (Main.rand.NextBool())
                neg = true;
            else
                neg = false;
            SpawnChance = Main.rand.Next(1, 8);
        }

		public override void AI()
		{
                                                                //this is projectile dust
           int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width-3, Projectile.height-3, 244, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.DarkRed, 1.8f);
           Main.dust[DustID2].noGravity = true;
                                                          //this make that the projectile faces the right way 
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
            elapsedTime += 1f / 60f; // 1f / 60f is the equivalent of Time.deltaTime in tModLoader

            if (neg)
                Projectile.position = Projectile.position + Projectile.velocity * elapsedTime + new Vector2(amplitude * (float)Math.Cos(frequency * elapsedTime), amplitude * (float)Math.Sin(frequency * elapsedTime));
            else
                Projectile.position = Projectile.position + Projectile.velocity * elapsedTime + new Vector2(amplitude * (float)Math.Sin(frequency * elapsedTime), amplitude * (float)Math.Cos(frequency * elapsedTime));
        }

        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            float ran1 = Main.rand.Next(-2, 2);
            float ran2 = Main.rand.Next(-2, 2);
            Player owner = Main.player[Projectile.owner];
            n.AddBuff(32, 240); //Slow 4
            n.AddBuff(31, 120); //confused 2
            n.AddBuff(20, 600); //poisoned 10
            if (SpawnChance > 3)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, ran1, ran2, ModContent.ProjectileType<Wasp>(), 21, Projectile.knockBack, Main.myPlayer);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(SpawnChance > 5)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, ModContent.ProjectileType<Wasp>(), 21, Projectile.knockBack, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, ModContent.ProjectileType<Wasp>(), 21, Projectile.knockBack, Main.myPlayer);
            }
            return true;
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