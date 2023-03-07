using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.AimedWeapons
{
    public class TrueDemonsScourge : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Demon's Scourge");
            Tooltip.SetDefault("An aimed whip possessing the power of a true demon\nChanneled knives that follow the player's cursor");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 20;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(0,10,0,0);
            Item.rare = 3;  
          
 
            Item.UseSound = SoundID.Item39;
            Item.shoot = Mod.Find<ModProjectile>("TrueDemonsScourgeProj").Type;
            Item.shootSpeed = 15f;
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.Flamelash, 3);
        //    recipe.AddIngredient(Mod.GetItem("ManaKnivesAnimated"), 1);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.Flamelash, 2);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.Flamelash, 1);
        //    recipe.AddIngredient(ItemID.Ruby, 4);
        //    recipe.AddIngredient(Mod.GetItem("ManaKnivesAnimated"), 1);
        //    recipe.AddIngredient(Mod.GetItem("CorruptionCrystal"), 2);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.Flamelash, 1);
        //    recipe.AddIngredient(ItemID.Ruby, 2);
        //    recipe.AddIngredient(Mod.GetItem("CorruptionCrystal"), 1);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.Flamelash, 1);
        //    recipe.AddIngredient(ItemID.Ruby, 4);
        //    recipe.AddIngredient(Mod.GetItem("SorcerersSarukh"), 1);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.Flamelash, 1);
        //    recipe.AddIngredient(ItemID.Ruby, 2);
        //    recipe.AddIngredient(Mod.GetItem("SorcerersSarukh"), 1);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();
        //}
    }
}
