using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials
{
    public class CrimsonCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unstable Crimson Crystal");
            Tooltip.SetDefault("Radiates the life essence of a monsterous beast");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 9));
        }
        public override void SetDefaults()
        {
            Item.maxStack = 10;
            Item.value = 5000;
            Item.rare = 10;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CorruptionCrystal>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}