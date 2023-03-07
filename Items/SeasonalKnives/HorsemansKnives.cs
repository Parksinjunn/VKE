using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.SeasonalKnives
{
    public class HorsemansKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Horsemans Blades");
            Tooltip.SetDefault("Blades that stick into the ground and randomly spawn pumpkins that home in on enemies");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 28; 
            Item.width = 68;
            Item.height = 68;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 4.25f;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.rare = 8;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("HorsemansKnivesProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TheHorsemansBlade, 1);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TheHorsemansBlade, 1);
            recipe.AddIngredient(ItemID.SpookyWood, 132);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();


            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TheHorsemansBlade, 1);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TheHorsemansBlade, 1);
            recipe.AddIngredient(ItemID.LunarBar, 78);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
