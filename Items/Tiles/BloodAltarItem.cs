using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using vke.Tiles;
using VKE.Items.Materials;
using VKE.Tiles;

namespace VKE.Items.Tiles
{
    public class BloodAltarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Altar (NOT WORKING)");
            Tooltip.SetDefault("An altar to perform rituals at");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 16;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<BloodAltar>();
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.Obsidian, 40);
        //    recipe.AddIngredient(ItemID.HallowedBar, 12);
        //    recipe.AddIngredient(ModContent.ItemType<StableCrimsonCrystal>(), 4);
        //    recipe.AddTile(TileID.WorkBenches);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.Obsidian, 40);
        //    recipe.AddIngredient(ItemID.HallowedBar, 12);
        //    recipe.AddIngredient(ModContent.ItemType<StableCrimsonCrystal>(), 4);
        //    recipe.AddTile(ModContent.TileType<KnifeBench>());
        //    recipe.Register();
        //}
    }
}