using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace VKE.Dusts
{
    public class LeafDustTrail : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity = Vector2.Zero;
            base.OnSpawn(dust);
        }
        public override bool Update(Dust dust)
        {
            if (dust.customData != null && dust.customData is Projectile projectile && projectile.active)
            {
                if (Main.rand.Next(0, 2) == 1)
                    dust.rotation += 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
                else
                    dust.rotation -= 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
                //dust.scale -= 0.05f;
                // Here we assign position to some offset from the player that was assigned. This offset scales with dust.scale. The scale and rotation cause the spiral movement we desired.
                dust.position = projectile.Center + Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.scale * 50;

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
            dust.alpha += 60;
            if (dust.alpha > 255)
            {
                dust.active = false;
            }
            return false;
        }
        //public override Color? GetAlpha(Dust dust, Color lightColor)
        //{
        //    return col;
        //}
    }
}