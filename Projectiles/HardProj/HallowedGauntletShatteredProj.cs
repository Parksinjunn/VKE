using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.HardProj
{
    public class HallowedGauntletShatteredProj : KnifeProjectile
    {
        bool FramePicked = false;
        int HomingTimer;
        Vector2 OriginalPosition;
        public override void SafeSetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 10;
            Projectile.friendly = true;
            Main.projFrames[Projectile.type] = 6;           
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.scale = Main.rand.NextFloat(0.75f,1.26f);
        }

        public override void AI()
        {
            Projectile.alpha = 0;
            if (FramePicked == false)
            {
                int RandomFrame = Main.rand.Next(0, 6);
                Projectile.frame = RandomFrame;
                Projectile.rotation = MathHelper.ToRadians(Main.rand.Next(0, 361));
                OriginalPosition = Projectile.Center;
                Projectile.damage = 0;
                FramePicked = true;
            }
            if(Projectile.timeLeft < 150)
            {
                Projectile.damage = 3 + Main.rand.Next(0, 5);
            }
            Projectile.rotation += MathHelper.ToRadians(Main.rand.Next(-3, 4));
            Lighting.AddLight(Projectile.position, new Vector3(0.2f, 0.2f, 0.2f));
            if (Projectile.timeLeft < 120 && Projectile.timeLeft > 60)
            {
                Projectile.rotation += MathHelper.ToRadians(3+((200 - Projectile.timeLeft)/60));

                float shootToX = OriginalPosition.X + (float)Projectile.width * 0.5f - Projectile.Center.X;
                    float shootToY = OriginalPosition.Y - Projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                if (distance < 2000f)
                {
                    distance = 3f / distance;

                    shootToX *= distance * 5;
                    shootToY *= distance * 5;

                    Projectile.velocity.X = shootToX;
                    Projectile.velocity.Y = shootToY;
                }
            }
            if(Projectile.timeLeft == 59)
            {
                int iteration = Main.rand.Next(1, 361);
                int ProjectileVelocity = 20;
                Projectile.velocity = new Vector2(ProjectileVelocity * (float)Math.Cos(iteration / ProjectileVelocity), ProjectileVelocity * (float)Math.Sin(iteration / ProjectileVelocity));
                Projectile.tileCollide = false;

                float CircleRadius = 0.85f;
                for (int iteration2 = 0; iteration2 < 360; iteration2++)
                {
                    int DustID3 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 15, 0f, 0f, 10, Color.White, 2f);
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID3].velocity = new Vector2(CircleRadius * (float)Math.Cos(iteration2 / CircleRadius), CircleRadius * (float)Math.Sin(iteration2 / CircleRadius));
                }
            }
            if(Projectile.timeLeft == 39)
            {
                float CircleRadius = 0.65f;
                for (int iteration = 0; iteration < 360; iteration++)
                {
                    int DustID3 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 15, 0f, 0f, 10, Color.White, 2f);
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID3].velocity = new Vector2(CircleRadius * (float)Math.Cos(iteration / CircleRadius), CircleRadius * (float)Math.Sin(iteration / CircleRadius));
                }
            }
            if(Projectile.timeLeft == 29)
            {
                float CircleRadius = 0.45f;
                for (int iteration = 0; iteration < 360; iteration++)
                {
                    int DustID3 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 15, 0f, 0f, 10, Color.White, 2f);
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID3].velocity = new Vector2(CircleRadius * (float)Math.Cos(iteration / CircleRadius), CircleRadius * (float)Math.Sin(iteration / CircleRadius));
                }
            }
            if(Projectile.timeLeft == 9)
            {
                float CircleRadius = 0.25f;
                for (int iteration = 0; iteration < 360; iteration++)
                {
                    int DustID3 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 15, 0f, 0f, 10, Color.White, 2f);
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID3].velocity = new Vector2(CircleRadius * (float)Math.Cos(iteration / CircleRadius), CircleRadius * (float)Math.Sin(iteration / CircleRadius));
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity *= 0f;
            Projectile.tileCollide = false;
            return false;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {

        }
        public override bool SafePreKill(int timeLeft)
        {
            float CircleRadius = 0.15f;
            for (int iteration = 0; iteration < 360; iteration++)
            {
                int DustID3 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 15, 0f, 0f, 10, Color.White, 2f);
                Main.dust[DustID3].noGravity = true;
                Main.dust[DustID3].velocity = new Vector2(CircleRadius * (float)Math.Cos(iteration / CircleRadius), CircleRadius * (float)Math.Sin(iteration / CircleRadius));
            }
            return base.SafePreKill(timeLeft);
        }
    }
}