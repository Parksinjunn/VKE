using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public abstract class PlateItem : ModItem
    {
        public short BarType;
        public int Rarity;
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.rare = Rarity;
            Item.maxStack = 99;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(BarType, 3);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(BarType, 2);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}