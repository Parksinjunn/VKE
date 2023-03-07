using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class SharpeningRodCast : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharpening Rod Cast");
            Tooltip.SetDefault("Cast used to make a sharpening rod");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
        }
        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ModContent.ItemType<StoneRodSculpt>(), 1);
        //    recipe.AddIngredient(ItemID.IronBar, 5);
        //    recipe.AddRecipeGroup(RecipeGroupID.IronBar);
        //    recipe.AddTile(ModContent.TileType<KnifeBench>());
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ModContent.ItemType<StoneRodSculpt>(), 1);
        //    recipe.AddIngredient(ItemID.IronBar, 3);
        //    recipe.AddRecipeGroup(RecipeGroupID.IronBar);
        //    recipe.AddTile(ModContent.TileType<VampTableTile>());
        //    recipe.Register();
        //}
    }
}