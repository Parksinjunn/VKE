using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VKE.Items.Materials
{
    public class CorruptionShard : ModItem
    {
        int stack;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corruption Shard");
            Tooltip.SetDefault("Parts to a bigger picture\nAutomatically combine into crystals");
        }
        //public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        //{
        //    if (stack == 1)
        //    {
        //        return true;
        //    }
        //    if (stack == 2)
        //    {
        //        Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/CorruptionShard2").Value;
        //        spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
        //        return false;
        //    }
        //    if (stack == 3)
        //    {
        //        Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/CorruptionShard3").Value;
        //        spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
        //        return false;
        //    }
        //    if (stack == 4)
        //    {
        //        Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/CorruptionShard4").Value;
        //        spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
        //        return false;
        //    }
        //    if (stack == 5)
        //    {
        //        Item.NewItem(null, Main.LocalPlayer.getRect(), ModContent.ItemType<CorruptionCrystal>());

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
                Item.SetDefaults(ModContent.ItemType<CorruptionCrystal>());
            }
            base.UpdateInventory(player);
        }
        public override void SetDefaults()
        {
            Item.maxStack = 5;
            Item.value = Item.sellPrice(0,0,10,0);
            Item.rare = 11;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CrimsonShard>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}