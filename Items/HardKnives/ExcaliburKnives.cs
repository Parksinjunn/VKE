using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.HardKnives
{
    public class ExcaliburKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arthur's Kaledvoulc'h");
            Tooltip.SetDefault("High-Calibur Knives");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 42; 
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 8;    
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ExcaliburProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Excalibur);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Excalibur);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
