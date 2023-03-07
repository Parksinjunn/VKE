using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials
{
    public class CorruptionCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unstable Corruption Crystal");
            Tooltip.SetDefault("Seems to have corrupt energy pouring from its cracks");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 9));
        }
        public override void SetDefaults()
        {
            Item.maxStack = 10;
            Item.value = Item.sellPrice(0, 0, 50, 0) ;
            Item.rare = 11;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CrimsonCrystal>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}