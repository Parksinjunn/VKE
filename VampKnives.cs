using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using vke.Tiles;
using vke.UI;
using VKE.Items.PreKnives;
using static VKE.VampNet;

namespace VKE
{
    public class VampKnives : Mod
    {
        public static VampKnives Instance { get; private set; }
        public const string Abbreviation = "MoR";
        private List<ILoadable> _loadCache;

        public static ModKeybind HoodUpDownHotkey;
        public static ModKeybind SupportHotKey;
        public static ModKeybind VampDashHotKey;
        public static ModKeybind SupportArmorHotKey;
        public static ModKeybind BookHotKey;

        public static float ConfigDamageMult = 1f;
        public static float ConfigHealAmntMult = 1f;
        public static float HealProjectileSpawn = 1f;
        public static float AmmoDefenseDecrease = 1f;
        public static string Test = "[c/FF0000:Overrides advanced settings)]";

        internal UserInterface VampireUserInterface;
        internal UserInterface VampireUserInterface2;

        public VampKnives()
        {
            Instance = this;
        }
        public override void Load()
        {
            LoadCache();
            Instance = this;
            HoodUpDownHotkey = KeybindLoader.RegisterKeybind(this, "Pull hood up or down", "P");
            SupportHotKey = KeybindLoader.RegisterKeybind(this, "Key to add/remove support debuff", "L");
            VampDashHotKey = KeybindLoader.RegisterKeybind(this, "Double tap to transform into a bat for a few seconds(Requires vampiric armor)", "D");
            SupportArmorHotKey = KeybindLoader.RegisterKeybind(this, "Key to use the support armor's buff", "C");
            BookHotKey = KeybindLoader.RegisterKeybind(this, "Key to open the in-game guide", "H");
            if (!Main.dedServ)
            {
                //AddEquipTexture(null, EquipType.Legs, "ExampleRobe_Legs", "ExampleMod/Items/Armor/ExampleRobe_Legs");
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/PyromancersHood_Head", EquipType.Head, name: "PyroHead", equipTexture: new Items.Accessories.Hoods.PyroHead());
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/DarkPyromancersHood_Head", EquipType.Head, name: "DPyroHead", equipTexture: new Items.Accessories.Hoods.DPyroHead());
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/TransmutersHood_Head", EquipType.Head, name: "TransmuterHead", equipTexture: new Items.Accessories.Hoods.TransmuterHead());
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/InvokersHood_Head", EquipType.Head, name: "InvokerHead", equipTexture: new Items.Accessories.Hoods.InvokerHead());
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/TechnomancersHood_Head", EquipType.Head, name: "TechnomancerHead", equipTexture: new Items.Accessories.Hoods.TechnomancerHead());
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/PartyHood_Head", EquipType.Head, name: "PartyHead", equipTexture: new Items.Accessories.Hoods.PartyHead());
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/ShamansHood_Head", EquipType.Head, name: "ShamanHead", equipTexture: new Items.Accessories.Hoods.ShamanHead());
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/WitchDoctorHood_Head", EquipType.Head, name: "WitchDoctorHead", equipTexture: new Items.Accessories.Hoods.WitchDoctorHead());
                //EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/Hoods/MagesHood_Head", EquipType.Head, name: "MageHead", equipTexture: new Items.Accessories.Hoods.MageHead());
                EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/BatTransform", EquipType.Head, name: "BatTransform");
                EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/BatTransformHidden", EquipType.Head, name: "BatTransformHidden");
                EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/BatTransformHidden", EquipType.Body, name: "BatTransformHidden_Body");
                EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/BatTransformHidden", EquipType.Legs, name: "BatTransformHidden_Legs");
                EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/BatFlyMovement", EquipType.Wings, name: "BatFlyMovement");
                EquipLoader.AddEquipTexture(this, "VKE/Items/Accessories/BatWingsHidden", EquipType.Wings, name: "BatWingsHidden");
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/SuccubusHeartCorset_Head", EquipType.Head, name: "VeiTransformHead");
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/SuccubusHeartCorset_Body", EquipType.Body, name: "VeiTransformBody")/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/SuccubusHeartCorset_Legs", EquipType.Legs, name: "VeiTransformLegs");
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/GamerHeadphones_Head", EquipType.Head, name: "NyanTransformHead");
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/GamerHeadphones_Body", EquipType.Body, name: "NyanTransformBody")/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/GamerHeadphones_Legs", EquipType.Legs, name: "NyanTransformLegs");
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/DemonLoli_Head", EquipType.Head, name: "MouseTransformHead");
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/DemonLoli_Body", EquipType.Body, name: "MouseTransformBody")/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
                //EquipLoader.AddEquipTexture(this, "VampKnives/Items/VtuberItems/DemonLoli_Legs", EquipType.Legs, name: "MouseTransformLegs");

                Ref<Effect> BasicTrailRef = new(Assets.Request<Effect>("Effects/Primitives/BasicTrailShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> LightningTrailRef = new(Assets.Request<Effect>("Effects/Primitives/LightningTrailShader", AssetRequestMode.ImmediateLoad).Value);
                //Ref<Effect> TestTrailRef = new(Assets.Request<Effect>("Effects/Primitives/TestShader", AssetRequestMode.ImmediateLoad).Value);




                GameShaders.Misc["VampKnives:BasicTrail"] = new MiscShaderData(BasicTrailRef, "TrailPass");
                GameShaders.Misc["VampKnives:LightningTrail"] = new MiscShaderData(LightningTrailRef, "TrailPass");
                //GameShaders.Misc["VampKnives:TestTrail"] = new MiscShaderData(TestTrailRef, "TrailPass");



                //customRecources = new UserInterface();
                //customResources2 = new UserInterface();
                //FirstLoadUI = new UserInterface();
                //vampBar = new VampBar();
                //VampBar.visible = true;
                //RecipePage = new RecipePageState();
                //customResources2.SetState(RecipePage);
                //customRecources.SetState(vampBar);
                //VampireUserInterface = new UserInterface();
                //VampireUserInterface2 = new UserInterface();

                //WorkbenchSlots = new UserInterface();
                //WorkbenchSlotPanel = new WorkbenchSlotState();
                //WorkbenchSlots.SetState(WorkbenchSlotPanel);

                //UpgradePanelUI = new UserInterface();
                ////UpgradePanelState = new UpgradePanel();
                ////UpgradePanelUI.SetState(UpgradePanelState);

                //BloodAltarUIPanel = new UserInterface();
                //BloodAltarUIState = new BloodAltarUI();
                //BloodAltarUIPanel.SetState(BloodAltarUIState);

                //StartupInterface = new UserInterface();
                //StartupState = new StartupBookUI();
                //StartupInterface.SetState(StartupState);
            }
            //if (Main.netMode != NetmodeID.Server)
            //{
            //    Ref<Effect> shaderRef = new Ref<Effect>(GetEffect("Effects/MilkShader"));
            //    GameShaders.Misc["Technique1"] = new MiscShaderData(shaderRef, "ArmorBasic").UseColor(new Vector3(Color.Brown.R / 255f, Color.Brown.G / 255f, Color.Brown.B / 255f));
            //}
        }
        public override void Unload()
        {
            HoodUpDownHotkey = null;
            SupportHotKey = null;
            VampDashHotKey = null;
            SupportArmorHotKey = null;
            BookHotKey = null;
            Instance = null;
            //if (!Main.dedServ)
            //{
            //    UpgradePanelUI = null;
            //    BloodAltarUIPanel = null;
            //}
            base.Unload();
        }

        public static int OwnerRecieve = 1;
        int OwnerSend = 2;
        public static int ResetSpaceServer = 3;
        int ResetSpaceMPClient = 4;
        public static int SyncBloodPoints = 5;
        public static int RitualCostRecieveMPClient = 6;
        public static int StoneRitualRecieveMPClient = 7;
        public static int MinerRitualRecieveMPClient = 8;
        public static int MidasRitualRecieveMPClient = 9;
        public static int SoulsRitualRecieveMPClient = 10;
        public static int SoulItemSync = 11;
        int SoulItemSyncClient = 12;
        public static int HoodServerRecieve = 20;
        int HoodSendToClient = 21;
        public static int SendBloodPoints = 22;
        public static int SyncSupportHealsServer = 23;
        int SyncSupportHealsClient = 24;
        public static int BatTransformRecieve = 25;
        int BatTransformSend = 26;
        public static int VeiTransormRecieve = 27;
        int VeiTransformSend = 28;
        public static int NyanTransformRecieve = 29;
        int NyanTransformSend = 30;
        public static int MouseTransformRecieve = 31;
        int MouseTransformSend = 32;
        public static int SupportArmorRecieve = 33;
        int SupportArmorSend = 34;

        int DustType;
        int DustTimer;

        internal ModPacket GetPacket(MessageType type, int capacity)
        {
            ModPacket packet = GetPacket(capacity + 1);
            packet.Write((byte)type);
            return packet;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            //MessageType message = (MessageType)reader.ReadByte();
            //switch (message)
            //{
            //    case MessageType.AltarMessage:
            //        this.GetTileEntity<Tiles.BloodAltarTE>(reader.ReadInt32())?.RecieveAltarMessage(reader, whoAmI);
            //        //Main.NewText("Recieved on Server");
            //        break;
            //}
            int idVariable = reader.ReadInt32();
            if (idVariable == HoodServerRecieve)
            {
                bool KeyPressed = reader.ReadBoolean();
                int playerID = reader.ReadInt32();
                ModPacket packet2 = this.GetPacket();
                packet2.Write(HoodSendToClient);
                packet2.Write(KeyPressed);
                packet2.Write(playerID);
                packet2.Send(-1, playerID);
            }
            if (idVariable == HoodSendToClient)
            {
                bool KeyPressed2 = reader.ReadBoolean();

                int playerID = reader.ReadInt32();
                Main.player[playerID].GetModPlayer<VampPlayer>().HoodIsVisible = KeyPressed2;
            }
            if (idVariable == SendBloodPoints)
            {
                Main.LocalPlayer.GetModPlayer<VampPlayer>().NeckProgress++;
            }

            //if (idVariable == SyncSupportHealsServer)
            //{
            //    int Owner = reader.ReadInt32();
            //    int Decision = reader.ReadInt32();
            //    int SyncLifeAmount = reader.ReadInt32();
            //    bool BuffState = Main.player[Owner].HasBuff(ModContent.BuffType<Buffs.TrueSupportDebuff>());
            //    ModPacket packet5 = this.GetPacket();
            //    packet5.Write(SyncSupportHealsClient);
            //    packet5.Write(Decision);
            //    packet5.Write(SyncLifeAmount);
            //    packet5.Write(Owner);
            //    packet5.Write(BuffState);
            //    packet5.Send(-1, Owner);
            //}
            //if (idVariable == SyncSupportHealsClient)
            //{
            //    int Decision = reader.ReadInt32();
            //    int SyncLifeAmount = reader.ReadInt32();
            //    int Owner = reader.ReadInt32();
            //    bool BuffState = reader.ReadBoolean();
            //    if (BuffState)
            //        Main.player[Owner].AddBuff(ModContent.BuffType<Buffs.TrueSupportDebuff>(), 60);
            //    Main.player[Decision].statLife += (SyncLifeAmount);
            //    if (SyncLifeAmount >= 1)
            //        Main.player[Decision].HealEffect(SyncLifeAmount, false);
            //}
            if (idVariable == BatTransformRecieve)
            {
                bool Transform = reader.ReadBoolean();
                bool HasTablet = reader.ReadBoolean();
                int playerID = reader.ReadInt32();
                ModPacket packet = this.GetPacket();
                packet.Write(BatTransformSend);
                packet.Write(Transform);
                packet.Write(HasTablet);
                packet.Write(playerID);
                packet.Send(-1, playerID);
            }
            if (idVariable == BatTransformSend)
            {
                bool Transform = reader.ReadBoolean();
                bool HasTablet = reader.ReadBoolean();
                int playerID = reader.ReadInt32();
                Main.player[playerID].GetModPlayer<VampPlayer>().Transform = Transform;
                Main.player[playerID].GetModPlayer<VampPlayer>().HasTabletEquipped = HasTablet;
            }
            //if (idVariable == VeiTransormRecieve)
            //{
            //    bool VeiTransform = reader.ReadBoolean();
            //    int PlayerID = reader.ReadInt32();
            //    ModPacket packet = this.GetPacket();
            //    packet.Write(VeiTransformSend);
            //    packet.Write(VeiTransform);
            //    packet.Write(PlayerID);
            //    packet.Send(-1, PlayerID);
            //}
            //if (idVariable == VeiTransformSend)
            //{
            //    bool VeiTransform = reader.ReadBoolean();
            //    int playerID = reader.ReadInt32();
            //    Main.player[playerID].GetModPlayer<VampPlayer>().VeiTransform = VeiTransform;
            //}
            //if (idVariable == NyanTransformRecieve)
            //{
            //    bool NyanTransform = reader.ReadBoolean();
            //    int PlayerID = reader.ReadInt32();
            //    ModPacket packet = this.GetPacket();
            //    packet.Write(NyanTransformSend);
            //    packet.Write(NyanTransform);
            //    packet.Write(PlayerID);
            //    packet.Send(-1, PlayerID);
            //}
            //if (idVariable == NyanTransformSend)
            //{
            //    bool NyanTransform = reader.ReadBoolean();
            //    int PlayerID = reader.ReadInt32();
            //    Main.player[PlayerID].GetModPlayer<VampPlayer>().NyanTransform = NyanTransform;
            //}
            //if (idVariable == MouseTransformRecieve)
            //{
            //    bool MouseTransform = reader.ReadBoolean();
            //    int PlayerID = reader.ReadInt32();
            //    ModPacket packet = this.GetPacket();
            //    packet.Write(MouseTransformSend);
            //    packet.Write(MouseTransform);
            //    packet.Write(PlayerID);
            //    packet.Send(-1, PlayerID);
            //}
            //if (idVariable == MouseTransformSend)
            //{
            //    bool MouseTransform = reader.ReadBoolean();
            //    int PlayerID = reader.ReadInt32();
            //    Main.player[PlayerID].GetModPlayer<VampPlayer>().MouseTransform = MouseTransform;
            //}
            //if (idVariable == SupportArmorRecieve)
            //{
            //    int PlayerToBuff = reader.ReadInt32();
            //    int BuffCountStore = reader.ReadInt32();
            //    int PlayerID = reader.ReadInt32();
            //    ModPacket packet = this.GetPacket();
            //    packet.Write(SupportArmorSend);
            //    packet.Write(PlayerToBuff);
            //    packet.Write(BuffCountStore);
            //    packet.Send(-1, PlayerID);
            //}
            //if (idVariable == SupportArmorSend)
            //{
            //    int PlayerToBuff = reader.ReadInt32();
            //    int BuffCountStore = reader.ReadInt32();
            //    Main.player[PlayerToBuff].AddBuff(ModContent.BuffType<Buffs.SupportBuff>(), 600);
            //    Main.player[PlayerToBuff].GetModPlayer<VampPlayer>().BuffCountStore = BuffCountStore;
            //}

            /* RITUAL NETCODE */
            //if (idVariable == OwnerRecieve)
            //{
            //    int ID = reader.ReadInt32();
            //    int OwnerID = reader.ReadInt32();

            //    this.GetTileEntity<BloodAltarTE>(ID).RitualOwner = OwnerID;
            //    ModPacket packet = this.GetPacket();
            //    packet.Write(OwnerSend);
            //    packet.Write(ID);
            //    packet.Write(OwnerID);
            //    packet.Send(-1, OwnerID);
            //}
            //if (idVariable == OwnerSend)
            //{
            //    int ID = reader.ReadInt32();
            //    int OwnerID = reader.ReadInt32();
            //    this.GetTileEntity<BloodAltarTE>(ID).RitualOwner = OwnerID;
            //}
            if (idVariable == ResetSpaceServer)
            {
                short PosX = reader.ReadInt16();
                short PosY = reader.ReadInt16();

                WorldGen.KillTile(PosX + 1, PosY - 2, false, false, false);
                WorldGen.KillTile(PosX + 1, PosY - 1, false, false, true);

                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)PosX + 1, (float)PosY - 1, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)PosX + 1, (float)PosY - 2, 0f, 0, 0, 0);

                ModPacket packet = this.GetPacket();
                packet.Write(ResetSpaceMPClient);
                packet.Write(PosX);
                packet.Write(PosY);
                packet.Send(-1, -1);
            }
            if (idVariable == ResetSpaceMPClient)
            {
                int PosX = reader.ReadInt16();
                int PosY = reader.ReadInt16();

                WorldGen.KillTile(PosX + 1, PosY - 2, false, false, false);
                WorldGen.KillTile(PosX + 1, PosY - 1, false, false, true);

            }
            if (idVariable == RitualCostRecieveMPClient)
            {
                int Cost = reader.ReadInt32();
                int Owner = reader.ReadInt32();
                Main.player[Owner].GetModPlayer<VampPlayer>().BloodPoints -= Cost;
            }
            if (idVariable == SyncBloodPoints)
            {
                int BloodPoints = reader.ReadInt32();
                int Owner = reader.ReadInt32();

                Main.player[Owner].GetModPlayer<VampPlayer>().BloodPoints = BloodPoints;
            }
            if (idVariable == StoneRitualRecieveMPClient)
            {
                int ID = reader.ReadInt32();
                bool RitualOfTheStone = reader.ReadBoolean();
                int Owner = reader.ReadInt32();
                ushort tileid = reader.ReadUInt16();

                TileEntity ent = TileEntity.Read(reader, true);
                ent.ID = ID;
                TileEntity.ByID[ID] = ent;
                TileEntity.ByPosition[ent.Position] = ent;
                if(ent is BloodAltarTE)
                {
                    ((BloodAltarTE)ent).RitualOfTheStone = RitualOfTheStone;
                    ((BloodAltarTE)ent).RitualOwner = Owner;
                    ((BloodAltarTE)ent).RoSType = tileid;
                    NetMessage.SendData(MessageID.TileEntitySharing, -1, Owner, null, ID, ent.Position.X, ent.Position.Y);
                }
            }
            //if (idVariable == MinerRitualRecieveMPClient)
            //{
            //    int ID = reader.ReadInt32();
            //    bool RitualOfTheMiner = reader.ReadBoolean();
            //    int Owner = reader.ReadInt32();
            //    ushort tileid = reader.ReadUInt16();

            //    this.GetTileEntity<BloodAltarTE>(ID).RitualOfTheMiner = RitualOfTheMiner;
            //    this.GetTileEntity<BloodAltarTE>(ID).RitualOwner = Owner;
            //    this.GetTileEntity<BloodAltarTE>(ID).RoMinType = tileid;
            //}
            //if (idVariable == MidasRitualRecieveMPClient)
            //{
            //    int ID = reader.ReadInt32();
            //    bool RitualOfMidas = reader.ReadBoolean();
            //    int Owner = reader.ReadInt32();
            //    short ItemID = reader.ReadInt16();

            //    this.GetTileEntity<BloodAltarTE>(ID).RitualOfMidas = RitualOfMidas;
            //    this.GetTileEntity<BloodAltarTE>(ID).RitualOwner = Owner;
            //    this.GetTileEntity<BloodAltarTE>(ID).RoMidType = ItemID;
            //}
            //if (idVariable == SoulsRitualRecieveMPClient)
            //{
            //    int ID = reader.ReadInt32();
            //    bool RitualOfSouls = reader.ReadBoolean();
            //    int Owner = reader.ReadInt32();
            //    int NPCID = reader.ReadInt32();
            //    int Delay = reader.ReadInt32();
            //    //ItemIO.Receive(reader, true);

            //    this.GetTileEntity<BloodAltarTE>(ID).RitualOfSouls = RitualOfSouls;
            //    this.GetTileEntity<BloodAltarTE>(ID).RitualOwner = Owner;
            //    this.GetTileEntity<BloodAltarTE>(ID).RoSoType = NPCID;
            //    this.GetTileEntity<BloodAltarTE>(ID).RoSoDelay = Delay;
            //    //this.GetTileEntity<BloodAltarTE>(ID).BloodCrystal = ItemIO.Receive(reader, true);
            //}
            base.HandlePacket(reader, whoAmI);
        }
        internal enum MessageType
        {
            AltarMessage
        }







        //public static void PremultiplyTexture(ref Texture2D texture)
        //{
        //    Color[] buffer = new Color[texture.Width * texture.Height];
        //    texture.GetData(buffer);
        //    for (int i = 0; i < buffer.Length; i++)
        //    {
        //        buffer[i] = Color.FromNonPremultiplied(
        //                buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
        //    }
        //    texture.SetData(buffer);
        //}
        private void LoadCache()
        {
            _loadCache = new List<ILoadable>();

            foreach (Type type in Code.GetTypes())
            {
                if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(ILoadable)))
                {
                    _loadCache.Add(Activator.CreateInstance(type) as ILoadable);
                }
            }

            _loadCache.Sort((x, y) => x.Priority > y.Priority ? 1 : -1);

            for (int i = 0; i < _loadCache.Count; ++i)
            {
                if (Main.dedServ && !_loadCache[i].LoadOnDedServer)
                {
                    continue;
                }

                _loadCache[i].Load();
            }
        }

        //public static ModPacket WriteToPacket(ModPacket packet, byte msg, params object[] param)
        //{
        //    packet.Write(msg);

        //    for (int m = 0; m < param.Length; m++)
        //    {
        //        object obj = param[m];
        //        if (obj is bool boolean) packet.Write(boolean);
        //        else
        //        if (obj is byte @byte) packet.Write(@byte);
        //        else
        //        if (obj is int @int) packet.Write(@int);
        //        else
        //        if (obj is float single) packet.Write(single);
        //    }
        //    return packet;
        //}
        //public override void HandlePacket(BinaryReader bb, int whoAmI)
        //{
        //    ModMessageType msgType = (ModMessageType)bb.ReadByte();
        //    switch (msgType)
        //    {
        //        case ModMessageType.SpawnTrail:
        //            int projindex = bb.ReadInt32();

        //            if (Main.netMode == NetmodeID.Server)
        //            {
        //                WriteToPacket(GetPacket(), (byte)ModMessageType.SpawnTrail, projindex).Send();
        //                break;
        //            }

        //            if (Main.projectile[projindex].ModProjectile is ITrailProjectile trailproj)
        //                trailproj.DoTrailCreation(RedeSystem.TrailManager);

        //            break;
        //    }
        //}
    }
}