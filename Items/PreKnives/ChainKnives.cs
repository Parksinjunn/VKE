using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class ChainKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chain Knives");
            Tooltip.SetDefault("Blades attatched to large chains that retract upon hitting a target");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 54; 
            Item.width = 66;
            Item.height = 52;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item39; 
            Item.shoot = Mod.Find<ModProjectile>("ChainProj").Type;
            Item.shootSpeed = 24f;
            NumProj = 3;
        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChainGuillotines, 2);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChainKnife, 5);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(ItemID.IronBar, 40);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddIngredient(ItemID.SilverBar, 20);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBar, 5);
            recipe.AddIngredient(ItemID.IronBar, 40);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddIngredient(ItemID.TungstenBar, 20);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChainGuillotines, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChainKnife, 4);
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddIngredient(ItemID.IronBar, 20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddIngredient(ItemID.SilverBar, 10);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBar, 3);
            recipe.AddIngredient(ItemID.IronBar, 20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddIngredient(ItemID.TungstenBar, 10);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
