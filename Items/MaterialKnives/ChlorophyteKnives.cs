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

namespace VKE.Items.MaterialKnives
{
    public class ChlorophyteKnives : KnifeMaterialItem
    {
        public override void SetStaticDefaults()
        {
                DisplayName.SetDefault("Chlorophyte Knives");
            Tooltip.SetDefault("Homes in on enemies\nDecreases the defense of hit enemies");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(15, 10));
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 15;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 4, 30, 20);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.shoot = Mod.Find<ModProjectile>("ChlorophyteProj").Type;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddTile(TileID.Furnaces);
                        recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 6);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
                        recipe.Register();
        }
    }

}
