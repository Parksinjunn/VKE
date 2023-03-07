using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.PreKnives;
using VKE.Tiles;

namespace VKE.Items.HardKnives
{
	public class WaspKnives : KnifeItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wasp Knives");
			Tooltip.SetDefault("Life Stealing Knives Covered in Wasps");
        }
		public override void SafeSetDefaults()
		{
			Item.damage = 31;
            Item.mana = 6;
            
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.useStyle = 1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 3;
			Item.value = 1000;
			Item.rare = 8;
			Item.UseSound = SoundID.Item97;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("WaspKnifeProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LifeFruit, 3);
            recipe.AddIngredient(ItemID.Stinger, 15);
            recipe.AddIngredient(ModContent.ItemType<BeeKnives>());
            recipe.AddTile(ModContent.TileType<KnifeBench>());
			recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeFruit, 1);
            recipe.AddIngredient(ItemID.Stinger, 12);
            recipe.AddIngredient(ModContent.ItemType<BeeKnives>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
	}

}
