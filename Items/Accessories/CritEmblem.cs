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
    public class CritEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crit Emblem");
            Tooltip.SetDefault("8% increased knife critical strike chance to all damage types\nUsed in upgrading your knives");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.rare = 2;
            Item.accessory = true;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(0, 2, 40, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Magic) += 8;
            player.GetCritChance(DamageClass.Generic) += 8;
            player.GetCritChance(DamageClass.Throwing) += 8;
            player.GetCritChance(DamageClass.Ranged) += 8;
            player.GetCritChance<KnifeDamageClass>() += 8;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 6);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 3);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
