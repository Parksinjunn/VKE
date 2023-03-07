using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.HealProj
{
    public class ManaHeal : KnifeProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Name = "Mana Healing";
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = false;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
        }

        public override void AI()
        {
            //this is projectile dust
            Dust dust;
            dust = Dust.NewDustPerfect(new Vector2(Projectile.position.X, Projectile.position.Y), 135, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), 10, new Color(255, 0, 0), 2.5f);
            dust.noGravity = true;

            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 135, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 255, Color.LightBlue, 2.5f);
            Main.dust[DustID2].noGravity = true;
            //this make that the projectile faces the right way 
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;

            for (int i = 0; i < 200; i++)
            {
                Player owner = Main.player[Projectile.owner];
                //Get the shoot trajectory from the projectile and target
                float shootToX = owner.position.X + owner.width * 0.5f - Projectile.Center.X;
                float shootToY = owner.position.Y - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                //If the distance between the live targeted player and the projectile is less than 480 pixels
                if (distance < 2000f && owner.active)
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
                float distanceX = owner.position.X - Projectile.position.X;
                float distanceY = owner.position.Y - Projectile.position.Y;
                if (distance < 50f && Projectile.position.X < owner.position.X + owner.width && Projectile.position.X + Projectile.width > owner.position.X && Projectile.position.Y < owner.position.Y + owner.height && Projectile.position.Y + Projectile.height > owner.position.Y)
                {
                    owner.statMana += Projectile.damage;
                    owner.ManaEffect(Projectile.damage);
                    Projectile.Kill();
                    break;
                }
            }
        }
    }
}
//For all of the NPC slots in Main.npc
//Note, you can replace NPC with other entities such as Projectiles and Players
