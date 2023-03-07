using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.Ammo;
using VKE.Tiles;

namespace VKE.Items.MaterialKnives
{
    public class TungstenKnives : KnifeMaterialItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tungsten Knives");
            Tooltip.SetDefault("Decreases the defense of hit enemies");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 3;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 0, 15, 50);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.shoot = Mod.Find<ModProjectile>("IronProj").Type;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
            Item.useAmmo = ModContent.ItemType<ThrowingKnivesIron>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            recipe.AddTile(TileID.Furnaces);
                        recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TungstenBar, 6);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
                        recipe.Register();
        }
    }

}
