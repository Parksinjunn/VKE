using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace VKE.Dusts
{
	internal class TrailDust : ModDust
	{

		public override void OnSpawn(Dust dust)
		{
			//dust.noGravity = true;

			//int desiredVanillaDustTexture = 135;
			//int frameX = desiredVanillaDustTexture * 10 % 1000;
			//int frameY = desiredVanillaDustTexture * 10 / 1000 * 30 + Main.rand.Next(3) * 10;
			//dust.frame = new Rectangle(frameX, frameY, 8, 8);

			//dust.velocity = Vector2.Zero;
			dust.alpha = 160;
		}
		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.alpha+= 25;
			if (dust.alpha > 255)
				dust.active = false;
			//dust.scale *= 0.76f;
			//if (dust.scale < 0.3f)
   //         {
			//	dust.active = false;
   //         }
			return false;
		}
    }
}