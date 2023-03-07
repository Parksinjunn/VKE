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

namespace VKE.Items.Accessories
{
    public class ExtraFinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Y'know what they say, five's better than four\nAdds another knife to each strike");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.rare = 2;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 5, 50, 60);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<VampPlayer>().ExtraProj += 1;
        }

    //public override void AddRecipes()
    //    {
    //        ModRecipe recipe = new ModRecipe(mod);
    //        recipe.AddIngredient(ItemID.DirtBlock, 5);
    //        recipe.AddTile(mod.GetTile("KnifeBench"));
    //        recipe.SetResult(this);
    //        recipe.AddRecipe();
    //    }
    }
}
