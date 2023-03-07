using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Items.Materials;
using VKE.Tiles;

namespace VKE.Items.HardKnives
{
    public class TrueExcaliburKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arthur's True Kaledvoulc'h");
            Tooltip.SetDefault("Truly High-Calibur Knives\nUpon collision these blades become ethereal for a short while");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 52;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = -12;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("TrueExcaliburProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BrokenHeroKnives>());
            recipe.AddIngredient(ModContent.ItemType<ExcaliburKnives>());
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BrokenHeroKnives>());
            recipe.AddIngredient(ModContent.ItemType<ExcaliburKnives>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
