using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.Ammo;
using VKE.Tiles;

namespace VKE.Items.MaterialKnives
{
    public class ReaverHead : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaver Head");
            Tooltip.SetDefault("Launches Teeth");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 8;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.shoot = Mod.Find<ModProjectile>("ToothProj").Type;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
            Item.useAmmo = ModContent.ItemType<ThrowingTeeth>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ReaverShark, 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ReaverShark, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
