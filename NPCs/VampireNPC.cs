using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using VKE.Items.Armor;
using VKE.Items.Materials;
//using VKE.UI;
using static Terraria.ModLoader.ModContent;


namespace VKE.NPCs
{
    [AutoloadHead]
    public class VampireNPC : ModNPC
    {
        public bool SkinShopClicked;
        public override string Texture
        {
            get
            {
                return "VKE/NPCs/VampireNPC";
            }
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                              // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                              // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like) 
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Like)
                .SetBiomeAffection<CrimsonBiome>(AffectionLevel.Love) 
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Hate) 
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Hate) 
                .SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Nurse, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like) 
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Like) 
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Like) 
            ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 24;
            NPC.height = 46;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 9001;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("A mysterious entity that emanates power and a lust for blood"),
            });
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num = NPC.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 5);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (NPC.downedBoss2)
            {
                return true;
            }
            return false;
        }
        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Darick",
                "Francius",
                "Vlad",
                "Viktor",
                "Balitiu II"
            };
        }
        public override string GetChat()
        {
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            if (p.Given == false)
                return "Young one, interested in an old vampire relic?";
            if (p.Given == true && SkinShopClicked == false)
            {
                return "Sometimes I get the urge to drink from everyone here";
            }
            if (SkinShopClicked == true)
                return ("Sometimes I get the urge to drink from everyone here");
            else
                return "who pie is?";
        }
        public int timer;
        public override void SetChatButtons(ref string button, ref string button2)
        {
            timer++;
            if(timer == 300)
            {
                SkinShopClicked = false;
            }
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            if (p.Given == false)
            {
                button = "Sure?";
                button2 = "N-n-no thanks";
            }
            if (p.Given == true)
            {
                button = Language.GetTextValue("LegacyInterface.28");
                button2 = ("Open Skin Shop");
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            if (p.Given == false)
            {
                if (firstButton)
                {
                    Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_DropAsItem(), ModContent.ItemType<Items.Accessories.VampNecklace>());
                    p.Given = true;
                }
                else
                {
                    p.Given = false;
                    Main.npcChatText = "";
                }
            }
            if(p.Given == true)
            {
                if (firstButton)
                {
                    shop = true;
                }
                else
                {
                    SkinShopClicked = true;
                }
            }
            if (SkinShopClicked == true)
            {
                Main.playerInventory = true;
                // remove the chat window...
                Main.npcChatText = "DISABLED FOR NOW";
                // and start an instance of our UIState.
                //GetInstance<UIHelper>().VampireUserInterface.SetState(new UI.SkinInventory());
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {//THINK OF GOOD WAY TO MAKE RESOURCES INFINITE
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Misc.StartupBook>());
            //nextSlot++; 
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.RecipePages.AmmoCastRecipe>());
            //nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.RecipePages.IronCastRecipe>());
            //nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.RecipePages.SharpeningRodRecipe>());
            //nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.RecipePages.PlateRecipe>());
            //nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.RecipePages.AmmoSculptRecipe>());
            //nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.RecipePages.KnifeSculptRecipe>());
            //nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.RecipePages.SharpeningSculptRecipe>());
            //nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<PsionicHood>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<PsionicChestplate>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<PsionicLeggings>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<VampiricHelm>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<VampiricChestplate>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<VampiricGreaves>());
            nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<SpectralHood>());
            //nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<SpectralRobes>());
            //nextSlot++;
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<SpectralBoots>());
            //nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.VampNecklaces.VampNecklaceBee>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Materials.SacrificialDagger>());
            nextSlot++;
            if (NPC.downedSlimeKing)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Superglue>());
                nextSlot++;
            }
            if(NPC.downedBoss2)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<CorruptionCrystal>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<CorruptionShard>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<CrimsonCrystal>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<CrimsonShard>());
                nextSlot++;
            }
            if(NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<PlantFiber>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.Hoods.TransmutersHood>());
                nextSlot++;
                if (Main.eclipse)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<LivingTissue>());
                    nextSlot++;
                }
            }
            if(NPC.downedGolemBoss && Main.eclipse)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Materials.BrokenHeroKnives>());
                nextSlot++;
            }
            if(NPC.downedBoss3)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.PreKnives.SengosForgottenBlades>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.ExtraFinger>());
                nextSlot++;
            }
        }

        public override void OnKill()
        {
            base.OnKill();
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = Mod.Find<ModProjectile>("VampireKnifeProj").Type;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}