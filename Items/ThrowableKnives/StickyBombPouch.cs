using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.Materials;
using VKE.Tiles;

namespace VKE.Items.ThrowableKnives
{
    public class StickyBombPouch : PouchWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sticky Bomb Pouch");
            Tooltip.SetDefault("Pouch filled with sticky bombs that are strung together");
        }
        public override void PouchDefaults()
        {
            Item.width = 40;
            Item.height = 56;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 0, 54, 20);
            Item.rare = 1;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.StickyBomb;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StickyBomb, 99);
            recipe.AddIngredient(ModContent.ItemType<EmptyPouch>(), 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StickyBomb, 50);
            recipe.AddIngredient(ModContent.ItemType<EmptyPouch>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 99);
            recipe.AddIngredient(ModContent.ItemType<BombPouch>(), 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 50);
            recipe.AddIngredient(ModContent.ItemType<BombPouch>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
