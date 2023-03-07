using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class PiercingTip : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Defaults
            DisplayName.SetDefault("Piercing Tip");
            Tooltip.SetDefault("Sharp! Handle with care!\nUsed in upgrading your knives");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Spear, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
