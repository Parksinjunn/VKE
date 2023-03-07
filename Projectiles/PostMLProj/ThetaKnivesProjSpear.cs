using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PostMLProj
{
    public class ThetaKnivesProjSpear : KnifeProjectile
    {
        public int Mode;
        public Vector2 TargetPos;
        int Delay;
        bool PastTarget;
        bool PosDefined;
        Vector2 VelocityStore;
        float distance;
        public override void SafeSetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.scale = 1f;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 260;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Hoods(n);
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Player p = Main.player[Projectile.owner];

            if (Mode == 0)
            {
                if(Delay < 4)
                    Delay++;
                else
                {
                    float shootToX = TargetPos.X - Projectile.position.X;
                    float shootToY = TargetPos.Y - Projectile.position.Y;

                    distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                    if (distance >= 50 && !PastTarget)
                    {
                        Vector2 velo = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(TargetPos) * 17f, 0.10f);
                        Projectile.velocity = velo;
                    }
                    else if (distance < 50 && !PastTarget)
                    {
                        VelocityStore = Projectile.velocity;
                        PastTarget = true;
                    }
                    else if (PastTarget)
                    {
                        Projectile.velocity = VelocityStore;
                    }
                    Projectile.netUpdate = true;
                }
            }
            else if (Mode == 1)
            {
                /*Position the player based on where the player is, the Sin/Cos of the angle times the /
                /distance for the desired distance away from the player minus the projectile's width   /
                /and height divided by two so the center of the projectile is at the right place.     */
                if(!PosDefined)
                {
                    double deg = (double)Projectile.ai[0] * 72; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                    double rad = deg * (Math.PI / 180); //Convert degrees to radians
                    double dist = 50; //Distance away from the player

                    Projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                    Projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                    Projectile.tileCollide = false;
                    PosDefined = true;
                    Projectile.ai[1] += (float)deg;
                }
                else
                {
                    double deg = (double)Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                    double rad = deg * (Math.PI / 180); //Convert degrees to radians
                    double dist = 50; //Distance away from the player

                    float posAddX = ((p.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2) - Projectile.position.X) / 5f;
                    float posAddY = ((p.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2) - Projectile.position.Y) / 5f;
                    Projectile.velocity = new Vector2(posAddX, posAddY);
                }


                //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                Projectile.ai[1] += 4f;
            }
            else if (Mode == 2)
            {
                if (!PosDefined)
                {
                    double deg = (double)Projectile.ai[0] * 72; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                    double rad = deg * (Math.PI / 180); //Convert degrees to radians
                    double dist = 50; //Distance away from the player

                    //projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
                    //projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;
                    Projectile.position = new Vector2(p.position.X, p.position.Y - 20f);
                    Projectile.tileCollide = false;
                    PosDefined = true;
                    Projectile.ai[1] += (float)deg;
                }
                else
                {
                    double deg = (double)Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                    double rad = deg * (Math.PI / 180); //Convert degrees to radians
                    double dist = 50; //Distance away from the player

                    float posAddX = ((Main.MouseWorld.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2) - Projectile.position.X) / 5f;
                    float posAddY = ((Main.MouseWorld.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2) - Projectile.position.Y) / 5f;
                    Projectile.velocity = new Vector2(posAddX, posAddY);
                }

                Projectile.netUpdate = true;
                //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                Projectile.ai[1] += 4f;
            }
        }
    }
}