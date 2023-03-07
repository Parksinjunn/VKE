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
    public class MechanicalFingers : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("They seem to move on their own...\nAdds two more knives with every strike");
        }

        public override void SetDefaults()
        {
            Item.width = 43;
            Item.height = 43;
            Item.value = 10000;
            Item.rare = 2;
            Item.accessory = true;
            Item.defense = 2;
            Item.value = Item.sellPrice(0, 10, 20, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<VampPlayer>().ExtraProj += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddIngredient(ItemID.Bone, 60);
            recipe.AddIngredient(ModContent.ItemType<ExtraFinger>());
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ModContent.ItemType<ExtraFinger>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
