using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.HardKnives;
using VKE.Items.Materials;
using VKE.Tiles;

namespace VKE.Items.Tiles
{
    public class VampTable : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampire Altar");
            Tooltip.SetDefault("Used to craft knives more efficiently \n(most recipes from this mod are cheaper at this altar)");
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 47;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("VampTableTile").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VampireKnives);
            recipe.AddIngredient(ItemID.Obsidian, 75);
            recipe.AddIngredient(ModContent.ItemType<StableCrimsonCrystal>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VampireKnivesMagic>());
            recipe.AddIngredient(ItemID.Obsidian, 75);
            recipe.AddIngredient(ModContent.ItemType<StableCrimsonCrystal>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VampireKnives);
            recipe.AddIngredient(ItemID.Obsidian, 75);
            recipe.AddIngredient(ModContent.ItemType<StableCrimsonCrystal>(), 2);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VampireKnivesMagic>());
            recipe.AddIngredient(ItemID.Obsidian, 75);
            recipe.AddIngredient(ModContent.ItemType<StableCrimsonCrystal>(), 2);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();
        }
    }
}