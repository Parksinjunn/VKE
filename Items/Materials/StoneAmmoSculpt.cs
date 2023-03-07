using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using VKE.Tiles;

namespace VKE.Items.Materials
{
    public class StoneAmmoSculpt : ModItem
    {
        int stack;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sculpted knife ammo");
            Tooltip.SetDefault("Combining five of these sculpts will producea set of knife sculpts fit for a cast");
        }
        public override void HoldItem(Player player)
        {
            stack = Item.stack;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (stack == 1)
            {
                return true;
            }
            if (stack == 2)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/StoneAmmoSculpt2").Value;
                spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
                return false;
            }
            if (stack == 3)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/StoneAmmoSculpt3").Value;
                spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
                return false;
            }
            if (stack == 4)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Items/Materials/StoneAmmoSculpt4").Value;
                spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
                return false;
            }
            if (stack >= 5)
            {
                Item.NewItem(null, Main.LocalPlayer.getRect(), ModContent.ItemType<StoneAmmoSculptComplete>());

                return false;
            }
            else
                return true;
        }
        public override void UpdateInventory(Player player)
        {
            stack = Item.stack;
            if (Item.stack == 5)
            {
                Item.SetDefaults(ModContent.ItemType<StoneAmmoSculptComplete>());
            }
            base.UpdateInventory(player);
        }
        public override void SetDefaults()
        {
            Item.maxStack = 5;
            Item.value = Item.sellPrice(0,0,0,0);
            Item.rare = -1;
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.StoneBlock, 6);
        //    recipe.AddTile(ModContent.TileType<KnifeBench>());
        //    recipe.Register();

        //    recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.StoneBlock, 4);
        //    recipe.AddTile(ModContent.TileType<VampTableTile>());
        //    recipe.Register();
        //}
    }
}