using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace VKE.Tiles
{
    public class VampTableTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.Width = 9;
            TileObjectData.newTile.Height = 7;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(4, 6);
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Vampire Altar\nRight-Click to upgrade your knives");
            AddMapEntry(new Color(200, 200, 200), name);
            AdjTiles = new int[] { TileID.WorkBenches };

            AnimationFrameHeight = 126;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX == 72 && tile.TileFrameY == 54)
            {
                r = 0.40f;
                g = 0.0f;
                b = 0.0f;
            }
        }

        public int frameCount;
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 4)
            {
                frameCounter = 0;
                frame++;
                frameCount++;
                if (frame > 14)
                {
                    frame = 0;
                    frameCount = 0;
                }
            }
        }
        public int tileX;
        public int tileY;
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            tileX = Main.tile[i, j].TileFrameX;
            tileY = Main.tile[i, j].TileFrameY;
            if (frameCount > 12)
            {
                if (tileX == 54 && tileY == 54)
                {
                    int DustID2 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, -6, -6, 10, Color.Red, 0.5f);
                    Main.dust[DustID2].fadeIn = 1.05f;
                    int DustID3 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, -7, -9, 10, Color.Red, 0.5f);
                    Main.dust[DustID3].fadeIn = 1.05f;
                    int DustID4 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, -6, -3, 10, Color.Red, 0.5f);
                    Main.dust[DustID4].fadeIn = 1.05f;
                    int DustID5 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 224, -8, -4, 10, Color.Red, 1f);
                    Main.dust[DustID5].fadeIn = 1.05f;
                    Main.dust[DustID2].noGravity = true;
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID4].noGravity = true;
                    Main.dust[DustID5].noGravity = true;
                    Main.dust[DustID5].shader = GameShaders.Armor.GetSecondaryShader(58, Main.LocalPlayer);
                }
                if (tileX == 108 && tileY == 54)
                {

                    int DustID2 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, 6, -6, 10, Color.Red, 0.5f);
                    Main.dust[DustID2].fadeIn = 1.05f;
                    int DustID3 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, 7, -8, 10, Color.Red, 0.5f);
                    Main.dust[DustID3].fadeIn = 1.05f;
                    int DustID4 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, 6, -3, 10, Color.Red, 0.5f);
                    Main.dust[DustID4].fadeIn = 1.05f;
                    int DustID5 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 224, 8, -4, 10, Color.Red, 1f);
                    Main.dust[DustID5].fadeIn = 1.05f;
                    Main.dust[DustID2].noGravity = true;
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID4].noGravity = true;
                    Main.dust[DustID5].noGravity = true;
                    Main.dust[DustID5].shader = GameShaders.Armor.GetSecondaryShader(58, Main.LocalPlayer);
                }
                if (tileX == 54 && tileY == 90)
                {
                    int DustID2 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, -8, 0.5f, 10, Color.Red, 0.5f);
                    Main.dust[DustID2].fadeIn = 1.05f;
                    int DustID3 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, -8, 0.5f, 10, Color.Red, 0.5f);
                    Main.dust[DustID3].fadeIn = 1.05f;
                    int DustID4 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, -8, 0.5f, 10, Color.Red, 0.5f);
                    Main.dust[DustID4].fadeIn = 1.05f;
                    int DustID5 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 224, -8, 0.5f, 10, Color.Red, 1f);
                    Main.dust[DustID5].fadeIn = 1.05f;
                    Main.dust[DustID2].noGravity = true;
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID4].noGravity = true;
                    Main.dust[DustID5].noGravity = true;
                    Main.dust[DustID5].shader = GameShaders.Armor.GetSecondaryShader(58, Main.LocalPlayer);
                }
                if (tileX == 108 && tileY == 90)
                {
                    int DustID2 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, 8, 0.5f, 10, Color.Red, 0.5f);
                    Main.dust[DustID2].fadeIn = 1.05f;
                    int DustID3 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, 8, 0.5f, 10, Color.Red, 0.5f);
                    Main.dust[DustID3].fadeIn = 1.05f;
                    int DustID4 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 73, 8, 0.5f, 10, Color.Red, 0.5f);
                    Main.dust[DustID4].fadeIn = 1.05f;
                    int DustID5 = Dust.NewDust(new Vector2(i, j) * 16, 0, 21, 224, 8, 0.5f, 10, Color.Red, 1f);
                    Main.dust[DustID5].fadeIn = 1.05f;
                    Main.dust[DustID2].noGravity = true;
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID4].noGravity = true;
                    Main.dust[DustID5].noGravity = true;
                    Main.dust[DustID5].shader = GameShaders.Armor.GetSecondaryShader(58, Main.LocalPlayer);
                }
            }
            return true;
        }

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 48, 32, ModContent.ItemType<Items.Tiles.VampTable>());
        }
        //public override bool RightClick(int i, int j)
        //{
        //    if (UpgradePanel.visible)
        //    {
        //        Main.playerInventory = false;
        //        VKE.CloseUpgradeUI();
        //        UpgradePanel.visible = false;
        //        SoundEngine.PlaySound(SoundID.MenuClose);
        //    }
        //    else if (UpgradePanel.visible == false)
        //    {
        //        Main.playerInventory = true;
        //        VKE.OpenUpgradeUI();
        //        UpgradePanel.visible = true;
        //        SoundEngine.PlaySound(SoundID.MenuOpen);
        //    }
        //    return true;
        //}

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile.TileFrameX % 36 != 0)
            {
                left--;
            }
            if (tile.TileFrameY != 0)
            {
                top--;
            }
            int chest = Chest.FindChest(left, top);
            player.cursorItemIconID = -1;
            if (chest < 0)
            {
                player.cursorItemIconText = "Vampire Altar\nRight-Click to upgrade your knives";
            }
            else
            {
                player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Vampire Altar\nRight-Click to upgrade your knives";
                if (player.cursorItemIconText == "Vampire Altar\nRight-Click to upgrade your knives")
                {
                    player.cursorItemIconID = ModContent.ItemType<Items.Tiles.VampTable>();
                    player.cursorItemIconText = "";
                }
            }
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
        }

        public override void MouseOverFar(int i, int j)
        {
            MouseOver(i, j);
            Player player = Main.LocalPlayer;
            if (player.cursorItemIconText == "")
            {
                player.cursorItemIconEnabled = false;
                player.cursorItemIconID = 0;
            }
        }
    }
}