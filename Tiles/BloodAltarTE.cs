using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using vke.Projectiles.BloodAltarProj;
using vke.UI;
using VKE;
using VKE.Items.Misc;
using VKE.Tiles;

namespace vke.Tiles
{
    public class BloodAltarTE : ModTileEntity
    {
        public bool RitualOfTheStone;
        public ushort RoSType;
        public bool RitualOfTheMiner;
        public ushort RoMinType;
        public bool RitualOfMidas;
        public short RoMidType;
        public bool RitualOfSouls;
        public int RoSoType;
        public int RoSoDelay;
        public int RitualOwner;
        public int CurrentPlayer;
        public int DustType;
        public int DustTimer;
        int PointCost;
        bool Cost;
        public Item BloodCrystal;

        public BloodAltarTE()
        {
            RitualOfTheStone = false;
            RoSType = 1;
            RitualOfTheMiner = false;
            RoMinType = 7;
            RitualOfMidas = false;
            RoMidType = 71;
            RitualOfSouls = false;
            RoSoType = -69;
            RoSoDelay = 600;
            RitualOwner = 255;
            CurrentPlayer = 255;
            DustType = 43;
            DustTimer = 12;
            BloodCrystal = new Item();
        }
        float StoreDisPlayer;
        int StorePlayer;
        public override void Update()
        {
            NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, ID);

            if ((Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints < PointCost/* && !HasBeenWarned*/) && (RitualOfTheStone || RitualOfTheMiner || RitualOfMidas || RitualOfSouls))
            {
                RitualOfTheStone = false;
                RitualOfTheMiner = false;
                RitualOfMidas = false;
                RitualOfSouls = false;
                RoSoType = -69;
                SendStoneRitualInfo();
                //SendMinerRitualInfo();
                //SendMidasRitualInfo();
                //HasBeenWarned = true;
            }
            //if (HasBeenWarned && Main.player[AltarTE.RitualOwner].GetModPlayer<VampPlayer>().BloodPoints >= Cost)
            //{
            //    HasBeenWarned = false;
            //}
            //SyncBloodPointsSend();
            if (RitualOfTheStone == true && RitualOwner != byte.MaxValue && !RitualOfTheMiner && !RitualOfMidas && !RitualOfSouls)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                    StoneRitual(Position.X, Position.Y, Main.player[RitualOwner], RoSType);
                if (Main.netMode == NetmodeID.Server && Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints >= PointCost)
                {
                    if (Main.tile[Position.X + 1, Position.Y - 2].TileType != RoSType)
                    {
                        StoneRitual(Position.X, Position.Y, Main.player[RitualOwner], RoSType);
                        if(RoSType != TileID.Mud && Cost)
                        {
                            Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints -= PointCost;
                            //SendCost();
                        }
                        else
                        {

                        }
                    }
                }
            }
            else if (!RitualOfTheStone && RitualOwner != byte.MaxValue && RitualOfTheMiner == true && !RitualOfMidas && !RitualOfSouls)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                    MinerRitual(Position.X, Position.Y, Main.player[RitualOwner], RoMinType);
                if (Main.netMode == NetmodeID.Server && Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints >= PointCost)
                {
                    if (Main.tile[Position.X + 1, Position.Y - 2].TileType != RoMinType)
                    {
                        MinerRitual(Position.X, Position.Y, Main.player[RitualOwner], RoMinType);
                        Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints -= PointCost;
                        //SendCost();
                    }
                }
            }
            else if (!RitualOfTheStone && RitualOwner != byte.MaxValue && !RitualOfTheMiner && RitualOfMidas == true && !RitualOfSouls)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                    MidasRitual(Position.X, Position.Y, Main.player[RitualOwner], RoMidType);
                if (Main.netMode == NetmodeID.Server && Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints >= PointCost)
                {
                    MidasRitual(Position.X, Position.Y, Main.player[RitualOwner], RoMidType);
                    Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints -= PointCost;
                    //SendCost();
                }
            }
            else if (!RitualOfTheStone && RitualOwner != byte.MaxValue && !RitualOfTheMiner && !RitualOfMidas && RitualOfSouls == true)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                    SoulsRitual(Position.X, Position.Y, Main.player[RitualOwner], RoSoType, RoSoDelay);
                if (Main.netMode == NetmodeID.Server && Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints >= PointCost)
                {
                    SoulsRitual(Position.X, Position.Y, Main.player[RitualOwner], RoSoType, RoSoDelay);
                }
            }
            else
            {

            }
            for(int x = 0; x < Main.maxItems; x++)
            {
                if(Main.item[x].type == ModContent.ItemType<DamagedWyvernHead>() && Main.item[x].active)
                {
                    float DisItemX = (16f * Position.X) - Main.item[x].position.X;
                    float DisItemY = (16f * Position.Y) - Main.item[x].position.Y;
                    float DisItem = (float)Math.Sqrt((double)(DisItemX * DisItemX + DisItemY * DisItemY));
                    if(DisItem < 75f)
                    {
                        int Value = 0;
                        if (Main.item[x].type == ModContent.ItemType<DamagedWyvernHead>())
                        {
                            Value = 35;
                        }
                        for (int y = 0; y < Main.CurrentFrameFlags.ActivePlayersCount; y++)
                        {
                            float DisPlayerX = (16f * Position.X) - Main.player[y].position.X;
                            float DisPlayerY = (16f * Position.Y) - Main.player[y].position.Y;
                            float DisPlayer = (float)Math.Sqrt((double)(DisPlayerX * DisPlayerX + DisPlayerY * DisPlayerY));
                            if (DisPlayer < StoreDisPlayer || StoreDisPlayer == 0f)
                            {
                                StoreDisPlayer = DisPlayer;
                                StorePlayer = y;
                            }
                        }
                        Main.player[StorePlayer].GetModPlayer<VampPlayer>().BloodPoints += Value * Main.item[x].stack;
                        Main.item[x].active = false;
                        Main.item[x].TurnToAir();
                    }
                }
            }
        }

        public void SummonBloodCrystalProj()
        {
            int i = Projectile.NewProjectile(new EntitySource_TileEntity(this), new Vector2((Position.X * 16f) + 24, Position.Y * 16f), new Vector2(0, -0.4f), ModContent.ProjectileType<BloodCrystalProj>(), 0, 0, Main.player[RitualOwner].whoAmI, RoSoType);
            Main.projectile[i].ai[0] = RoSoType;
        }
        public void SendStoneRitualInfo()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                return;
            }
            ModPacket packet = Mod.GetPacket();
            packet.Write(VampKnives.StoneRitualRecieveMPClient);
            packet.Write(ID);
            packet.Write(RitualOfTheStone);
            packet.Write(RitualOwner);
            packet.Write(RoSType);
            TileEntity.Write(packet, TileEntity.ByID[ID], true);
            packet.Send();
        }
        //public void SendMinerRitualInfo()
        //{
        //    if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        return;
        //    }
        //    ModPacket packet = Mod.GetPacket();
        //    packet.Write(VampKnives.MinerRitualRecieveMPClient);
        //    packet.Write(ID);
        //    packet.Write(RitualOfTheMiner);
        //    packet.Write(RitualOwner);
        //    packet.Write(RoMinType);
        //    packet.Send();
        //}
        //public void SendMidasRitualInfo()
        //{
        //    if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        return;
        //    }
        //    ModPacket packet = Mod.GetPacket();
        //    packet.Write(VampKnives.MidasRitualRecieveMPClient);
        //    packet.Write(ID);
        //    packet.Write(RitualOfMidas);
        //    packet.Write(RitualOwner);
        //    packet.Write(RoMidType);
        //    packet.Send();
        //}
        //public void SendSoulsRitualInfo()
        //{
        //    if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        return;
        //    }
        //    ModPacket packet = Mod.GetPacket();
        //    packet.Write(VampKnives.SoulsRitualRecieveMPClient);
        //    packet.Write(ID);
        //    packet.Write(RitualOfSouls);
        //    packet.Write(RitualOwner);
        //    packet.Write(RoSoType);
        //    packet.Write(RoSoDelay);
        //    //ItemIO.Send(BloodCrystal, packet, true);
        //    //packet.Write(BloodCrystal);
        //    packet.Send();
        //}
        //public void SendCost()
        //{
        //    if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        return;
        //    }
        //    ModPacket StoneRitualSend = Mod.GetPacket();
        //    StoneRitualSend.Write(VampKnives.RitualCostRecieveMPClient);
        //    StoneRitualSend.Write(PointCost);
        //    StoneRitualSend.Write(RitualOwner);
        //    StoneRitualSend.Send();
        //}
        //public void SyncBloodPointsSend()
        //{
        //    if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        return;
        //    }
        //    ModPacket packet = mod.GetPacket();
        //    packet.Write(VampKnives.SyncBloodPoints);
        //    packet.Write(Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints);
        //    packet.Write(RitualOwner);
        //    packet.Send(-1, RitualOwner);
        //}
        //public void SyncOwnerSend()
        //{
        //    if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        return;
        //    }
        //    ModPacket OwnerSend = mod.GetPacket();
        //    OwnerSend.Write(VampKnives.OwnerRecieve);
        //    OwnerSend.Write(ID);
        //    OwnerSend.Write(RitualOwner);
        //    OwnerSend.Send();
        //}

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(RitualOfTheStone);
            writer.Write(RoSType);
            writer.Write(RitualOfTheMiner);
            writer.Write(RoMinType);
            writer.Write(RitualOfMidas);
            writer.Write(RoMidType);
            writer.Write(RitualOfSouls);
            writer.Write(RoSoType);
            writer.Write(RoSoDelay);
            writer.Write(RitualOwner);
            ItemIO.Send(BloodCrystal, writer, true);
        }

        public override void NetReceive(BinaryReader reader)
        {
            RitualOfTheStone = reader.ReadBoolean();
            RoSType = reader.ReadUInt16();
            RitualOfTheMiner = reader.ReadBoolean();
            RoMinType = reader.ReadUInt16();
            RitualOfMidas = reader.ReadBoolean();
            RoMidType = reader.ReadInt16();
            RitualOfSouls = reader.ReadBoolean();
            RoSoType = reader.ReadInt32();
            RoSoDelay = reader.ReadUInt16();
            RitualOwner = reader.ReadInt32();
            BloodCrystal = ItemIO.Receive(reader, true);
        }

        public override bool IsTileValidForEntity(int i, int j)
		{
			var tile = Main.tile[i, j];
            bool Valid = tile.HasTile && tile.TileType == ModContent.TileType<BloodAltar>();
            return Valid;
		}
        public override void OnKill()
        {
            if (CurrentPlayer != byte.MaxValue)
            {
                //if (Main.netMode == NetmodeID.Server)
                //{
                //    ModPacket packet = VampKnives.Instance.GetPacket(MessageType.ExtractorMessage, 6);
                //    packet.Write(ID);
                //    packet.Write((byte)ExtractorMessage.SyncPlayer);
                //    packet.Write(byte.MaxValue);
                //    packet.Send(CurrentPlayer);
                //}
                //else
                //{
                    BloodAltarUI.AltarTE.CloseUI();
                //}
            }
        }
        //public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        //{
        //    Main.NewText("Placed Positions: " + i + ", " + j);

        //    if (Main.netMode == NetmodeID.MultiplayerClient)
        //    {
        //        NetMessage.SendTileRange(Main.myPlayer, i, j, 3, 1);
        //        NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
        //        return -1;
        //    }
        //    return Place(i, j);
        //}
        public override void SaveData(TagCompound tag)
        {
            tag["RitualOwner"] = RitualOwner;
            tag["RoSoDelay"] = RoSoDelay;
            if(!BloodCrystal.IsAir)
                tag["BloodCrystal"] = BloodCrystal;
        }

        public override void LoadData(TagCompound tag)
        {
            RitualOwner = tag.GetInt("RitualOwner");
            RoSoDelay = tag.GetInt("RoSoDelay");
            BloodCrystal = tag.Get<Item>("BloodCrystal");
            if (BloodCrystal == null)
            {
                BloodCrystal = new Item();
            }
        }

        internal void OpenUI(bool fromNet = false)
		{
			bool switching = BloodAltarUI.AltarTE.CurrentPlayer != byte.MaxValue;
			if (switching)
			{
				BloodAltarUI.AltarTE.CurrentPlayer = byte.MaxValue;
			}

			if (PlayerInput.GrappleAndInteractAreShared)
			{
				PlayerInput.Triggers.JustPressed.Grapple = false;
			}

            BloodAltarUI.visible = true;
			ModContent.GetInstance<VampUISystem>().ShowMyUI();
			BloodAltarUI.AltarTE = this;
            //BloodAltarUI.RepopulateItemSlot = true;
			Main.playerInventory = true;
			Main.recBigList = false;
			SoundEngine.PlaySound(switching ? SoundID.MenuTick : SoundID.MenuOpen);
		}

		internal void CloseUI(bool silent = false, bool fromNet = false)
		{
            //if (!fromNet)
            //{
            //    if (Main.netMode == NetmodeID.MultiplayerClient)
            //    {
            //        ModPacket packet = VampKnives.Instance.GetPacket(MessageType.ExtractorMessage, 6);
            //        packet.Write(ID);
            //        packet.Write((byte)ExtractorMessage.SyncPlayer);
            //        packet.Write(byte.MaxValue);
            //        packet.Send();
            //    }
            //    else
            //    {
            //        Main.stackSplit = 600;
            //    }
            //}
            BloodAltarUI.visible = false;
            ModContent.GetInstance<VampUISystem>().HideMyUI(); 
            CurrentPlayer = byte.MaxValue;
			BloodAltarUI.AltarTE = new BloodAltarTE();
			if (!silent)
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
			}
		}
        public void ResetRitualSpace()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                WorldGen.KillTile(Position.X + 1, Position.Y - 2, false, false, false);
                WorldGen.KillTile(Position.X + 1, Position.Y - 1, false, false, false);
                return;
            }
            //ModPacket packet = mod.GetPacket();
            //packet.Write(ResetSpaceServer);
            //packet.Write(Position.X);
            //packet.Write(Position.Y);
            //packet.Send();
        }
        public void StoneRitual(int i, int j, Player player, ushort tileid)
        {
            PointCost = 1;
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            if (p.BloodPoints > 1 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (tileid == TileID.Stone)
                {
                    DustType = 1;
                }
                else if (tileid == TileID.Mud)
                {
                    DustType = 0;
                }
                else if (tileid == TileID.Sand)
                {
                    DustType = 32;
                }
                else if (tileid == TileID.Silt)
                {
                    DustType = 121;
                }
                else if (tileid == TileID.SnowBlock)
                {
                    DustType = 51;
                }
                DustTimer++;
                if (DustTimer >= 2)
                {
                    VampPlayer.OvalDust(new Vector2(16 * (i + 1) + 4.5f, 16 * (j - 1) + 4), 2, 0.8f, Color.White, DustType, 0.2f);
                    DustTimer = 0;
                }
            }
            if (Main.tile[i + 1, j - 2].TileType != tileid && Main.netMode == NetmodeID.SinglePlayer && tileid != TileID.Mud)
            {
                p.BloodPoints -= 1;
            }
            if (tileid == TileID.Sand || tileid == TileID.Silt || tileid == TileID.SnowBlock)
            {
                WorldGen.PlaceTile(i + 1, j - 1, ModContent.TileType<AltarPillar>());
            }
            WorldGen.PlaceTile(i + 1, j - 2, tileid);
            if (Main.tile[i + 1, j - 2].TileType != tileid)
            {
                p.BloodPoints += 1;
                Cost = false;
            }
            else
                Cost = true;
            if(tileid == TileID.Mud)
            {
                Main.tile[i + 1, j - 2].TileType = TileID.Dirt;
            }
            if (Main.netMode == NetmodeID.Server)
            {
                WorldGen.SquareTileFrame(Position.X + 1, Position.Y - 2, true);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, Position.X + 1, Position.Y - 2, 2);
                }
            }
        }
        public void MinerRitual(int i, int j, Player player, ushort tileid)
        {
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            if (tileid == TileID.Copper || tileid == TileID.Tin)
            {
                PointCost = 1;
            }
            else if (tileid == TileID.Iron || tileid == TileID.Lead)
            {
                PointCost = 2;
            }
            else if (tileid == TileID.Silver || tileid == TileID.Tungsten)
            {
                PointCost = 4;
            }
            else if (tileid == TileID.Gold || tileid == TileID.Platinum)
            {
                PointCost = 8;
            }
            else if (tileid == TileID.Meteorite)
            {
                PointCost = 12;
            }
            else if (tileid == TileID.Demonite || tileid == TileID.Crimtane)
            {
                PointCost = 16;
            }
            else if (tileid == TileID.Hellstone)
            {
                PointCost = 24;
            }
            else if (tileid == TileID.Cobalt || tileid == TileID.Palladium)
            {
                PointCost = 50;
            }
            else if (tileid == TileID.Mythril || tileid == TileID.Orichalcum)
            {
                PointCost = 75;
            }
            else if (tileid == TileID.Adamantite || tileid == TileID.Titanium)
            {
                PointCost = 100;
            }
            else if (tileid == TileID.Chlorophyte)
            {
                PointCost = 150;
            }
            else if (tileid == TileID.LunarOre)
            {
                PointCost = 300;
            }
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                if (tileid == TileID.Copper)
                {
                    DustType = 9;
                }
                else if (tileid == TileID.Tin)
                {
                    DustType = 81;
                }
                else if (tileid == TileID.Iron)
                {
                    DustType = 8;
                }
                else if (tileid == TileID.Lead)
                {
                    DustType = 82;
                }
                else if (tileid == TileID.Silver)
                {
                    DustType = 11;
                }
                else if (tileid == TileID.Tungsten)
                {
                    DustType = 83;
                }
                else if (tileid == TileID.Gold)
                {
                    DustType = 10;
                }
                else if (tileid == TileID.Platinum)
                {
                    DustType = 84;
                }
                else if (tileid == TileID.Meteorite)
                {
                    DustType = 127;
                }
                else if (tileid == TileID.Demonite)
                {
                    DustType = 65;
                }
                else if (tileid == TileID.Crimtane)
                {
                    DustType = 117;
                }
                else if (tileid == TileID.Hellstone)
                {
                    DustType = 6;
                }
                else if (tileid == TileID.Cobalt)
                {
                    DustType = 48;
                }
                else if (tileid == TileID.Palladium)
                {
                    DustType = 144;
                }
                else if (tileid == TileID.Mythril)
                {
                    DustType = 49;
                }
                else if (tileid == TileID.Orichalcum)
                {
                    DustType = 145;
                }
                else if (tileid == TileID.Adamantite)
                {
                    DustType = 50;
                }
                else if (tileid == TileID.Titanium)
                {
                    DustType = 146;
                }
                else if (tileid == TileID.Chlorophyte)
                {
                    DustType = 128;
                }
                else if (tileid == TileID.LunarOre)
                {
                    DustType = 242;
                }
                DustTimer++;
                if (DustTimer >= 2)
                {
                    VampPlayer.OvalDust(new Vector2(16 * (i + 1) + 4.5f, 16 * (j - 1) + 4), 2, 0.8f, Color.White, DustType, 1f);
                    DustTimer = 0;
                }
            }
            if (Main.tile[i + 1, j - 2].TileType != tileid && Main.netMode == NetmodeID.SinglePlayer)
            {
                p.BloodPoints -= PointCost;
            }
            WorldGen.PlaceTile(i + 1, j - 2, tileid);
            if (Main.netMode == NetmodeID.Server)
            {
                WorldGen.SquareTileFrame(Position.X + 1, Position.Y - 2, true);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, Position.X + 1, Position.Y - 2, 2);
                }
            }
        }
        public void MidasRitual(int i, int j, Player player, short itemid)
        {
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            if (itemid == ItemID.CopperCoin)
            {
                PointCost = 1;
            }
            else if (itemid == ItemID.SilverCoin)
            {
                PointCost = 10;
            }
            else if (itemid == ItemID.GoldCoin)
            {
                PointCost = 100;
            }
            else if (itemid == ItemID.PlatinumCoin)
            {
                PointCost = 999;
            }
            DustTimer++;
            if (DustTimer >= 5)
            {
                if (itemid == ItemID.CopperCoin)
                {
                    DustType = 9;
                    PointCost = 1;
                }
                else if (itemid == ItemID.SilverCoin)
                {
                    DustType = 11;
                    PointCost = 10;
                }
                else if (itemid == ItemID.GoldCoin)
                {
                    DustType = 10;
                    PointCost = 100;
                }
                else if (itemid == ItemID.PlatinumCoin)
                {
                    DustType = 11;
                    PointCost = 999;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    VampPlayer.OvalDust(new Vector2(16 * (i + 1) + 4.5f, 16 * (j - 1) + 4), 1, 2f, Color.White, DustType, 1f);
                }
                DustTimer = 0;
            }
            if (Main.netMode == NetmodeID.SinglePlayer)
                p.BloodPoints -= PointCost;
            Item.NewItem(new EntitySource_TileEntity(this), new Vector2(16 * (i + 1) + 4.5f, 16 * (j - 1) + 4), 1, 1, itemid);
        }
        NPC TestNPC;
        int SoulsRitualDelay;
        public void SoulsRitual(int i, int j, Player player, int NPCID, int SpawnDelay)
        {
            TestNPC = new NPC();
            TestNPC.SetDefaults(NPCID);
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            PointCost = (TestNPC.lifeMax / 100) > 1 ? (TestNPC.lifeMax / 100) : 1;
            if (SoulsRitualDelay >= SpawnDelay)
            {
                SoulsRitualDelay = 0;
                NPC.NewNPC(new EntitySource_TileEntity(this), (i + 1) * 16, (j + 1) * 16, NPCID);
                if (Main.netMode == NetmodeID.SinglePlayer)
                    p.BloodPoints -= PointCost;
                else if (Main.netMode == NetmodeID.Server)
                {
                    Main.player[RitualOwner].GetModPlayer<VampPlayer>().BloodPoints -= PointCost;
                    //SendCost();
                }

            }
            SoulsRitualDelay++;
        }
    }
}