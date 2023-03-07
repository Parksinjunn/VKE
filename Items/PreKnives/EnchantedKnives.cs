using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class EnchantedKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Knives");
            Tooltip.SetDefault("");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 16; 
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 4.25f;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 2;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("EnchantedKnivesProj").Type;
            Item.shootSpeed = 12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddIngredient(ModContent.ItemType<WeakVampireKnives>());
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddIngredient(ModContent.ItemType<WeakVampireKnives>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
