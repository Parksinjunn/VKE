using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class ButchersKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Butcher's Knives");
            Tooltip.SetDefault("A set of rusty knives that stick into their target and apply bleeding");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 15; 
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0,0, 50,0); 
            Item.rare = 10;  
          
 
            Item.UseSound = SoundID.NPCHit4;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ButchersKnivesProj").Type;
            Item.shootSpeed = 24f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
            recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddIngredient(ItemID.Vertebrae, 3);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
