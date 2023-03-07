
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class StableCrimsonCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stable Crimson Crystal");
            Tooltip.SetDefault("The life energy of a monsterous beast is sealed within this translucent crimson vessel\nUsed in upgrading your knives");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 30;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 10;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CrimsonCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Superglue>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CrimsonCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Superglue>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}