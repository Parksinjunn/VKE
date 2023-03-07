using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class Chisel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chisel");
            Tooltip.SetDefault("[c/FF0000:Used to chip stone into shapes]\nCan be stored in the knife workbench");
        }
        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 16;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 1);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddIngredient(ItemID.Wood, 1);
            recipe.AddRecipeGroup(RecipeGroupID.Wood);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 1);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddIngredient(ItemID.Wood, 1);
            recipe.AddRecipeGroup(RecipeGroupID.Wood);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}