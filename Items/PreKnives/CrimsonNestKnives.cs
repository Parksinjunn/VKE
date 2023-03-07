using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class CrimsonNestKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Nest");
            Tooltip.SetDefault("Nest filled with larval crimera");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 16; 
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item39; 
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("CrimsonNestKnivesProj").Type;
            Item.shootSpeed = 12f;
            Item.scale = 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bladetongue);
            recipe.AddIngredient(ItemID.CrimtaneBar, 12);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bladetongue);
            recipe.AddIngredient(ItemID.CrimtaneBar, 7);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
