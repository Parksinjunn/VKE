using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Items.Materials;
using VKE.Items.PreKnives;
using VKE.Tiles;

namespace VKE.Items.HardKnives
{
    public class TrueShadowKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Umbra");
            Tooltip.SetDefault("Truly very dark\nThese ethereal blades pass through all objects");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 43; 
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = -12;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("TrueShadowProj").Type;
            Item.shootSpeed = 15f;
            NumProj = 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BrokenHeroKnives>());
            recipe.AddIngredient(ModContent.ItemType<ShadowKnives>());
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BrokenHeroKnives>());
            recipe.AddIngredient(ModContent.ItemType<ShadowKnives>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
