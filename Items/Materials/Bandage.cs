using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Buffs;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class Bandage : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Stops the bleeding from a sacrificial cut");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 34;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.maxStack = 99;
            Item.rare = 1;
            //item.value = Item.buyPrice(gold: 1);
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<BleedingOutDebuff>()))
            {
                return true;
            }
            else
                return false;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (!player.HasBuff(ModContent.BuffType<BandageBuff>()))
            {
                player.AddBuff(ModContent.BuffType<BandageBuff>(), 18000);
                Item.consumable = true;
            }
            else
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 50, player.width, player.height), new Color(255, 0, 0, 255), "You already used a bandage!", true);
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();
            recipe = CreateRecipe(4);
            recipe.AddIngredient(ItemID.Silk, 3);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe(2);
            recipe.AddIngredient(ModContent.ItemType<Materials.PlanteraCloth>(), 5);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();
            recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<Materials.PlanteraCloth>(), 3);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
