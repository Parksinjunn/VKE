using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using VKE;
using VKE.Items;
using VKE.Items.Tiles;
using static Terraria.ModLoader.ModContent;

namespace vke.Tiles
{
    public class BloodAltar : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileSolid[Type] = true;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<BloodAltarTE>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Vampire Altar\nRight-Click to upgrade your knives");
            TileID.Sets.DisableSmartCursor[Type] = true;

            AnimationFrameHeight = 18;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        internal static BloodAltarTE AltarTE = new BloodAltarTE();
        public override void PlaceInWorld(int i, int j, Item item)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                //AltarTE.RitualOwner = Main.LocalPlayer.whoAmI;
                //AltarTE.SyncOwnerSend();

                //VampPlayer p = Main.player[item.playerIndexTheItemIsReservedFor].GetModPlayer<VampPlayer>();
                //p.SendPackage = true;
            }
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (Main.player[0].HeldItem.type == ItemID.CopperPickaxe)
            {
                blockDamaged = true;
                return true;
            }
            else
            {
                blockDamaged = false;
                return false;
            }
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
            if (frameCounter > 6)
            {
                frameCounter = 0;
                frame++;
                frameCount++;
                if (frame > 4)
                {
                    frame = 0;
                    frameCount = 0;
                }
            }
        }

        public int tileX;
        public int tileY;
        int Offset;
        short CalculatedOffset;
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            if (p.BloodPoints >= 2000)
            {
                Offset = 360;
            }
            else if (p.BloodPoints >= 1500)
            {
                Offset = 270;
            }
            else if (p.BloodPoints >= 1000)
            {
                Offset = 180;
            }
            else if (p.BloodPoints >= 500)
            {
                Offset = 90;
            }
            else if (p.BloodPoints < 500)
            {
                Offset = 0;
            }
                CalculatedOffset = (short)(Offset);
            Main.tile[i, j].TileFrameY = CalculatedOffset;
            return true;
        }
        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 32, 16, ModContent.ItemType<BloodAltarItem>());
            Point16 AltarPos = TEPosition(x, y);
            AltarPos = AltarPos - new Point16(1, 1);
            //BloodAltarTE AltarTE = ModContent.TileEntityType<BloodAltarTE>();
            //if (!AltarTE.BloodCrystal.IsAir)
            //{
            //    Item CrystalDrop = Main.item[Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Misc.BloodCrystalSoul>())];
            //    CrystalDrop.GetGlobalItem<Items.Misc.BloodCrystalInstanced>().NPCID = AltarTE.BloodCrystal.GetGlobalItem<Items.Misc.BloodCrystalInstanced>().NPCID;
            //    CrystalDrop.GetGlobalItem<Items.Misc.BloodCrystalInstanced>().NPCName = AltarTE.BloodCrystal.GetGlobalItem<Items.Misc.BloodCrystalInstanced>().NPCName;
            //}
            GetInstance<BloodAltarTE>().Kill(AltarPos.X, AltarPos.Y);
            WorldGen.KillTile(AltarPos.X + 1, AltarPos.Y - 2, false, false, false);
            WorldGen.KillTile(AltarPos.X + 1, AltarPos.Y - 1, false, false, false);
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)AltarPos.X + 1, (float)AltarPos.Y - 1, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)AltarPos.X + 1, (float)AltarPos.Y - 2, 0f, 0, 0, 0);
            }
        }

        public override bool RightClick(int i, int j)
        {
            Main.mouseRightRelease = false;
            //Main.NewText("Click");
            Point16 AltarPos = TEPosition(i, j);
            AltarPos = AltarPos - new Point16(1,1);
            //Main.NewText("Pos: " + AltarPos);
            //BloodAltarTE AltarTE = Mod.GetTileEntity<BloodAltarTE>(AltarPos);
            //Main.NewText("Position: " + AltarTE.Position);
            if (AltarTE == null)
            {
                //Main.NewText("Null");
                return false;
            }

            Player player = Main.LocalPlayer;
            VampPlayer vampPlayer = player.GetModPlayer<VampPlayer>();
            //if (AltarTE.RitualOwner >= byte.MaxValue)
            //{
                AltarTE.RitualOwner = Main.LocalPlayer.whoAmI;
            //}
            if (AltarTE.CurrentPlayer == player.whoAmI)
            {
                AltarTE.CloseUI();
            }
            else
            {
                AltarTE.CurrentPlayer = (byte)player.whoAmI;
                AltarTE.OpenUI();
            }
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ItemType<BloodAltarItem>();
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
        private Point16 TEPosition(int i, int j) => new Point16(i - Main.tile[i, j].TileFrameX / 18 + 1, j - Main.tile[i, j].TileFrameY % AnimationFrameHeight / 18 + 1);

    }
}