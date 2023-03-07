using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VKE.Dusts
{
	public class PsiExplosionDust : ModDust
	{
		//public override void OnSpawn(Dust dust)
		//{
		//	dust.noGravity = true;
		//	dust.frame = new Rectangle(0, 0, 30, 30);
		//	//If our texture had 2 different dust on top of each other (a 30x60 pixel image), we might do this:
		//	//dust.frame = new Rectangle(0, Main.rand.Next(2) * 30, 30, 30);
		//	dust.scale = 0.5f;
		//}
		//public override bool Update(Dust dust)
		//{
		//	dust.position += dust.velocity;
		//	dust.scale -= 0.02f;
		//	if (dust.scale < 0.1f)
		//	{
		//		dust.active = false;
		//	}
		//	return false;
		//}
		//public static void LightColor(Vector3 color, Dust DustID)
		//      {
		//	Lighting.AddLight(DustID.position, color);
		//}
		/*
			Spawning this dust is a little more involved because we need to assign a rotation, customData, and fix the position.
			Position must be fixed here because otherwise the first time the dust is drawn it'll draw in the incorrect place.
			This dust is not used in ExampleMod yet, so you'll have to add some code somewhere. Try ExamplePlayer.DrawEffects.
			Dust dust = Dust.NewDustDirect(Player.Center, 0, 0, ModContent.DustType<Content.Dusts.AdvancedDust>(), Scale: 2);
			dust.rotation = Main.rand.NextFloat(6.28f);
			dust.customData = Player;
			dust.position = Player.Center + Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.scale * 50;
		*/

		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			// Since the vanilla dust texture has all the dust in 1 file, we'll need to do some math.
			// If you want to use a vanilla dust texture, you can copy and paste it, changing the desiredVanillaDustTexture
			int frameY = 10 * Main.rand.Next(0, 5);
			dust.frame = new Rectangle(0, frameY, 10, 10);

			dust.velocity = Vector2.Zero;
		}
		// This Update method shows off some interesting movement. Using customData assigned to a Player, we spiral around the Player while slowly getting closer. In practice, it looks like a vortex.
		public override bool Update(Dust dust)
		{
			// Here we rotate and scale down the dust. The dustIndex % 2 == 0 part lets half the dust rotate clockwise and the other half counter clockwise
			if (Main.rand.Next(0, 2) == 1)
				dust.rotation += 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
			else
				dust.rotation -= 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
			dust.scale -= 0.05f;
			// Here we use the customData field. If customData is the type we expect, Player, we do some special movement.
			if (dust.customData != null && dust.customData is Projectile projectile)
			{
				// Here we assign position to some offset from the player that was assigned. This offset scales with dust.scale. The scale and rotation cause the spiral movement we desired.
				dust.position = projectile.Center + Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.scale * 50;
			}

			// Here we make sure to kill any dust that get really small.
			if (dust.scale < 0.25f)
				dust.active = false;

			return false;
		}
        public static void LightColor(Vector3 color, Dust DustID)
        {
            Lighting.AddLight(DustID.position, color);
        }
    }
}