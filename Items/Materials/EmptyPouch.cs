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
    public class EmptyPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Defaults
            DisplayName.SetDefault("Empty Pouch");
            Tooltip.SetDefault("An empty pouch");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 48;
            Item.maxStack = 99;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.GreenThread, 6);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 9);
            recipe.AddIngredient(ItemID.GreenThread, 3);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlanteraCloth>(), 6);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlanteraCloth>(), 3);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
