using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class PlanteraCloth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magical Plant Silk");
            //Tooltip.SetDefault("Cloth infused with the power of a magical beast");
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 44;
            Item.maxStack = 99;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlantFiber>(), 7);
            recipe.AddTile(TileID.LivingLoom);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CorruptionCrystal>(), 5);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}