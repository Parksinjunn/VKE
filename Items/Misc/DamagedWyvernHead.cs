using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VKE.Items.Misc
{
    public class DamagedWyvernHead : ModItem
    {
        int stack;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Damaged Wyvern Head");
            //Tooltip.SetDefault("");
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (stack == 1)
            {
                return true;
            }
            if (stack == 2)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Items/Misc/DamagedWyvernHead1").Value;
                spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
                return false;
            }
            if (stack == 3)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Items/Misc/DamagedWyvernHead2").Value;
                spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
                return false;
            }
            if (stack == 4)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Items/Misc/DamagedWyvernHead3").Value;
                spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
                return false;
            }
            if(stack >= 5)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Items/Misc/DamagedWyvernHead4").Value;
                spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
                return false;
            }
            else
                return true;
        }
        public override void UpdateInventory(Player player)
        {
            stack = Item.stack;
            base.UpdateInventory(player);
        }
        public override void SetDefaults()
        {
            Item.maxStack = 30;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 11;
        }
    }
}