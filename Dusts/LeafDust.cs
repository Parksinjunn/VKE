using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace VKE.Dusts
{
    public class LeafDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity = Vector2.Zero;
            base.OnSpawn(dust);
        }
        int rotation;
        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position, Color.White.ToVector3());
            if (dust.customData != null && dust.customData is Projectile projectile && projectile.active)
            {
                rotation += 30;
                if (Main.rand.Next(0, 2) == 1)
                    dust.rotation += 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
                else
                    dust.rotation -= 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
                //dust.scale -= 0.05f;
                //if(projectile.ai[1] == 0f)
                //{
                //Vector2 rot = (Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.alpha / 50) * Main.rand.Next(6, 30);
                //dust.position = projectile.Center + rot;
                //}

                //else if (projectile.ai[1] == 1f)
                //{
                    double deg = (double)rotation; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                    double rad = deg * (Math.PI / 180); //Convert degrees to radians
                    double dist = 230; //Distance away from the player

                    float posAddX = ((projectile.Center.X - (int)(Math.Cos(rad) * dist)) - dust.position.X) / 5f;
                    float posAddY = ((projectile.Center.Y - (int)(Math.Sin(rad) * dist)) - dust.position.Y) / 5f;
                    dust.velocity = new Vector2(posAddX, posAddY);
                dust.position += dust.velocity;
                //}
            }
            else
            {
                if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                {
                    dust.velocity *= 0.25f;
                }
                else if (!dust.noGravity)
                {
                    dust.velocity.Y += 0.35f;
                }
                dust.position += dust.velocity;
            }
            //dust.rotation += dust.velocity.X * 0.1f;
            dust.alpha += 80;
            if (dust.alpha > 255)
            {
                dust.active = false;
            }
            return false;
        }
        //public override Color? GetAlpha(Dust dust, Color lightColor)
        //{
        //    return Color.;
        //}
    }
}