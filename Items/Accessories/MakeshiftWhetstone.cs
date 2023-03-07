using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.DataStructures;
using Terraria.Localization;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Accessories
{
    public class MakeshiftWhetstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Makeshift Whetstone");
            Tooltip.SetDefault("A crude stone used to sharpen your knives" +
                "\n15% increased knife critical strike chance");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = 2;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 0, 5, 20);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance<KnifeDamageClass>() += 15;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StoneBlock, 20);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StoneBlock, 15);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
