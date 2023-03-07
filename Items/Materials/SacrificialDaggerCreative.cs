using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials
{
    public class SacrificialDaggerCreative : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unlimited Power");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.maxStack = 1;
            Item.rare = 1;
            Item.value = Item.buyPrice(gold: 1);
            Item.consumable = false;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.GetModPlayer<VampPlayer>().BloodPoints += Main.rand.Next(150, 225);
            Item.consumable = false;
            return true;
        }
        //public override void AddRecipes()
        //{
        //    ModRecipe recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.BottledWater,2);
        //    recipe.AddIngredient(ItemID.TissueSample, 4);
        //    recipe.AddIngredient(ItemID.Hemopiranha);
        //    recipe.AddTile(mod.GetTile("KnifeBench"));
        //    recipe.SetResult(this, 2);
        //                recipe.Register();
        //    recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.BottledWater, 3);
        //    recipe.AddIngredient(ItemID.ShadowScale, 6);
        //    recipe.AddIngredient(ItemID.Ebonkoi);
        //    recipe.AddTile(mod.GetTile("KnifeBench"));
        //    recipe.SetResult(this, 3);
        //                recipe.Register();

        //    recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.BottledWater, 2);
        //    recipe.AddIngredient(ItemID.TissueSample, 4);
        //    recipe.AddIngredient(ItemID.Hemopiranha);
        //    recipe.AddTile(mod.GetTile("VampTableTile"));
        //    recipe.SetResult(this, 3);
        //                recipe.Register();
        //    recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.BottledWater, 3);
        //    recipe.AddIngredient(ItemID.ShadowScale, 6);
        //    recipe.AddIngredient(ItemID.Ebonkoi);
        //    recipe.AddTile(mod.GetTile("VampTableTile"));
        //    recipe.SetResult(this, 4);
        //                recipe.Register();
        //}
    }
}
