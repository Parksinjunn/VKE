using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.Materials;
using VKE.Tiles;

namespace VKE.Items.Tiles
{
    public class KnifeBenchItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Knife Worktable");
            Tooltip.SetDefault("Used to craft knives");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 14;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = Mod.Find<ModTile>("KnifeBench").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddRecipeGroup(RecipeGroupID.Wood);
            recipe.AddIngredient(ItemID.IronBar, 8);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddIngredient(ModContent.ItemType<Hammer>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Chisel>(), 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddRecipeGroup(RecipeGroupID.Wood);
            recipe.AddIngredient(ItemID.IronBar, 6);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            recipe.AddIngredient(ModContent.ItemType<Hammer>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Chisel>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}