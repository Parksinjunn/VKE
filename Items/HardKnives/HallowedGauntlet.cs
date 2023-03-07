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
	public class HallowedGauntlet : KnifeItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Knives");
			Tooltip.SetDefault("Life and mana stealing knives that chip upon contact and fire off in random directions");
            Item.staff[Item.type] = true;
        }
        public override void SafeSetDefaults()
		{
			Item.damage = 42;
			Item.width = 52;
			Item.height = 52;
			Item.useTime = 15;
			Item.useAnimation = 15;
            Item.scale = 0.65f;
            Item.channel = true;
            Item.useStyle = 5;
            Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0,0,68,52);
			Item.rare = 8;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("HallowedGauntletProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.UnicornHorn, 5);
            recipe.AddIngredient(ItemID.HallowedBar, 14);
            recipe.AddIngredient(ItemID.SoulofLight, 20);
            recipe.AddIngredient(ModContent.ItemType<ManaKnivesAnimated>());
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.StableCrimsonCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.StableCorruptionCrystal>(), 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
			recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.UnicornHorn, 5);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 14);
            recipe.AddIngredient(ModContent.ItemType<ManaKnivesAnimated>());
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.StableCrimsonCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.StableCorruptionCrystal>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
