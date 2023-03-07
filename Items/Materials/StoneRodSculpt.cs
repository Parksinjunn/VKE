using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class StoneRodSculpt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sculpted Rod ");
            Tooltip.SetDefault("A rough sculpt of a sharpening rod");
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.StoneBlock, 20);
        //    recipe.AddTile(ModContent.TileType<KnifeBench>());
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.StoneBlock, 15);
        //    recipe.AddTile(ModContent.TileType<VampTableTile>());
        //    recipe.Register();
        //}
    }
}