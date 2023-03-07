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
    public class MythrilKnives : KnifeMaterialItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Knives");
            Tooltip.SetDefault("Decreases the defense of hit enemies");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 10;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.crit = 15;
            Item.value = Item.sellPrice(0, 2, 20, 32);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("IronProj").Type;
            Item.shootSpeed = 15f;
            Item.useAmmo = ModContent.ItemType<ThrowingKnivesIron>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MythrilBar, 8);
            recipe.AddTile(TileID.Furnaces);
                        recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MythrilBar, 6);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
                        recipe.Register();
        }
    }

}
