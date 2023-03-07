using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.HardKnives
{
    public class TerraKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Blades");
            Tooltip.SetDefault("Upon collision these blades become ethereal for a short while");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 62; 
            Item.width = 38;
            Item.height = 38;
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
            Item.shoot = Mod.Find<ModProjectile>("TerraKnivesProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TrueExcaliburKnives>());
            recipe.AddIngredient(ModContent.ItemType<TrueShadowKnives>());
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TrueExcaliburKnives>());
            recipe.AddIngredient(ModContent.ItemType<TrueShadowKnives>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
