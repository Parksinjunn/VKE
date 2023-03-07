using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.AimedWeapons
{
    public class SorcerersSarukh : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sorcerers Sarukh");
            Tooltip.SetDefault("A wand possessing the power of the sorcerers of old\nChanneled knives that follow the player's cursor");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 23;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 14;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(0,8,0,0);
            Item.rare = 2;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.shoot = Mod.Find<ModProjectile>("SorcerersSarukhProj").Type;
            Item.shootSpeed = 15f;
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.MagicMissile, 3);
        //    recipe.AddIngredient(Mod.GetItem("ManaKnivesAnimated"), 1);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.MagicMissile, 2);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.MagicMissile, 1);
        //    recipe.AddIngredient(ItemID.Sapphire, 4);
        //    recipe.AddIngredient(Mod.GetItem("ManaKnivesAnimated"), 1);
        //    recipe.AddIngredient(Mod.GetItem("CorruptionCrystal"), 2);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.MagicMissile, 1);
        //    recipe.AddIngredient(ItemID.Sapphire, 2);
        //    recipe.AddIngredient(Mod.GetItem("CorruptionCrystal"), 1);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();
        //}
    }
}
