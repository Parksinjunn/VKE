
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class Superglue : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Superglue");
            Tooltip.SetDefault("It contains a mysterious white sticky substance with adhesive properties");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 99;
            Item.value = Item.buyPrice(0,0,50,0);
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ItemID.Gel, 50);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ItemID.Gel, 50);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}