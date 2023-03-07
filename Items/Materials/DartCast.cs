using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class DartCast : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwing Knives Cast");
            Tooltip.SetDefault("Used to make ammo for the Material Knives");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
        }
        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ModContent.ItemType<StoneAmmoSculptComplete>(), 1);
        //    recipe.AddIngredient(ItemID.IronBar, 5);
        //    recipe.AddRecipeGroup(RecipeGroupID.IronBar);
        //    recipe.AddTile(ModContent.TileType<KnifeBench>());
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ModContent.ItemType<StoneAmmoSculptComplete>(), 1);
        //    recipe.AddIngredient(ItemID.IronBar, 3);
        //    recipe.AddRecipeGroup(RecipeGroupID.IronBar);
        //    recipe.AddTile(ModContent.TileType<VampTableTile>());
        //    recipe.Register();
        //}
    }
}