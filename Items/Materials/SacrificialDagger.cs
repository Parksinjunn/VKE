using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Buffs;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class SacrificialDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Sacrifice a bit of your own blood to get a bit of bp instantly\n[c/B48C8C:Disclaimer: This kind of behavior is not endorsed]\n[c/B48C8C:this is a game not real life]\n[c/B48C8C:If you have any thoughts of self-harm or suicide]\n[c/B48C8C:please talk to a loved one you trust or dial this number: 1-800-273-8255 to talk to someone who will listen]\n[c/B48C8C:You may feel alone, but I urge you to keep fighting, there's a reason you're here]\n[c/B48C8C:when you think you have done nothing worthwile, just remember that by downloading this mod you've made my day]\n[c/B48C8C:knowing that think of how many other people's days you've made better just by living]\n[c/B48C8C:we all make mistakes, and yours aren't any greater than mine]\n[c/B48C8C:we're all just hunks of carbon in the end, so make the most of your time alive]\n[c/B48C8C:you're a warrior, by the power of grayskull, you have the power!]");
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
            if(!player.HasBuff(ModContent.BuffType<BleedingOutDebuff>()))
            {
                player.AddBuff(ModContent.BuffType<BleedingOutDebuff>(),18000);
                player.GetModPlayer<VampPlayer>().BloodPoints += Main.rand.Next(50, 75);
                Item.consumable = false;
            }
            else
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 50, player.width, player.height), new Color(255, 0, 0, 255), "You are still recovering!", true);
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 6);
            recipe.AddIngredient(ItemID.HallowedBar, 4);
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 4);
            recipe.AddIngredient(ItemID.HallowedBar, 3);
            recipe.AddIngredient(ItemID.GoldBar, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
