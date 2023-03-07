using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class SengosForgottenBlades : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sengos Forgotten Blades");
            Tooltip.SetDefault("Forged with starlight by Muramasa himself");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 10; 
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0,0,43,55); 
            Item.rare = 1;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SengosForgottenProj").Type;
            Item.shootSpeed = 15f;
            NumProj = 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.AddIngredient(ItemID.GoldenKey, 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.Bone, 15);
            recipe.AddIngredient(ItemID.GoldenKey, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
