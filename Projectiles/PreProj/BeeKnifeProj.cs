using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SteelSeries.GameSense;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items;

namespace VKE.Projectiles.PreProj
{
	public class BeeKnifeProj : KnifeProjectile
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
            KnifeMomentum = 0;
			Projectile.Name = "Bee Proj";
			Projectile.width = 34;
			Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                      
            Main.projFrames[Projectile.type] = 4;         
            Projectile.hostile = false;
            Projectile.tileCollide = true;                
			Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            amplitude = Main.rand.Next(2, 8);
            frequency = Main.rand.Next(5, 12);
            if (Main.rand.NextBool())
                neg = true;
            else
                neg = false;
            SpawnChance = Main.rand.Next(1, 8);
        }

        public override void SafeAI()
        {
            elapsedTime += 1f / 60f; // 1f / 60f is the equivalent of Time.deltaTime in tModLoader

            //Projectile.position.Y += ((15 * (float)Math.Sin(Projectile.timeLeft / 11.25)) / (float)Math.PI * 2f);
            if (neg)
                Projectile.position = Projectile.position + Projectile.velocity * elapsedTime + new Vector2(amplitude * (float)Math.Cos(frequency * elapsedTime), amplitude * (float)Math.Sin(frequency * elapsedTime));
            else
                Projectile.position = Projectile.position + Projectile.velocity * elapsedTime + new Vector2(amplitude * (float)Math.Sin(frequency * elapsedTime), amplitude * (float)Math.Cos(frequency * elapsedTime));
        }
        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            float ran1 = Main.rand.NextFloat(-10f, 10f);
            float ran2 = Main.rand.NextFloat(-10f, 10f);
            Player owner = Main.player[Projectile.owner];
            n.AddBuff(32, 300); //Slow 5 sec
            n.AddBuff(31, 120); //confused 2 sec
            if (SpawnChance > 4)
            {
                if(owner.strongBees == true)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(ran1, ran2), 566, Projectile.damage, Projectile.knockBack, Main.myPlayer);
                else
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(ran1, ran2), 181, Projectile.damage, Projectile.knockBack, Main.myPlayer);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            float ran1 = Main.rand.NextFloat(-10f, 10f);
            float ran2 = Main.rand.NextFloat(-10f, 10f);
            Player owner = Main.player[Projectile.owner];
            if (SpawnChance > 6)
            {
                if (owner.strongBees == true)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(ran1, ran2), 566, Projectile.damage, Projectile.knockBack, Main.myPlayer);
                else
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(ran1, ran2), 181, Projectile.damage, Projectile.knockBack, Main.myPlayer);
            }
            return true;
        }
        public override bool PreDraw(ref Color lightColor) 
        {
            if (!ZenithActive)
            {
                Projectile.frameCounter++; 
                if (Projectile.frameCounter >= 3) 
                {
                    Projectile.frame++; 
                    Projectile.frameCounter = 0; 
                    if (Projectile.frame > 3) 
                        Projectile.frame = 0; 
                }
            }
            return true;
        }
    }
}