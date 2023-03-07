using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using VKE.Items;
using VKE.Items.Misc;
using vke.Tiles;
using static Terraria.ModLoader.ModContent;
using VKE;

namespace vke.UI
{
    internal class BloodAltarUI : UIState
    {
        public static bool visible = false;
        internal static BloodAltarTE AltarTE = new BloodAltarTE();
        public EntranceButton ClaimButton;
        public EntranceButton CloseButton;
        public EntranceButton RitualOfStoneButton;
        public EntranceButton RoEStone;
        public EntranceButton RoEDirt;
        public EntranceButton RoESand;
        public EntranceButton RoESilt;
        public EntranceButton RoESnow;
        public EntranceButton RitualOfTheMinerButton;
        public EntranceButton CopperTinButton;
        public EntranceButton IronLeadButton;
        public EntranceButton SilverTungstenButton;
        public EntranceButton GoldPlatinumButton;
        public EntranceButton MeteoriteButton;
        public EntranceButton DemoniteCrimtaneButton;
        public EntranceButton HellstoneButton;
        public EntranceButton CobaltPalladiumButton;
        public EntranceButton MythrilOrichalcumButton;
        public EntranceButton AdamantiteTitaniumButton;
        public EntranceButton ChlorophyteButton;
        public EntranceButton LuminiteButton;
        public EntranceButton RitualOfMidasButton;
        public EntranceButton CopperCoinButton;
        public EntranceButton SilverCoinButton;
        public EntranceButton GoldCoinButton;
        public EntranceButton PlatinumCoinButton;
        public EntranceButton RitualOfSoulsButton;
        public EntranceButton AddDelayButton;
        public EntranceButton SubtractDelayButton;
        public EntranceBackgroundPanel Background;
        public UIFlatPanel centerTest;
        public UIImage EarthActive;
        public UIImage MinerActive;
        public UIImage MidasActive;
        public UIImage SoulsActive;
        public UIText OwnerActiveText;
        public UIText EarthActiveText;
        public UIText MinerActiveText;
        public UIText MidasActiveText;
        public UIText SoulsActiveText;
        public UIText SoulsNPC;
        public UIText SpawnDelay;
        private VanillaItemSlotWrapper _vanillaItemSlot;
        public UIImage BCSlot;
        public UIImage AltarTitle;
        public EntranceButton BCEject;

        string EarthText = "None";
        string MinerText = "None";
        string MidasText = "None";
        string SoulsText = "None";
        string SoulsDelay = "Delay = 0";
        string OwnerText = "Owner: ";
        Vector2 ButtonSize = new Vector2(256, 66);

        float BackgroundWidth = 400f;
        float BackgroundHeight = 700f;
        int TextOffset = 35;

        float RoSButtonVPos = 0.1f;
        float RoMButtonVPos = 0.25f;
        float RoMiButtonVPos = 0.5f;
        float RoMButtonsVPos1 = 0.35f;
        float RoMButtonsVPos2 = 0.4f;
        float RoSoButtonVPos = 0.65f;
        bool HasBeenWarned;

        public override void OnInitialize()
        {
            Background = new EntranceBackgroundPanel();
            AltarTE = new BloodAltarTE();

            Background.BackgroundColor = Color.Black;
            Background.BorderColor = Color.DarkGray;
            Background.Width.Set(BackgroundWidth, 0f);
            Background.Height.Set(BackgroundHeight, 0f);
            Background.HAlign = 0.7f; // 1
            Background.VAlign = 0.5f;
            base.Append(Background);

            AltarTitle = new UIImage(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/BloodAltarTitle").Value);
            AltarTitle.HAlign = 0.5f;
            AltarTitle.VAlign = 0.015f;
            Background.Append(AltarTitle);

            //ClaimButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/ClaimButton"), "Claim this altar (you pay for the rituals performed at this altar)");
            //ClaimButton.VAlign = 0.015f;
            //ClaimButton.HAlign = 0.05f;
            //ClaimButton.Width.Set(128, 0f);
            //ClaimButton.Height.Set(33, 0f);
            //ClaimButton.OnClick += new MouseEvent(ClaimButtonClicked);
            //Background.Append(ClaimButton);

            CloseButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/CloseButton"), "Close the UI");
            CloseButton.VAlign = 0.015f;
            CloseButton.HAlign = 0.95f;
            CloseButton.Width.Set(32, 0f);
            CloseButton.Height.Set(32, 0f);
            CloseButton.OnClick += new MouseEvent(CloseButtonClicked);
            Background.Append(CloseButton);

            //_vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
            //{
            //    HoverText = "Upgrade Slot",
            //    ValidItemFunc = item => item.IsAir || !item.IsAir && (GetModItem(item.type) is Items.KnifeItem)
            //};
            //_vanillaItemSlot.HAlign = 0.5f;
            //_vanillaItemSlot.VAlign = 0.05f;
            //Background.Append(_vanillaItemSlot);

            RitualOfStoneButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/RitualOfEarth"), "Perform the ritual of the earth, summon any of these five materials for 1 bp each");
            RitualOfStoneButton.VAlign = RoSButtonVPos;
            RitualOfStoneButton.HAlign = 0.5f;
            RitualOfStoneButton.Width.Set(ButtonSize.X, 0f);
            RitualOfStoneButton.Height.Set(ButtonSize.Y, 0f);
            RitualOfStoneButton.OnClick += new MouseEvent(StoneRitualButtonClicked);
            Background.Append(RitualOfStoneButton);

            RoEStone = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/StoneButton"), "");
            RoEStone.VAlign = 1.5f;
            RoEStone.HAlign = 0.1f;
            RoEStone.Width.Set(32, 0f);
            RoEStone.Height.Set(32, 0f);
            RoEStone.OnClick += new MouseEvent(StoneButtonClicked);
            RitualOfStoneButton.Append(RoEStone);

            RoEDirt = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/DirtButton"), "");
            RoEDirt.VAlign = 1.5f;
            RoEDirt.HAlign = 0.3f;
            RoEDirt.Width.Set(32, 0f);
            RoEDirt.Height.Set(32, 0f);
            RoEDirt.OnClick += new MouseEvent(DirtButtonClicked);
            RitualOfStoneButton.Append(RoEDirt);

            RoESand = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/SandButton"), "");
            RoESand.VAlign = 1.5f;
            RoESand.HAlign = 0.5f;
            RoESand.Width.Set(32, 0f);
            RoESand.Height.Set(32, 0f);
            RoESand.OnClick += new MouseEvent(SandButtonClicked);
            RitualOfStoneButton.Append(RoESand);

            RoESilt = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/SiltButton"), "");
            RoESilt.VAlign = 1.5f;
            RoESilt.HAlign = 0.7f;
            RoESilt.Width.Set(32, 0f);
            RoESilt.Height.Set(32, 0f);
            RoESilt.OnClick += new MouseEvent(SiltButtonClicked);
            RitualOfStoneButton.Append(RoESilt);

            RoESnow = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/SnowButton"), "");
            RoESnow.VAlign = 1.5f;
            RoESnow.HAlign = 0.9f;
            RoESnow.Width.Set(32, 0f);
            RoESnow.Height.Set(32, 0f);
            RoESnow.OnClick += new MouseEvent(SnowButtonClicked);
            RitualOfStoneButton.Append(RoESnow);

            RitualOfTheMinerButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/RitualOfTheMiner"), "Perform the ritual of the miner");
            RitualOfTheMinerButton.VAlign = RoMButtonVPos;
            RitualOfTheMinerButton.HAlign = 0.5f;
            RitualOfTheMinerButton.Width.Set(ButtonSize.X, 0f);
            RitualOfTheMinerButton.Height.Set(ButtonSize.Y, 0f);
            RitualOfTheMinerButton.OnClick += new MouseEvent(MinerRitualButtonClicked);
            Background.Append(RitualOfTheMinerButton);

            CopperTinButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/CopperTinButton"), "Copper/Tin: 1bp");
            CopperTinButton.VAlign = RoMButtonsVPos1;
            CopperTinButton.HAlign = 0f;
            CopperTinButton.Width.Set(32, 0f);
            CopperTinButton.Height.Set(32, 0f);
            CopperTinButton.OnClick += new MouseEvent(CopperTinButtonClicked);
            Background.Append(CopperTinButton);

            IronLeadButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/IronLeadButton"), "Iron/Lead: 2bp");
            IronLeadButton.VAlign = RoMButtonsVPos1;
            IronLeadButton.HAlign = 0.2f;
            IronLeadButton.Width.Set(32, 0f);
            IronLeadButton.Height.Set(32, 0f);
            IronLeadButton.OnClick += new MouseEvent(IronLeadButtonClicked);
            Background.Append(IronLeadButton);

            SilverTungstenButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/SilverTungstenButton"), "Silver/Tungsten: 4bp");
            SilverTungstenButton.VAlign = RoMButtonsVPos1;
            SilverTungstenButton.HAlign = 0.4f;
            SilverTungstenButton.Width.Set(32, 0f);
            SilverTungstenButton.Height.Set(32, 0f);
            SilverTungstenButton.OnClick += new MouseEvent(SilverTungstenButtonClicked);
            Background.Append(SilverTungstenButton);

            GoldPlatinumButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/GoldPlatinumButton"), "Gold/Platinum: 8bp");
            GoldPlatinumButton.VAlign = RoMButtonsVPos1;
            GoldPlatinumButton.HAlign = 0.6f;
            GoldPlatinumButton.Width.Set(32, 0f);
            GoldPlatinumButton.Height.Set(32, 0f);
            GoldPlatinumButton.OnClick += new MouseEvent(GoldPlatinumButtonClicked);
            Background.Append(GoldPlatinumButton);

            MeteoriteButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/MeteoriteButton"), "Meteorite: 12bp");
            MeteoriteButton.VAlign = RoMButtonsVPos1;
            MeteoriteButton.HAlign = 0.8f;
            MeteoriteButton.Width.Set(32, 0f);
            MeteoriteButton.Height.Set(32, 0f);
            MeteoriteButton.OnClick += new MouseEvent(MeteoriteButtonClicked);
            Background.Append(MeteoriteButton);

            DemoniteCrimtaneButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/DemoniteCrimtaneButton"), "Demonite/Crimtane: 16bp");
            DemoniteCrimtaneButton.VAlign = RoMButtonsVPos1;
            DemoniteCrimtaneButton.HAlign = 1f;
            DemoniteCrimtaneButton.Width.Set(32, 0f);
            DemoniteCrimtaneButton.Height.Set(32, 0f);
            DemoniteCrimtaneButton.OnClick += new MouseEvent(DemoniteCrimtaneButtonClicked);
            Background.Append(DemoniteCrimtaneButton);

            HellstoneButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/HellstoneButton"), "Hellstone: 24bp");
            HellstoneButton.VAlign = RoMButtonsVPos2;
            HellstoneButton.HAlign = 0f;
            HellstoneButton.Width.Set(32, 0f);
            HellstoneButton.Height.Set(32, 0f);
            HellstoneButton.OnClick += new MouseEvent(HellstoneButtonClicked);
            Background.Append(HellstoneButton);

            CobaltPalladiumButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/CobaltPalladiumButton"), "Cobalt/Palladium: 50bp");
            CobaltPalladiumButton.VAlign = RoMButtonsVPos2;
            CobaltPalladiumButton.HAlign = 0.2f;
            CobaltPalladiumButton.Width.Set(32, 0f);
            CobaltPalladiumButton.Height.Set(32, 0f);
            CobaltPalladiumButton.OnClick += new MouseEvent(CobaltPalladiumButtonClicked);
            Background.Append(CobaltPalladiumButton);

            MythrilOrichalcumButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/MythrilOrichalcumButton"), "Mythril/Orichalcum: 75bp");
            MythrilOrichalcumButton.VAlign = RoMButtonsVPos2;
            MythrilOrichalcumButton.HAlign = 0.4f;
            MythrilOrichalcumButton.Width.Set(32, 0f);
            MythrilOrichalcumButton.Height.Set(32, 0f);
            MythrilOrichalcumButton.OnClick += new MouseEvent(MythrilOrichalcumButtonClicked);
            Background.Append(MythrilOrichalcumButton);

            AdamantiteTitaniumButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/AdamantiteTitaniumButton"), "Adamantite/Titanium: 100bp");
            AdamantiteTitaniumButton.VAlign = RoMButtonsVPos2;
            AdamantiteTitaniumButton.HAlign = 0.6f;
            AdamantiteTitaniumButton.Width.Set(32, 0f);
            AdamantiteTitaniumButton.Height.Set(32, 0f);
            AdamantiteTitaniumButton.OnClick += new MouseEvent(AdamantiteTitaniumButtonClicked);
            Background.Append(AdamantiteTitaniumButton);

            ChlorophyteButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/ChlorophyteButton"), "Chlorophyte: 150bp");
            ChlorophyteButton.VAlign = RoMButtonsVPos2;
            ChlorophyteButton.HAlign = 0.8f;
            ChlorophyteButton.Width.Set(32, 0f);
            ChlorophyteButton.Height.Set(32, 0f);
            ChlorophyteButton.OnClick += new MouseEvent(ChlorophyteButtonClicked);
            Background.Append(ChlorophyteButton);

            LuminiteButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/LuminiteButton"), "Luminite: 300bp");
            LuminiteButton.VAlign = RoMButtonsVPos2;
            LuminiteButton.HAlign = 1f;
            LuminiteButton.Width.Set(32, 0f);
            LuminiteButton.Height.Set(32, 0f);
            LuminiteButton.OnClick += new MouseEvent(LuminiteButtonClicked);
            Background.Append(LuminiteButton);

            UIFlatPanel SharpnessUnderline = new UIFlatPanel();
            SharpnessUnderline.BackgroundColor = Color.White;
            SharpnessUnderline.VAlign = -0.2f;
            SharpnessUnderline.HAlign = 0.5f;
            SharpnessUnderline.Width.Set(ButtonSize.X, 0f);
            SharpnessUnderline.Height.Set(6, 0f);
            //RitualOfStoneButton.Append(SharpnessUnderline);

            UIText Sharpness = new UIText("Rituals");
            Sharpness.Top.Set(-25, 0f);
            Sharpness.HAlign = 0.5f;
            //SharpnessUnderline.Append(Sharpness);

            Texture2D ActiveImage = ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/ActiveButton").Value;
            EarthActive = new UIImage(ActiveImage);
            EarthActive.HAlign = 0f;
            EarthActive.VAlign = RoSButtonVPos+0.01f;
            Background.Append(EarthActive);

            //OwnerActiveText = new UIText(OwnerText);
            //OwnerActiveText.HAlign = 0.6f;
            //OwnerActiveText.VAlign = 0.025f;
            //Background.Append(OwnerActiveText);

            EarthActiveText = new UIText("Dirt");
            EarthActiveText.HAlign = 1f;
            EarthActiveText.VAlign = RoSButtonVPos + 0.01f;
            Background.Append(EarthActiveText);

            MinerActive = new UIImage(ActiveImage);
            MinerActive.HAlign = 0f;
            MinerActive.VAlign = RoMButtonVPos + 0.01f;
            Background.Append(MinerActive);

            MinerActiveText = new UIText("Copper");
            MinerActiveText.HAlign = 1f;
            MinerActiveText.VAlign = RoMButtonVPos + 0.01f;
            Background.Append(MinerActiveText);

            MidasActive = new UIImage(ActiveImage);
            MidasActive.HAlign = 0f;
            MidasActive.VAlign = RoMiButtonVPos - 0.01f;
            Background.Append(MidasActive);

            MidasActiveText = new UIText("Copper");
            MidasActiveText.HAlign = 1f;
            MidasActiveText.VAlign = RoMiButtonVPos - 0.01f;
            Background.Append(MidasActiveText);

            RitualOfMidasButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/RitualOfMidasButton"),"");
            RitualOfMidasButton.HAlign = 0.5f;
            RitualOfMidasButton.VAlign = RoMiButtonVPos;
            RitualOfMidasButton.Width.Set(ButtonSize.X, 0f);
            RitualOfMidasButton.Height.Set(ButtonSize.Y, 0f);
            RitualOfMidasButton.OnClick += new MouseEvent(MidasRitualButtonClicked);
            Background.Append(RitualOfMidasButton);

            CopperCoinButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/CopperCoinButton"), "");
            CopperCoinButton.VAlign = 1.5f;
            CopperCoinButton.HAlign = 0.2f;
            CopperCoinButton.Width.Set(32, 0f);
            CopperCoinButton.Height.Set(32, 0f);
            CopperCoinButton.OnClick += new MouseEvent(CopperCoinButtonClick);
            RitualOfMidasButton.Append(CopperCoinButton);

            SilverCoinButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/SilverCoinButton"), "");
            SilverCoinButton.VAlign = 1.5f;
            SilverCoinButton.HAlign = 0.4f;
            SilverCoinButton.Width.Set(32, 0f);
            SilverCoinButton.Height.Set(32, 0f);
            SilverCoinButton.OnClick += new MouseEvent(SilverCoinButtonClick);
            RitualOfMidasButton.Append(SilverCoinButton);

            GoldCoinButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/GoldCoinButton"), "");
            GoldCoinButton.VAlign = 1.5f;
            GoldCoinButton.HAlign = 0.6f;
            GoldCoinButton.Width.Set(32, 0f);
            GoldCoinButton.Height.Set(32, 0f);
            GoldCoinButton.OnClick += new MouseEvent(GoldCoinButtonClick);
            RitualOfMidasButton.Append(GoldCoinButton);

            PlatinumCoinButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/PlatinumCoinButton"), "");
            PlatinumCoinButton.VAlign = 1.5f;
            PlatinumCoinButton.HAlign = 0.8f;
            PlatinumCoinButton.Width.Set(32, 0f);
            PlatinumCoinButton.Height.Set(32, 0f);
            PlatinumCoinButton.OnClick += new MouseEvent(PlatinumCoinButtonClick);
            RitualOfMidasButton.Append(PlatinumCoinButton);

            _vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
            {
                HoverText = "Blood Crystal Slot",
                ValidItemFunc = item => item.IsAir || !item.IsAir && (GetModItem(item.type) is BloodCrystalSoul) && AltarTE.RoSoType == -69
            };
            _vanillaItemSlot.HAlign = 0.06f;
            _vanillaItemSlot.VAlign = RoSoButtonVPos;
            Background.Append(_vanillaItemSlot);

            BCSlot = new UIImage(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/BCSlot"));
            BCSlot.ImageScale = 1.45f;
            BCSlot.HAlign = 0.2f;
            BCSlot.VAlign = 0.2f;
            _vanillaItemSlot.Append(BCSlot);

            BCEject = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/EjectButton"), "Subtract from the spawn delay");
            BCEject.HAlign = 0.06f;
            BCEject.VAlign = RoSoButtonVPos + 0.087f;
            BCEject.Width.Set(112, 0f);
            BCEject.Height.Set(33, 0f);
            BCEject.OnClick += new MouseEvent(BCEjectButtonClicked);
            Background.Append(BCEject);

            RitualOfSoulsButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/RitualOfSoulsButton"), "Perform the ritual of souls, summon any of these five materials for 1 bp each");
            RitualOfSoulsButton.VAlign = RoSoButtonVPos;
            RitualOfSoulsButton.HAlign = 0.56f;
            RitualOfSoulsButton.Width.Set(ButtonSize.X, 0f);
            RitualOfSoulsButton.Height.Set(ButtonSize.Y, 0f);
            RitualOfSoulsButton.OnClick += new MouseEvent(SoulsRitualButtonClicked);
            Background.Append(RitualOfSoulsButton);

            SoulsActive = new UIImage(ActiveImage);
            SoulsActive.HAlign = 0f;
            SoulsActive.VAlign = RoSoButtonVPos - 0.003f;
            Background.Append(SoulsActive);

            SoulsActiveText = new UIText("None");
            SoulsActiveText.HAlign = 1f;
            SoulsActiveText.VAlign = RoSoButtonVPos - 0.01f;
            Background.Append(SoulsActiveText);

            SoulsNPC = new UIText("None");
            SoulsNPC.HAlign = 0.5f + 0.2f;
            SoulsNPC.VAlign = RoSoButtonVPos + 0.05f;
            Background.Append(SoulsNPC);

            SpawnDelay = new UIText("None");
            SpawnDelay.HAlign = 0.5f + 0.2f;
            SpawnDelay.VAlign = RoSoButtonVPos + 0.08f;
            Background.Append(SpawnDelay);

            AddDelayButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/ButtonPlus"), "Add to the spawn delay");
            AddDelayButton.VAlign = RoSoButtonVPos + 0.087f;
            AddDelayButton.HAlign = 0.72f + 0.16f;
            AddDelayButton.Width.Set(32, 0f);
            AddDelayButton.Height.Set(32, 0f);
            AddDelayButton.OnClick += new MouseEvent(AddDelayButtonClicked);
            Background.Append(AddDelayButton);

            SubtractDelayButton = new EntranceButton(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/ButtonMinus"), "Subtract from the spawn delay");
            SubtractDelayButton.VAlign = RoSoButtonVPos + 0.087f;
            SubtractDelayButton.HAlign = 0.28f + 0.18f;
            SubtractDelayButton.Width.Set(32, 0f);
            SubtractDelayButton.Height.Set(32, 0f);
            SubtractDelayButton.OnClick += new MouseEvent(SubtractDelayButtonClicked);
            Background.Append(SubtractDelayButton);
        }
        //public override void Update(GameTime gameTime)
        //{
        //    if (visible == true && Main.netMode == NetmodeID.MultiplayerClient)
        //    {
        //        ExamplePlayer p = Main.LocalPlayer.GetModPlayer<ExamplePlayer>();
        //        p.UIOpen = true;
        //    }
        //}

        //public static bool RepopulateItemSlot;
        public override void OnDeactivate()
        {
            base.OnDeactivate();
            if (!Main.gameMenu)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
            }

            //Item item = _vanillaItemSlot.Item;
            //if (!item.IsAir)
            //{
            //    AltarTE.BloodCrystal = item.Clone();
            //}
        }
        public override void OnActivate()
        {

            base.OnActivate();
        }
        string NPCNameSave;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!Main.playerInventory)
            {
                AltarTE.CloseUI();
            }
            if (AltarTE.RoSoType != -69)
            {
                NPC n = new NPC();
                n.SetDefaults(AltarTE.RoSoType);
                NPCNameSave = n.FullName;
                SoulsNPC.SetText(NPCNameSave);
                BCSlot.SetImage(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/BCSlotFilled"));
            }
            if (!_vanillaItemSlot.Item.IsAir)
            {
                if (_vanillaItemSlot.Item.ModItem is BloodCrystalSoul soul)
                {
                    if (soul.NPCID != -69)
                    {
                        AltarTE.RoSoType = soul.NPCID;
                        NPCNameSave = soul.NPCName;
                        SoulsNPC.SetText(NPCNameSave);
                        _vanillaItemSlot.Item = new Item();
                        //AltarTE.SendSoulsRitualInfo();
                        BCSlot.SetImage(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/BCSlotFilled"));
                    }
                    else
                    {
                        Main.NewText("Please insert a blood crystal");
                        return;
                    }
                }
            }

            //if (RepopulateItemSlot)
            //{
            //    if (!AltarTE.BloodCrystal.IsAir && _vanillaItemSlot.Item.IsAir)
            //    {
            //        _vanillaItemSlot.Item = AltarTE.BloodCrystal;
            //    }
            //    Main.NewText("Item: " + _vanillaItemSlot.Item.whoAmI);
            //    RepopulateItemSlot = false;
            //}
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            Texture2D ActiveImage = ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/ActiveButton").Value;
            Texture2D InActiveImage = ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/InactiveButton").Value;
            //for (int iterations = 0; iterations < VampireWorld.AltarBeingUsed.Count; iterations += 2)
            //{
            //    if (VampireWorld.AltarBeingUsed[iterations] == VampireWorld.MostRecentClick.X && VampireWorld.AltarBeingUsed[iterations + 1] == VampireWorld.MostRecentClick.Y)
            //    {
            //        identifier = iterations;
            //    }
            //}
            if (!AltarTE.RitualOfTheStone)
            {
                EarthActive.SetImage(InActiveImage);
            }
            else
            {
                EarthActive.SetImage(ActiveImage);
            }
            if (!AltarTE.RitualOfTheMiner)
            {
                MinerActive.SetImage(InActiveImage);
            }
            else
            {
                MinerActive.SetImage(ActiveImage);
            }
            if (!AltarTE.RitualOfMidas)
            {
                MidasActive.SetImage(InActiveImage);
            }
            else
            {
                MidasActive.SetImage(ActiveImage);
            }
            if (!AltarTE.RitualOfMidas)
            {
                MidasActive.SetImage(InActiveImage);
            }
            else
            {
                MidasActive.SetImage(ActiveImage);
            }
            if (!AltarTE.RitualOfSouls)
            {
                SoulsActive.SetImage(InActiveImage);
            }
            else
            {
                SoulsActive.SetImage(ActiveImage);
            }
            NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, AltarTE.ID);
            //if (Main.player[AltarTE.RitualOwner].GetModPlayer<VampPlayer>().BloodPoints < Cost && !HasBeenWarned)
            //{
            //    EarthText = "None";
            //    MinerText = "None";
            //    MidasText = "None";
            //    SoulsText = "None";
            //    AltarTE.RitualOfTheStone = false;
            //    AltarTE.RitualOfTheMiner = false;
            //    AltarTE.RitualOfMidas = false;
            //    AltarTE.RitualOfSouls = false;
            //    AltarTE.RoSoType = -69;
            //    Main.NewText("You have too few blood points!");
            //    AltarTE.SendStoneRitualInfo();
            //    AltarTE.SendMinerRitualInfo();
            //    AltarTE.SendMidasRitualInfo();
            //    HasBeenWarned = true;
            //}
            //if (HasBeenWarned && Main.player[AltarTE.RitualOwner].GetModPlayer<VampPlayer>().BloodPoints >= Cost)
            //{
            //    HasBeenWarned = false;
            //}
            if (AltarTE.RoSType == TileID.Stone)
            {
                EarthText = "Stone";
            }
            else if (AltarTE.RoSType == TileID.Mud)
            {
                EarthText = "Dirt"; 
            }
            else if(AltarTE.RoSType == TileID.Sand)
            {
                EarthText = "Sand";
            }
            else if(AltarTE.RoSType == TileID.Silt)
            {
                EarthText = "Silt";
            }
            else if (AltarTE.RoSType == TileID.SnowBlock)
            {
                EarthText = "Snow";
            }
            if(AltarTE.RoMinType == TileID.Tin)
            {
                MinerText = "Tin";
            }
            else if (AltarTE.RoMinType == TileID.Copper)
            {
                MinerText = "Copper";
            }
            else if (AltarTE.RoMinType == TileID.Lead)
            {
                MinerText = "Lead";
            }
            else if (AltarTE.RoMinType == TileID.Iron)
            {
                MinerText = "Iron";
            }
            else if (AltarTE.RoMinType == TileID.Silver)
            {
                MinerText = "Silver";
            }
            else if (AltarTE.RoMinType == TileID.Tungsten)
            {
                MinerText = "Tungsten";
            }
            else if (AltarTE.RoMinType == TileID.Gold)
            {
                MinerText = "Gold";
            }
            else if (AltarTE.RoMinType == TileID.Platinum)
            {
                MinerText = "Platinum";
            }
            else if (AltarTE.RoMinType == TileID.Demonite)
            {
                MinerText = "Demonite";
            }
            else if (AltarTE.RoMinType == TileID.Crimtane)
            {
                MinerText = "Crimtane";
            }
            else if (AltarTE.RoMinType == TileID.Hellstone)
            {
                MinerText = "Hellstone";
            }
            else if (AltarTE.RoMinType == TileID.Meteorite)
            {
                MinerText = "Meteorite";
            }
            else if (AltarTE.RoMinType == TileID.Cobalt)
            {
                MinerText = "Cobalt";
            }
            else if (AltarTE.RoMinType == TileID.Palladium)
            {
                MinerText = "Palladium";
            }
            else if (AltarTE.RoMinType == TileID.Titanium)
            {
                MinerText = "Titanium";
            }
            else if (AltarTE.RoMinType == TileID.Adamantite)
            {
                MinerText = "Adamantite";
            }
            else if (AltarTE.RoMinType == TileID.Mythril)
            {
                MinerText = "Mythril";
            }
            else if (AltarTE.RoMinType == TileID.Orichalcum)
            {
                MinerText = "Orichalcum";
            }
            else if (AltarTE.RoMinType == TileID.Chlorophyte)
            {
                MinerText = "Chlorophyte";
            }
            else if (AltarTE.RoMinType == TileID.LunarOre)
            {
                MinerText = "Luminite";
            }
            if(AltarTE.RoMidType == ItemID.CopperCoin)
            {
                MidasText = "Copper";
            }
            else if (AltarTE.RoMidType == ItemID.SilverCoin)
            {
                MidasText = "Silver";
            }
            else if (AltarTE.RoMidType == ItemID.GoldCoin)
            {
                MidasText = "Gold";
            }
            else if (AltarTE.RoMidType == ItemID.PlatinumCoin)
            {
                MidasText = "Platinum";
            }
            EarthActiveText.SetText(EarthText);
            MinerActiveText.SetText(MinerText);
            MidasActiveText.SetText(MidasText);
            SoulsActiveText.SetText(SoulsText);
            SoulsDelay = (Math.Truncate((AltarTE.RoSoDelay / 60f) * 100) / 100).ToString() + " Seconds";
            SpawnDelay.SetText(SoulsDelay);
            //OwnerText = "Owner: " + Main.player[AltarTE.RitualOwner].name;
            //OwnerActiveText.SetText(OwnerText);

            base.DrawSelf(spriteBatch);

            Main.hidePlayerCraftingMenu = false;

        }
         
        private void ClaimButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            AltarTE.RitualOwner = Main.LocalPlayer.whoAmI;
            //AltarTE.SyncOwnerSend();
        }
        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.CloseUI();
        }
        int Cost;
        private void StoneRitualButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RitualOfTheMiner)
            {
                Main.NewText("Please turn off the ritual of the Miner");
            }
            else  if(AltarTE.RitualOfMidas)
            {
                Main.NewText("Please turn off the ritual of Midas");
            }
            else if(AltarTE.RitualOfTheStone)
            {
                AltarTE.RitualOfTheStone = false;
                //AltarTE.SendStoneRitualInfo();
                AltarTE.ResetRitualSpace();
                Main.NewText("Stopped");
            }
            else if (Main.player[AltarTE.RitualOwner].GetModPlayer<VampPlayer>().BloodPoints < Cost)
            {
                Main.NewText("You have too few blood points!");
            }
            else
            {
                AltarTE.RitualOfTheStone = true;
                AltarTE.SendStoneRitualInfo();
                Main.NewText("Started" /*+ AltarTE.RitualOfTheStone*/);
            }
        }

        private void StoneButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoSType = TileID.Stone;
            Cost = 1;
        }

        private void DirtButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoSType = TileID.Mud;
            Cost = 1;
        }

        private void SandButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoSType = TileID.Sand;
            Cost = 1;
        }

        private void SiltButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoSType = TileID.Silt;
            Cost = 1;
        }

        private void SnowButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoSType = TileID.SnowBlock;
            Cost = 1;
        }

        private void MinerRitualButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RitualOfTheStone)
            {
                Main.NewText("Please turn off the ritual of the earth");
            }
            else if (AltarTE.RitualOfMidas)
            {
                Main.NewText("Please turn off the ritual of Midas");
            }
            else if(!NPC.downedMechBossAny)
            {
                Main.NewText("You need to kill a mechanical boss before performing this ritual");
            }
            else if (Main.player[AltarTE.RitualOwner].GetModPlayer<VampPlayer>().BloodPoints < Cost)
            {
                Main.NewText("You have too few blood points!");
            }
            else if(!AltarTE.RitualOfTheMiner)
            {
                AltarTE.RitualOfTheMiner = true;
                //AltarTE.SendMinerRitualInfo();
            }
            else
            {
                AltarTE.RitualOfTheMiner = false;
                //AltarTE.SendMinerRitualInfo();
                AltarTE.ResetRitualSpace();
            }
        }
        private void CopperTinButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RoMinType == TileID.Copper)
            {
                AltarTE.RoMinType = TileID.Tin;
            }
            else
            {
                AltarTE.RoMinType = TileID.Copper;
            }
            //AltarTE.SendMinerRitualInfo();
            AltarTE.ResetRitualSpace();
            Cost = 1;
        }
        private void IronLeadButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RoMinType == TileID.Iron)
            {
                AltarTE.RoMinType = TileID.Lead;
            }
            else
            {
                AltarTE.RoMinType = TileID.Iron;
            }
            //AltarTE.SendMinerRitualInfo();
            AltarTE.ResetRitualSpace();
            Cost = 2;
        }
        private void SilverTungstenButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RoMinType == TileID.Silver)
            {
                AltarTE.RoMinType = TileID.Tungsten;
            }
            else
            {
                AltarTE.RoMinType = TileID.Silver;
            }
            //AltarTE.SendMinerRitualInfo();
            AltarTE.ResetRitualSpace();
            Cost = 4;
        }
        private void GoldPlatinumButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RoMinType == TileID.Gold)
            {
                AltarTE.RoMinType = TileID.Platinum;
            }
            else
            {
                AltarTE.RoMinType = TileID.Gold;
            }
            //AltarTE.SendMinerRitualInfo();
            AltarTE.ResetRitualSpace();
            Cost = 8;
        }
        private void MeteoriteButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoMinType = TileID.Meteorite;
            Cost = 12;
        }
        private void DemoniteCrimtaneButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RoMinType == TileID.Demonite)
            {
                AltarTE.RoMinType = TileID.Crimtane;
            }
            else
            {
                AltarTE.RoMinType = TileID.Demonite;
            }
            //AltarTE.SendMinerRitualInfo();
            AltarTE.ResetRitualSpace();
            Cost = 16;
        }
        private void HellstoneButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoMinType = TileID.Hellstone;
            Cost = 24;
        }
        private void CobaltPalladiumButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!VampPlayer.HasHeldTier1)
            {
                Main.NewText("You need to mine palladium or cobalt before performing this ritual");
            }
            else
            {
                if (AltarTE.RoMinType == TileID.Cobalt)
                {
    
                    AltarTE.RoMinType = TileID.Palladium;
                }
                else
                {
    
                    AltarTE.RoMinType = TileID.Cobalt;
                }
                Cost = 50;
                //AltarTE.SendMinerRitualInfo();
                AltarTE.ResetRitualSpace();
            }
        }
        private void MythrilOrichalcumButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!VampPlayer.HasHeldTier2)
            {
                Main.NewText("You need to mine orichalcum or mythril before performing this ritual");
            }
            else
            {
                if (AltarTE.RoMinType == TileID.Mythril)
                {
                    AltarTE.RoMinType = TileID.Orichalcum;
                }
                else
                {
                    AltarTE.RoMinType = TileID.Mythril;
                }
                Cost = 75;
                //AltarTE.SendMinerRitualInfo();
                AltarTE.ResetRitualSpace();
            }
        }
        private void AdamantiteTitaniumButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!VampPlayer.HasHeldTier3)
            {
                Main.NewText("You need to mine titanium or adamantite before performing this ritual");
            }
            else
            {
                if (AltarTE.RoMinType == TileID.Adamantite)
                {
                    AltarTE.RoMinType = TileID.Titanium;
                }
                else
                {
                    AltarTE.RoMinType = TileID.Adamantite;
                }
                Cost = 100;
                //AltarTE.SendMinerRitualInfo();
                AltarTE.ResetRitualSpace();
            }
        }
        private void ChlorophyteButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!NPC.downedPlantBoss)
            {
                Main.NewText("You need to kill plantera before performing this ritual");
            }
            else
            {
                AltarTE.RoMinType = TileID.Chlorophyte;
                Cost = 150;
                //AltarTE.SendMinerRitualInfo();
                AltarTE.ResetRitualSpace();
            }
        }
        private void LuminiteButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!NPC.downedPlantBoss)
            {
                Main.NewText("You need to kill the moon lord before performing this ritual");
            }
            else
            {
                AltarTE.RoMinType = TileID.LunarOre;
                Cost = 300;
                //AltarTE.SendMinerRitualInfo();
                AltarTE.ResetRitualSpace();
            }
        }
        private void MidasRitualButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RitualOfTheStone)
            {
                Main.NewText("Please turn off the ritual of the earth");
            }
            else if (AltarTE.RitualOfTheMiner)
            {
                Main.NewText("Please turn off the ritual of the miner");
            }
            else if (Main.player[AltarTE.RitualOwner].GetModPlayer<VampPlayer>().BloodPoints < Cost)
            {
                Main.NewText("You have too few blood points!");
            }
            else if (!AltarTE.RitualOfMidas)
            {
                AltarTE.RitualOfMidas = true;
                //AltarTE.SendMidasRitualInfo();
            }
            else
            {
                AltarTE.RitualOfMidas = false;
                //AltarTE.SendMidasRitualInfo();
            }
        }
        private void CopperCoinButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoMidType = ItemID.CopperCoin;
            Cost = 1;
        }
        private void SilverCoinButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoMidType = ItemID.SilverCoin;
            Cost = 10;
        }
        private void GoldCoinButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoMidType = ItemID.GoldCoin;
            Cost = 100;
        }
        private void PlatinumCoinButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            AltarTE.RoMidType = ItemID.PlatinumCoin;
            Cost = 999;
        }
        int NPCID;
        private void SoulsRitualButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            VampPlayer AltarTEPlayer = Main.player[AltarTE.RitualOwner].GetModPlayer<VampPlayer>();
            //if(!_vanillaItemSlot.Item.IsAir)
            //{
            //    AltarTE.BloodCrystal = _vanillaItemSlot.Item;
            //}
            if (AltarTE.RitualOfTheStone)
            {
                Main.NewText("Please turn off the ritual of the earth");
            }
            else if (AltarTE.RitualOfTheMiner)
            {
                Main.NewText("Please turn off the ritual of the Miner");
            }
            else if (AltarTE.RitualOfMidas)
            {
                Main.NewText("Please turn off the ritual of Midas");
            }
            else if (AltarTEPlayer.BloodPoints <= 1)
            {
                Main.NewText("You have too few blood points!");
            }
            else if (!AltarTE.RitualOfSouls && AltarTE.RoSoType != -69)
            {
                AltarTE.RitualOfSouls = true;
                //AltarTE.SendSoulsRitualInfo();
                SoulsText = "Active";
                //if (!AltarTE.BloodCrystal.IsAir)
                //{
                //    if (_vanillaItemSlot.Item.modItem is BloodCrystalSoul soul)
                //    {
                //        if (soul.NPCID != -69)
                //        {
                //            AltarTE.RoSoType = soul.NPCID;
                //            NPCNameSave = soul.NPCName;
                //        }
                //        else
                //        {
                //            Main.NewText("Please insert a filled blood crystal");
                //            return;
                //        }
                //    }

                //    SoulsNPC.SetText(NPCNameSave);
                //    _vanillaItemSlot.Item = new Item();
                //}
                //else
                //{
                //    Main.NewText("Please insert a filled blood crystal");
                //}
            }
            else
            {
                AltarTE.RitualOfSouls = false;
                //AltarTE.SendSoulsRitualInfo();
            }
        }
        private void BCEjectButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RoSoType != -69)
            {
                AltarTE.SummonBloodCrystalProj();
                AltarTE.RoSoType = -69;
                AltarTE.RitualOfSouls = false;
                //AltarTE.SendSoulsRitualInfo();
                SoulsNPC.SetText("None");
                BCSlot.SetImage(ModContent.Request<Texture2D>("VKE/UI/BloodAltarTextures/BCSlot"));
                SoulsText = "None";
            }
            else
                Main.NewText("There is no blood crystal to eject");
        }
        private void AddDelayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RoSoDelay < 900)
                AltarTE.RoSoDelay += 10;
            else
                Main.NewText("You cannot increase the delay any further");
        }
        private void SubtractDelayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (AltarTE.RoSoDelay > 10)
                AltarTE.RoSoDelay -= 10;
            else
                Main.NewText("You cannot decrease the delay any further");
        }
    }
}