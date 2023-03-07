using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.Materials;
using VKE.Items.MaterialKnives;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
	public class WeakVampireKnives : KnifeItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lesser Vampire Knives");
			Tooltip.SetDefault("Weak Life Stealing Knives");
        }
		public override void SafeSetDefaults()
		{
			Item.damage = 9;
            
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 15;
			Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = 1000;
			Item.rare = 8;
			Item.UseSound = SoundID.Item39;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("WeakVampireProj").Type;
            Item.shootSpeed = 16f;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<StableCrimsonCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<IronKnives>(), 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<StableCrimsonCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<IronKnives>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
		}
	}

}
