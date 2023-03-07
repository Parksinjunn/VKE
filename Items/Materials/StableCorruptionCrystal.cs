using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class StableCorruptionCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stable Corruption Crystal");
            Tooltip.SetDefault("Energy is sealed within this translucent purple vessel");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 30;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 11;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CorruptionCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Superglue>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CorruptionCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Superglue>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}