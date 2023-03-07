using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.MaterialKnives;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
	public class BeeKnives : KnifeItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Knives");
			Tooltip.SetDefault("Knives with an internal compartment \nfilled with bees that explode out on impact");
        }
		public override void SafeSetDefaults()
		{
			Item.damage = 8;            
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 21;
			Item.useAnimation = 21;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0,0,54,20);
			Item.rare = 8;
			Item.UseSound = SoundID.Item97;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("BeeKnifeProj").Type;
            Item.shootSpeed = 15f;
			NumProj = 5;
			KnifeSpread = Main.rand.Next(20, 45);
			KnifeSpreadDef = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HoneyBlock, 50);
            recipe.AddIngredient(ModContent.ItemType<IronKnives>(), 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HoneyBlock, 35);
            recipe.AddIngredient(ModContent.ItemType<IronKnives>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
		}
	}

}
