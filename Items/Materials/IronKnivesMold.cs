using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class IronKnivesMold : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Knives Cast");
            Tooltip.SetDefault("A cast used to make a metal knife fan");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
        }
        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ModContent.ItemType<StoneKnifeSculpt>(), 1);
        //    recipe.AddIngredient(ItemID.IronBar, 5);
        //    recipe.AddRecipeGroup(RecipeGroupID.IronBar);
        //    recipe.AddTile(ModContent.TileType<KnifeBench>());
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ModContent.ItemType<StoneKnifeSculpt>(), 1);
        //    recipe.AddIngredient(ItemID.IronBar, 3);
        //    recipe.AddRecipeGroup(RecipeGroupID.IronBar);
        //    recipe.AddTile(ModContent.TileType<VampTableTile>());
        //    recipe.Register();
        //}
    }
}