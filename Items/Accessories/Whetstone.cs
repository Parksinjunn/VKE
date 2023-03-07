using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Accessories
{
    public class Whetstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Whetstone");
            Tooltip.SetDefault("2,000 grit stone and 5,000 grit stone"
                + "\nput together in one brick"
                + "\n20% increased knife damage\nUsed in upgrading your knives");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = 2;
            Item.maxStack = 20;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<KnifeDamageClass>() += 0.2f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SiltBlock, 50);
            recipe.AddIngredient(ItemID.SandBlock, 50);
            recipe.AddIngredient(ItemID.Granite, 50);
            recipe.AddIngredient(ItemID.Marble, 50);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SiltBlock, 25);
            recipe.AddIngredient(ItemID.SandBlock, 25);
            recipe.AddIngredient(ItemID.Granite, 25);
            recipe.AddIngredient(ItemID.Marble, 25);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
