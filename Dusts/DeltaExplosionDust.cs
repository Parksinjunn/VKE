using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace VKE.Dusts
{
	public class DeltaExplosionDust : ModDust
	{
		float SpeedCap = 50f;
		public override bool Update(Dust dust)
		{
			if(dust.velocity.X >= SpeedCap)
            {
				dust.velocity.X = SpeedCap;
            }
			else if(dust.velocity.X <= -SpeedCap)
            {
				dust.velocity.X = -SpeedCap;
            }
			if(dust.velocity.Y >= SpeedCap)
            {
				dust.velocity.Y = SpeedCap;
            }
			else if (dust.velocity.Y <= -SpeedCap)
			{
				dust.velocity.Y = -SpeedCap;
			}
			float num63 = dust.scale * 1.4f;
			if(num63 > 1f)
            {
				num63 = 1f;
            }
			Lighting.AddLight(new Vector2((int)(dust.position.X), (int)(dust.position.Y)), new Vector3(0.76f, 0.4f, 0.9f));
			if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
            {
                dust.velocity *= 0.25f;
            }
			else
            {
				dust.velocity.Y += 0.35f;
			}
            dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.1f;
			dust.scale -= 0.02f;
			if (dust.scale < 0.3f)
			{
				dust.active = false;
			}
			return false;
		}
        //public static void LightColor(Vector3 color, Dust DustID)
        //{
        //    Lighting.AddLight(DustID.position, color);
        //}
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            int fade = (int)(dust.scale / 2.5f * 255f);
            return new Color(fade * 1.2f, fade * 0.98f, fade * 1.5f);
        }
    }
}