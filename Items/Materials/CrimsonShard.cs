using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials
{
    public class CrimsonShard : ModItem
    {
        int stack;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Shard");
            Tooltip.SetDefault("Parts to a bigger picture\nAutomatically combine into crystals");
        }

        //public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        //{
        //    if (stack == 1)
        //    {
        //        return true;
        //    }
        //    if(stack == 2)
        //    {
        //        Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/CrimsonShard2").Value;
        //        spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
        //        return false;
        //    }
        //    if(stack == 3)
        //    {
        //        Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/CrimsonShard3").Value;
        //        spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
        //        return false; 
        //    }
        //    if(stack == 4)
        //    {
        //        Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/CrimsonShard4").Value;
        //        spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
        //        return false;
        //    }
        //    if (stack == 5)
        //    {
        //        Item.NewItem(null, Main.LocalPlayer.getRect(), ModContent.ItemType<CrimsonCrystal>());

        //        return false;
        //    }
        //    else
        //        return true;
        //}
        public override void UpdateInventory(Player player)
        {
            stack = Item.stack;
            if (Item.stack == 5)
            {
                Item.SetDefaults(ModContent.ItemType<CrimsonCrystal>());
            }
            base.UpdateInventory(player);
        }
        public override void SetDefaults()
        {
            Item.maxStack = 5;
            Item.value = 1000;
            Item.rare = 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CorruptionShard>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}