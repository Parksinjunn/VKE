using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class ShadowKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Penumbra");
            Tooltip.SetDefault("Very Dark\nThe knives fall to the ground and bounce back to the user");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 14; 
            
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0,10,40,10); 
            Item.rare = 11;
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ShadowProj").Type;
            Item.shootSpeed = 20f;
            NumProj = 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LightandDark>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FieryKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<JungleKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SengosForgottenBlades>(), 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LightandDark>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FieryKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<JungleKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SengosForgottenBlades>(), 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LightandDark>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FieryKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<JungleKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SengosForgottenBlades>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LightandDark>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FieryKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<JungleKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SengosForgottenBlades>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
