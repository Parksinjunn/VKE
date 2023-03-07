using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.AimedWeapons
{
    public class PrismaticArcanum : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Arcanum");
            Tooltip.SetDefault("Magic in it's purest form\nChanneled knives that follow the player's cursor");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 49;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 22;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(0,20,0,0);
            Item.rare = 5;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.shoot = Mod.Find<ModProjectile>("PrismaticArcanumProj").Type;
            Item.shootSpeed = 15f;
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.RainbowRod, 3);
        //    recipe.AddIngredient(Mod.GetItem("ManaKnivesAnimated"), 1);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.RainbowRod, 2);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.RainbowRod, 1);
        //    recipe.AddIngredient(ItemID.Amethyst, 2);
        //    recipe.AddIngredient(ItemID.Ruby, 2);
        //    recipe.AddIngredient(ItemID.Sapphire, 2);
        //    recipe.AddIngredient(ItemID.Amber, 2);
        //    recipe.AddIngredient(ItemID.Diamond, 2);
        //    recipe.AddIngredient(ItemID.Emerald, 2);
        //    recipe.AddIngredient(Mod.GetItem("ManaKnivesAnimated"), 1);
        //    recipe.AddIngredient(Mod.GetItem("CorruptionCrystal"), 2);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.RainbowRod, 1);
        //    recipe.AddIngredient(ItemID.Amethyst, 1);
        //    recipe.AddIngredient(ItemID.Ruby, 1);
        //    recipe.AddIngredient(ItemID.Sapphire, 1);
        //    recipe.AddIngredient(ItemID.Amber, 1);
        //    recipe.AddIngredient(ItemID.Diamond, 1);
        //    recipe.AddIngredient(ItemID.Emerald, 1);
        //    recipe.AddIngredient(Mod.GetItem("CorruptionCrystal"), 1);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.RainbowRod, 1);
        //    recipe.AddIngredient(ItemID.Amethyst, 2);
        //    recipe.AddIngredient(ItemID.Ruby, 2);
        //    recipe.AddIngredient(ItemID.Amber, 2);
        //    recipe.AddIngredient(ItemID.Diamond, 2);
        //    recipe.AddIngredient(ItemID.Emerald, 2);
        //    recipe.AddIngredient(Mod.GetItem("SorcerersSarukh"), 1);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.RainbowRod, 1);
        //    recipe.AddIngredient(ItemID.Amethyst, 1);
        //    recipe.AddIngredient(ItemID.Ruby, 1);
        //    recipe.AddIngredient(ItemID.Amber, 1);
        //    recipe.AddIngredient(ItemID.Diamond, 1);
        //    recipe.AddIngredient(ItemID.Emerald, 1);
        //    recipe.AddIngredient(Mod.GetItem("SorcerersSarukh"), 1);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.RainbowRod, 1);
        //    recipe.AddIngredient(ItemID.Amethyst, 2);
        //    recipe.AddIngredient(ItemID.Amber, 2);
        //    recipe.AddIngredient(ItemID.Diamond, 2);
        //    recipe.AddIngredient(ItemID.Emerald, 2);
        //    recipe.AddIngredient(Mod.GetItem("TrueDemonsScourge"), 1);
        //    recipe.AddTile(TileID.Furnaces);
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.RainbowRod, 1);
        //    recipe.AddIngredient(ItemID.Amethyst, 1);
        //    recipe.AddIngredient(ItemID.Amber, 1);
        //    recipe.AddIngredient(ItemID.Diamond, 1);
        //    recipe.AddIngredient(ItemID.Emerald, 1);
        //    recipe.AddIngredient(Mod.GetItem("TrueDemonsScourge"), 1);
        //    recipe.AddTile(Mod.GetTile("VampTableTile"));
        //    recipe.Register();
        //}
    }
}
