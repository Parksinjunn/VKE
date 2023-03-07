using IL.Terraria.GameContent;
using Microsoft.Xna.Framework;
using On.Terraria.GameContent;
using ParticleLibrary;
using Steamworks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using VKE.Buffs.HoodBuffs;
using VKE.Items.Accessories.Hoods;
using VKE.Items.HardKnives;
using VKE.Projectiles.HealProj;

namespace VKE
{
	public class VampPlayer : ModPlayer
	{
		public int BloodPoints;
		public int NumProj = 5;
		public int ExtraProj;
		public int NumCrafted;



        public bool VampNecklace = false;
        public int KillCount;
        public int NeckProgress;
        public float NeckAdd = 1f;
        public bool Given = false;
        public string KillText;
        public int DelayTimer;
        public float VampCurrent;
        public float HealAccMult = 1f;
        public float VampMax;
        public float VampDecreaseRate = 2f;
        public float VampDecSlow = 1f;
        public double DelayAdd;

        public bool SacrificialDebuff;
        public bool PenetratingPoison;
        public bool Bandaged;

        public bool SpectreGlovesOn;
        public bool Transform;
        public bool HasTabletEquipped;
        bool DoubleTapStart;
        int DoubleTapTimer;

        public bool pyroAccessoryPrevious;
        public bool pyroAccessory;
        public bool pyroHideVanity;
        public bool pyroForceVanity;
        public bool pyroPower;
        public bool pyro = false;

        public bool dPyroAccessoryPrevious;
        public bool dPyroAccessory;
        public bool dPyroHideVanity;
        public bool dPyroForceVanity;
        public bool dPyroPower;
        public bool dPyro = false;

        public bool TransmuterAccessoryPrevious;
        public bool TransmuterAccessory;
        public bool TransmuterHideVanity;
        public bool TransmuterForceVanity;
        public bool TransmuterPower;
        public bool Transmuter = false;

        public bool InvokerAccessoryPrevious;
        public bool InvokerAccessory;
        public bool InvokerHideVanity;
        public bool InvokerForceVanity;
        public bool InvokerPower;
        public bool Invoker = false;

        public bool MageAccessoryPrevious;
        public bool MageAccessory;
        public bool MageHideVanity;
        public bool MageForceVanity;
        public bool MagePower;
        public bool Mage = false;

        public bool TechnomancerAccessoryPrevious;
        public bool TechnomancerAccessory;
        public bool TechnomancerHideVanity;
        public bool TechnomancerForceVanity;
        public bool TechnomancerPower;
        public bool Technomancer = false;

        public bool PartyAccessoryPrevious;
        public bool PartyAccessory;
        public bool PartyHideVanity;
        public bool PartyForceVanity;
        public bool PartyPower;
        public bool Party = false;

        public bool ShamanAccessoryPrevious;
        public bool ShamanAccessory;
        public bool ShamanHideVanity;
        public bool ShamanForceVanity;
        public bool ShamanPower;
        public bool Shaman = false;

        public bool WitchDoctorAccessoryPrevious;
        public bool WitchDoctorAccessory;
        public bool WitchDoctorHideVanity;
        public bool WitchDoctorForceVanity;
        public bool WitchDoctorPower;
        public bool WitchDoctor = false;
        public bool ShrunkenHead = false;

        public bool PsionicArmorSet;
        public bool PsionicPower;
        public bool VampiricArmorSet;
        public float VampiricSetScaler = 1f;
        public float DefenseReflectChance = 0f;
        public int DefenseExtraLives = 0;

        public bool nullified = false;
        public bool HoodKeyPressed = false;
        public bool HoodIsVisible = false;
        public bool AltFuncChannel;

        public static bool HasHeldTier1;
        public static bool HasHeldTier2;
        public static bool HasHeldTier3;

        public override void ResetEffects()
		{
			SacrificialDebuff = Bandaged = PenetratingPoison = false;
            SpectreGlovesOn = false;
            HasTabletEquipped = false;

            NumProj = 5;
            ExtraProj = 0;
            VampDecreaseRate = 2f;
            VampDecSlow = 1f;
            VampNecklace = false;
            HealAccMult = 1f * (VampKnives.ConfigHealAmntMult + NeckAdd);
            NeckAdd = 0f;

            //Hood Resets
            pyroAccessoryPrevious = pyroAccessory;
            pyroAccessory = pyroHideVanity = pyroForceVanity = pyroPower = pyro = false;
            dPyroAccessoryPrevious = dPyroAccessory;
            dPyroAccessory = dPyroHideVanity = dPyroForceVanity = dPyroPower = dPyro = false;
            TransmuterAccessoryPrevious = TransmuterAccessory;
            TransmuterAccessory = TransmuterHideVanity = TransmuterForceVanity = Transmuter = TransmuterPower = false;
            InvokerAccessoryPrevious = InvokerAccessory;
            InvokerAccessory = InvokerHideVanity = InvokerForceVanity = Invoker = InvokerPower = false;
            TechnomancerAccessoryPrevious = TechnomancerAccessory;
            TechnomancerAccessory = TechnomancerHideVanity = TechnomancerForceVanity = Technomancer = TechnomancerPower = false;
            PartyAccessoryPrevious = PartyAccessory;
            PartyAccessory = PartyHideVanity = PartyForceVanity = Party = PartyPower = false;
            ShamanAccessoryPrevious = ShamanAccessory;
            ShamanAccessory = ShamanHideVanity = ShamanForceVanity = Shaman = ShamanPower = false;
            WitchDoctorAccessoryPrevious = WitchDoctorAccessory;
            WitchDoctorAccessory = WitchDoctorHideVanity = WitchDoctorForceVanity = WitchDoctor = ShrunkenHead = WitchDoctorPower = false;
            MageAccessoryPrevious = MageAccessory;
            MageAccessory = MageHideVanity = MageForceVanity = Mage = MagePower = false;

            DefenseExtraLives = 0;
            base.ResetEffects();
		}
        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            //var staticVars = new List<string>();
            //RitualListSize = RitualItemsList.Count;
            //if (HasHeldTier3)
            //{
            //    staticVars.Add("Tier3");
            //}
            //if (HasHeldTier2)
            //{
            //    staticVars.Add("Tier2");
            //}
            //if (HasHeldTier1)
            //{
            //    staticVars.Add("Tier1");
            //}
            tag["Given"] = Given;
            tag["NeckAdd"] = NeckAdd;
            if (HasHeldTier3)
            {
                tag["Tier3"] = HasHeldTier3;
            }
            if (HasHeldTier2)
            {
                tag["Tier2"] = HasHeldTier2;
            }
            if (HasHeldTier1)
            {
                tag["Tier1"] = HasHeldTier1;
            }
            //tag.Add("NeckProgress", NeckProgress);
            //tag.Add("Given", Given);
            //tag.Add("NeckAdd", NeckAdd);
            //tag.Add("KillText", KillText);
            //tag.Add("NumCrafted", NumCrafted);
            //tag.Add("BloodPoints", BloodPoints);
            //tag.Add("staticVars", staticVars);
            //if (!UpgradeItem.IsAir)
            //    tag.Add("UpgradeItem", UpgradeItem);

        }
        public override void LoadData(TagCompound tag)
        {
            //var staticVars = tag.GetList<string>("staticVars");

            //NeckProgress = tag.GetInt("NeckProgress");
            Given = tag.GetBool("Given");
            NeckAdd = tag.GetFloat("NeckAdd");
            HasHeldTier1 = tag.GetBool("Tier1");
            HasHeldTier2 = tag.GetBool("Tier2");
            HasHeldTier3 = tag.GetBool("Tier3");
            //KillText = tag.GetString("KillText");
            //NumCrafted = tag.GetInt("NumCrafted");
            //BloodPoints = tag.GetInt("BloodPoints");
            //UpgradeItem = tag.Get<Item>("UpgradeItem");
            //if (UpgradeItem == null)
            //{
            //    UpgradeItem = new Item();
            //}
            //RitualListSize = tag.GetInt("RitualListSize");
            //for (int i = 0; i < RitualListSize; i += 2)
            //{
            //    RitualItemsList[i] = tag.Get<Item>("RitualItem" + i);
            //    if(RitualItemsList[i] == null)
            //    {
            //        RitualItemsList[i] = new Item();
            //    }
            //}
            //HasHeldTier1 = staticVars.Contains("Tier1");
            //HasHeldTier2 = staticVars.Contains("Tier2");
            //HasHeldTier3 = staticVars.Contains("Tier3");
            //if (staticVars.Contains("Tier3"))
            //{
            //    HasHeldTier3 = staticVars.Contains("Tier3");
            //    HasHeldTier2 = staticVars.Contains("Tier3");
            //    HasHeldTier1 = staticVars.Contains("Tier3");
            //}
            //else if (staticVars.Contains("Tier3"))
            //{
            //    HasHeldTier3 = staticVars.Contains("Tier2");
            //    HasHeldTier2 = staticVars.Contains("Tier2");
            //    HasHeldTier1 = staticVars.Contains("Tier2");
            //}
            //else if (staticVars.Contains("Tier1"))
            //{
            //    HasHeldTier3 = staticVars.Contains("Tier1");
            //    HasHeldTier2 = staticVars.Contains("Tier2");
            //    HasHeldTier1 = staticVars.Contains("Tier2");
            //}
        }
        public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
        {
            base.ModifyDrawLayerOrdering(positions);
        }
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if(SpectreGlovesOn)
                drawInfo.weaponOverFrontArm = true;
            base.HideDrawLayers(drawInfo);
        }
        //public override void PreUpdate()
        //{
        //    ParticleManager.UpdateParticles();

        //    base.PreUpdate();
        //}

        public override void UpdateDead()
        {
            PenetratingPoison = false;
            //SengosCurse = false;
            //TitaniumDefenseBuff = false;
            //ShroomiteBuff = false;
            //SupportArmorBuff = false;
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (VampiricArmorSet && Main.rand.Next(0, 1001) <= 2 * VampiricSetScaler)
            {
                for (int NPCID = 0; NPCID < Main.maxNPCs; NPCID++)
                {
                    float shootToX = Main.npc[NPCID].position.X + (float)Main.npc[NPCID].width * 0.5f - Player.Center.X;
                    float shootToY = Main.npc[NPCID].position.Y - Player.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                    if (Main.npc[NPCID].CanBeChasedBy() && distance < 1000f)
                    {
                        int LifeStealRandom = Main.rand.Next(40, 90);
                        Player.ApplyDamageToNPC(Main.npc[NPCID], LifeStealRandom, 0f, 0, false);
                        Projectile.NewProjectile(npc.GetSource_FromThis(), Main.npc[NPCID].position, new Vector2(0f, 0f), ModContent.ProjectileType<HealProj>(), LifeStealRandom, 0f, Player.whoAmI);
                    }
                }
            }
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (VampiricArmorSet && Main.rand.Next(0, 1001) <= (int)(2f * VampiricSetScaler))
            {
                for (int NPCID = 0; NPCID < Main.maxNPCs; NPCID++)
                {
                    float shootToX = Main.npc[NPCID].position.X + (float)Main.npc[NPCID].width * 0.5f - Player.Center.X;
                    float shootToY = Main.npc[NPCID].position.Y - Player.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                    if (Main.npc[NPCID].CanBeChasedBy() && distance < 1000f)
                    {
                        int LifeStealRandom = Main.rand.Next(40, 90);
                        Player.ApplyDamageToNPC(Main.npc[NPCID], LifeStealRandom, 0f, 0, false);
                        Projectile.NewProjectile(proj.GetSource_FromThis(), Main.npc[NPCID].position, new Vector2(0f, 0f), ModContent.ProjectileType<HealProj>(), LifeStealRandom, 0f, Player.whoAmI);
                    }
                }
            }
        }
        public override void UpdateEquips()
        {
            VampKnives v = new VampKnives();
            //if (Player.miscEquips[0].type != ModContent.ItemType<Items.VtuberItems.NottomMinionItem>())
            //{
            //    Player.ClearBuff(ModContent.BuffType<Buffs.VTuberBuffs.NottomBuff>());
            //}
            //if (MouseMilkBuff)
            //{
            //    Player.GetDamage(DamageClass.Generic) *= 1.4f;
            //    Player.maxRunSpeed *= 1.2f;
            //    Player.accRunSpeed *= 1.2f;
            //    Player.statDefense += 5;
            //}
            //if (player.controlJump && MelBoots)
            //{
            //    player.velocity.Y = player.velocity.Y - 0.1f * player.gravDir;
            //    if (player.gravDir == 1f)
            //    {
            //        if (player.velocity.Y > 0f)
            //        {
            //            player.velocity.Y = player.velocity.Y - 0.5f;
            //        }
            //        else if ((double)player.velocity.Y > (0.0 - (double)JumpSpeed) * 0.5)
            //        {
            //            player.velocity.Y = player.velocity.Y - 0.1f;
            //        }
            //        if (player.velocity.Y < (0f - JumpSpeed) * 1.5f)
            //        {
            //            player.velocity.Y = (0f - JumpSpeed) * 1.5f;
            //        }
            //    }
            //    else
            //    {
            //        if (player.velocity.Y < 0f)
            //        {
            //            player.velocity.Y = player.velocity.Y + 0.5f;
            //        }
            //        else if ((double)player.velocity.Y < (double)JumpSpeed * 0.5)
            //        {
            //            player.velocity.Y = player.velocity.Y + 0.1f;
            //        }
            //        if (player.velocity.Y > JumpSpeed * 1.5f)
            //        {
            //            player.velocity.Y = JumpSpeed * 1.5f;
            //        }
            //    }
            //}

            //if (SendPackage)
            //{
            //    SendPackage = false;
            //    ModPacket RitualClientSend = mod.GetPacket();
            //    RitualClientSend.Write(VampKnives.RitualsRecieve);
            //    RitualClientSend.Write(VampireWorld.AltarBeingUsed.Count);
            //    RitualClientSend.Write(player.whoAmI);
            //    for (int g = 0; g < VampireWorld.AltarBeingUsed.Count; g++)
            //    {
            //        //RitualClientSend.Write(VampireWorld.AltarBeingUsed[g]);
            //        RitualClientSend.Write(VampireWorld.RitualOfTheStone[g]);
            //        RitualClientSend.Write(VampireWorld.RoEType[g]);
            //        RitualClientSend.Write(VampireWorld.RitualOfTheMiner[g]);
            //        RitualClientSend.Write(VampireWorld.RoMType[g]);
            //        RitualClientSend.Write(VampireWorld.RitualOfMidas[g]);
            //        RitualClientSend.Write(VampireWorld.RoMiType[g]);
            //        RitualClientSend.Write(VampireWorld.RitualOfSouls[g]);
            //        RitualClientSend.Write(VampireWorld.AltarOwner[g]); 
            //    }
            //    RitualClientSend.Send();
            //}

            //if (SupportArmorSetBuff == true)
            //{
            //    RunSupportTimer = true;
            //}
            //if (RunSupportTimer)
            //{
            //    if (SupportTimer < SupportTime && SupportArmorSetBuffCount < 6)
            //    {
            //        SupportTimer++;
            //    }
            //    else if (SupportTimer >= SupportTime && SupportArmorSetBuffCount < 6)
            //    {
            //        SupportArmorSetBuffCount++;
            //        SupportTime += 50 * SupportArmorSetBuffCount;
            //        SupportTimer = 0;
            //    }
            //}
            //else
            //{
            //    RunSupportTimer = false;
            //    SupportTimer = 0;
            //    SupportArmorSetBuffCount = 0;
            //}
            //if (StartStoreResetTimer && HasSupportBuff == false)
            //{
            //    StoreResetTimer++;
            //    if (StoreResetTimer >= 600)
            //    {
            //        BuffCountStore = 0;
            //        StoreResetTimer = 0;
            //        StartStoreResetTimer = false;
            //    }
            //}
            //if (SupportArmorBuff && BuffCountStore > 0)
            //{
            //    Player.maxRunSpeed *= (1.5f + (float)Math.Log(BuffCountStore));
            //    Player.accRunSpeed *= (1.5f + (float)Math.Log(BuffCountStore));
            //    Player.lifeRegenTime += (int)(15 * BuffCountStore);
            //    Player.GetDamage(DamageClass.Generic) *= (1f + (float)Math.Log(BuffCountStore));
            //    Player.statDefense += (2 + (int)BuffCountStore);
            //}
            //if (SupportArmorSetBuffCount >= 1 && Transform == false)
            //{
            //    int DustType = 67;
            //    float VisorScale = 0.5f;
            //    Color VisorColor = new Color(0, 251, 255);
            //    if (SupportArmorSetBuffCount == 1)
            //    {
            //        VisorAlpha = 220;
            //    }
            //    if (SupportArmorSetBuffCount == 2 && VisualRun == true)
            //    {
            //        VisorAlpha = 180;
            //        OvalDust(new Vector2(Player.Center.X, Player.Center.Y - 13), 1.5f, 0.5f, VisorColor, DustType, 1.2f, true);
            //        VisualRun = false;
            //    }
            //    else if (SupportArmorSetBuffCount == 3 && VisualRun == false)
            //    {
            //        VisorAlpha = 140;
            //        OvalDust(new Vector2(Player.Center.X, Player.Center.Y - 13), 1.5f, 0.5f, VisorColor, DustType, 1.2f, true);
            //        VisualRun = true;
            //    }
            //    else if (SupportArmorSetBuffCount == 4 && VisualRun == true)
            //    {
            //        VisorAlpha = 100;
            //        OvalDust(new Vector2(Player.Center.X, Player.Center.Y - 13), 1.5f, 0.5f, VisorColor, DustType, 1.2f, true);
            //        VisualRun = false;
            //    }
            //    else if (SupportArmorSetBuffCount == 5 && VisualRun == false)
            //    {
            //        VisorAlpha = 60;
            //        OvalDust(new Vector2(Player.Center.X, Player.Center.Y - 13), 1.5f, 0.5f, VisorColor, DustType, 1.2f, true);
            //        VisualRun = true;
            //    }
            //    else if (SupportArmorSetBuffCount == 6 && VisualRun == true)
            //    {
            //        VisorAlpha = 0;
            //        OvalDust(new Vector2(Player.Center.X, Player.Center.Y - 13), 1.5f, 0.5f, VisorColor, DustType, 1.2f, true);
            //        VisualRun = false;
            //    }
            //    if (Player.direction == 1)
            //    {
            //        for (int x = 0; x < 5; x++)
            //        {
            //            if (x >= 1 && x < 3)
            //            {
            //                int VisorDust = Dust.NewDust(new Vector2((Player.Center.X + (4 - (2 * x))), Player.Center.Y - 12), 0, 0, DustType, 0f, 0f, VisorAlpha, VisorColor, VisorScale);
            //                Main.dust[VisorDust].noGravity = true;
            //                Main.dust[VisorDust].velocity *= 0;
            //                Main.dust[VisorDust].shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
            //            }
            //            else
            //            {
            //                int VisorDust = Dust.NewDust(new Vector2((Player.Center.X + (4 - (2 * x))), Player.Center.Y - 14), 0, 0, DustType, 0f, 0f, VisorAlpha, VisorColor, VisorScale);
            //                Main.dust[VisorDust].noGravity = true;
            //                Main.dust[VisorDust].velocity *= 0;
            //                Main.dust[VisorDust].shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int x = 0; x < 5; x++)
            //        {
            //            if (x >= 1 && x < 3)
            //            {
            //                int VisorDust = Dust.NewDust(new Vector2((Player.Center.X - (12 - (2 * x))), Player.Center.Y - 12), 0, 0, DustType, 0f, 0f, VisorAlpha, VisorColor, VisorScale);
            //                Main.dust[VisorDust].noGravity = true;
            //                Main.dust[VisorDust].velocity *= 0;
            //                Main.dust[VisorDust].shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
            //            }
            //            else
            //            {
            //                int VisorDust = Dust.NewDust(new Vector2((Player.Center.X - (12 - (2 * x))), Player.Center.Y - 14), 0, 0, DustType, 0f, 0f, VisorAlpha, VisorColor, VisorScale);
            //                Main.dust[VisorDust].noGravity = true;
            //                Main.dust[VisorDust].velocity *= 0;
            //                Main.dust[VisorDust].shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
            //            }
            //        }
            //    }
            //}
            //if (NumCrafted == 20 && !FirstEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    FirstEnhancedText = true;
            //}
            //if (NumCrafted == 50 && !SecondEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    SecondEnhancedText = true;
            //}
            //if (NumCrafted == 100 && !ThirdEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    ThirdEnhancedText = true;
            //}
            //if (NumCrafted == 150 && !FourthEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    FourthEnhancedText = true;
            //}
            //if (NumCrafted == 225 && !FifthEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    FifthEnhancedText = true;
            //}
            //if (NumCrafted == 300 && !SixthEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    SixthEnhancedText = true;
            //}
            //if (NumCrafted == 400 && !SeventhEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    SeventhEnhancedText = true;
            //}
            //if (NumCrafted == 500 && !EigthEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    EigthEnhancedText = true;
            //}
            //if (NumCrafted == 1000 && !NinthEnhancedText)
            //{
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), "Ammo Crafting Enhanced!", true);
            //    Main.NewText("You've crafted ammo a thousand times, you've mastered the art of crafting knife ammo", 180, 0, 0);
            //    NinthEnhancedText = true;
            //}
            //if (IsSupportKeyPressed)
            //{
            //    Player.AddBuff(ModContent.BuffType<Buffs.TrueSupportDebuff>(), 60, true);
            //}
            if (HoodKeyPressed == true && pyroAccessory)
            {
                Player.AddBuff(ModContent.BuffType<PyroHoodBuff>(), 60, true);
            }
            if (HoodKeyPressed == true && dPyroAccessory)
            {
                Player.AddBuff(ModContent.BuffType<DPyroHoodBuff>(), 60, true);
            }
            if (HoodKeyPressed == true && TransmuterAccessory)
            {
                Player.AddBuff(ModContent.BuffType<TransmuterHoodBuff>(), 60, true);
            }
            if (HoodKeyPressed == true && InvokerAccessory)
            {
                Player.AddBuff(ModContent.BuffType<InvokerHoodBuff>(), 60, true);
            }
            if (HoodKeyPressed == true && TechnomancerAccessory)
            {
                Player.AddBuff(ModContent.BuffType<TechnomancerHoodBuff>(), 60, true);
            }
            if (HoodKeyPressed == true && PartyAccessory)
            {
                Player.AddBuff(ModContent.BuffType<PartyHoodBuff>(), 60, true);
            }
            if (HoodKeyPressed == true && ShamanAccessory)
            {
                Player.AddBuff(ModContent.BuffType<ShamanHoodBuff>(), 60, true);
            }
            if (HoodKeyPressed == true && WitchDoctorAccessory)
            {
                Player.AddBuff(ModContent.BuffType<WitchDoctorHoodBuff>(), 60, true);
            }
            if (HoodKeyPressed == true && MageAccessory)
            {
                Player.AddBuff(ModContent.BuffType<MageHoodBuff>(), 60, true);
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    dust = Main.dust[Terraria.Dust.NewDust(new Vector2(Player.position.X - (Player.width / 2), Player.position.Y - (Player.height)), 47, 0, 226, 0f, 7f, 3, new Color(255, 255, 255), 0.6f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(22, Main.LocalPlayer);
                }
            }
            if (HasTabletEquipped == true /*&& Transform == true*/ && (Player.bodyFrame.Y == 560 || Player.bodyFrame.Y == 168))
            {
                SoundEngine.PlaySound(SoundID.Item32 with { Volume = 0.5f}, Player.position);
            }
            if (HasTabletEquipped == false)
            {
                Transform = false;
                DoubleTapTimer = 0;
                DoubleTapStart = false;
            }
        }

        public override void FrameEffects()
        {
            if ((pyroPower || pyroForceVanity) && !pyroHideVanity)
            {
                Main.NewText("PYRO ACTIVE");
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<PyromancersHood>().Name, EquipType.Head);
            }
            if ((dPyroPower || dPyroForceVanity) && !dPyroHideVanity)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<DarkPyromancersHood>().Name, EquipType.Head);
            }
            if ((TransmuterPower || TransmuterForceVanity) && !TransmuterHideVanity)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<TransmutersHood>().Name, EquipType.Head);
            }
            if ((InvokerPower || InvokerForceVanity) && !InvokerHideVanity)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<InvokersHood>().Name, EquipType.Head);
            }
            if ((TechnomancerPower || TechnomancerForceVanity) && !TechnomancerHideVanity)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<TechnomancersHood>().Name, EquipType.Head);
            }
            if ((PartyPower || PartyForceVanity) && !PartyHideVanity)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<PartyHood>().Name, EquipType.Head);
            }
            if ((ShamanPower || ShamanForceVanity) && !ShamanHideVanity)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<ShamansHood>().Name, EquipType.Head);
            }
            if ((WitchDoctorPower || WitchDoctorForceVanity) && !WitchDoctorHideVanity)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<WitchDoctorHood>().Name, EquipType.Head);
            }
            if ((MagePower || MageForceVanity) && !MageHideVanity)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, ModContent.GetInstance<MagesHood>().Name, EquipType.Head);
            }
            if (HasTabletEquipped && Transform)
            {
                int equipSlotBodyAlt = EquipLoader.GetEquipSlot(Mod, "BatTransformHidden_Body", EquipType.Body);
                int equipSlotLegsAlt = EquipLoader.GetEquipSlot(Mod, "BatTransformHidden_Legs", EquipType.Legs);

                ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBodyAlt] = true;
                ArmorIDs.Body.Sets.HidesArms[equipSlotBodyAlt] = true;
                ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegsAlt] = true;

                Player.body = EquipLoader.GetEquipSlot(Mod, "BatTransformHidden_Body", EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, "BatTransformHidden_Legs", EquipType.Legs);

                if (Player.bodyFrameCounter == 0 && (Player.velocity.X < 0 || Player.velocity.X > 0 || Player.velocity.Y < 0 || Player.velocity.Y > 0))
                {
                    int equipSlotHeadAlt = EquipLoader.GetEquipSlot(Mod, "BatTransformHidden", EquipType.Head);
                    ArmorIDs.Head.Sets.DrawHead[equipSlotHeadAlt] = false;

                    Player.head = EquipLoader.GetEquipSlot(Mod, "BatTransformHidden", EquipType.Head);
                    Player.wings = EquipLoader.GetEquipSlot(Mod, "BatFlyMovement", EquipType.Wings);

                    int num74 = 4;
                    if (Player.direction == 1)
                    {
                        num74 = -40;
                    }
                    int num75 = Dust.NewDust(new Vector2(Player.position.X + (float)(Player.width / 2) + (float)num74, Player.position.Y + (float)(Player.height / 2) - 15f), 30, 30, 182, 0f, 0f, 100, Color.Red, 0.5f);
                    Main.dust[num75].noGravity = true;
                    Dust dust3 = Main.dust[num75];
                    dust3.velocity *= 0.3f;
                    Lighting.AddLight(new Vector2(Player.position.X + (float)(Player.width / 2) + (float)num74, Player.position.Y + (float)(Player.height / 2) - 15f), new Vector3(0.45f, 0.02f, 0.02f));
                }
                else
                {
                    int equipSlotHeadAlt = EquipLoader.GetEquipSlot(Mod, "BatTransform", EquipType.Head);
                    ArmorIDs.Head.Sets.DrawHead[equipSlotHeadAlt] = false;
                    Player.head = EquipLoader.GetEquipSlot(Mod, "BatTransform", EquipType.Head);
                    Player.wings = EquipLoader.GetEquipSlot(Mod, "BatWingsHidden", EquipType.Wings);
                }
            }
            else if(HasTabletEquipped && !Transform)
            {
                Player.wings = EquipLoader.GetEquipSlot(Mod, "BatFlyMovement", EquipType.Wings);
                if(Player.velocity.Y == 0)
                {
                    Player.wingFrame = 1;
                }
            }
            //if (VeiTransform && !HasTabletEquipped && !NyanTransform && !MouseTransform)
            //{
            //    Player.head = EquipLoader.GetEquipSlot(Mod, "VeiTransformHead", EquipType.Head);
            //    Player.body = EquipLoader.GetEquipSlot(Mod, "VeiTransformBody", EquipType.Body);
            //    Player.legs = EquipLoader.GetEquipSlot(Mod, "VeiTransformLegs", EquipType.Legs);
            //}
            //if (NyanTransform && !VeiTransform && !HasTabletEquipped && !MouseTransform)
            //{
            //    Player.head = EquipLoader.GetEquipSlot(Mod, "NyanTransformHead", EquipType.Head);
            //    Player.body = EquipLoader.GetEquipSlot(Mod, "NyanTransformBody", EquipType.Body);
            //    Player.legs = EquipLoader.GetEquipSlot(Mod, "NyanTransformLegs", EquipType.Legs);
            //}
            //if (MouseTransform && !VeiTransform && !NyanTransform && !HasTabletEquipped)
            //{
            //    Player.head = EquipLoader.GetEquipSlot(Mod, "MouseTransformHead", EquipType.Head);
            //    Player.body = EquipLoader.GetEquipSlot(Mod, "MouseTransformBody", EquipType.Body);
            //    Player.legs = EquipLoader.GetEquipSlot(Mod, "MouseTransformLegs", EquipType.Legs);
            //}
            //if(VeiWingsEquipped)
            //{
            //    if (player.bodyFrameCounter == 0 && (player.velocity.X < 0 || player.velocity.X > 0 || player.velocity.Y < 0 || player.velocity.Y > 0))
            //    {
            //        player.wings = mod.GetEquipSlot("BatFlyMovement", EquipType.Wings);
            //        int num74 = 4;
            //        if (player.direction == 1)
            //        {
            //            num74 = -40;
            //        }
            //        int num75 = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)num74, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, 182, 0f, 0f, 100, Color.Red, 0.5f);
            //        Main.dust[num75].noGravity = true;
            //        Dust dust3 = Main.dust[num75];
            //        dust3.velocity *= 0.3f;
            //        Lighting.AddLight(new Vector2(player.position.X + (float)(player.width / 2) + (float)num74, player.position.Y + (float)(player.height / 2) - 15f), new Vector3(0.45f, 0.02f, 0.02f));
            //    }
            //    else
            //    {
            //        player.wings = mod.GetEquipSlot("BatWingsHidden", EquipType.Wings);
            //    }
            //}
            if (nullified)
            {
                //Nullify();
            }
        }

        public override bool PreItemCheck()
        {
            if (Player.altFunctionUse == 2)
            {
                AltFuncChannel = true;
            }
            return base.PreItemCheck();
        }
        public override void SetControls()
        {
            if (Mage == true && HoodKeyPressed == true)
            {
                Player.controlRight = false;
                Player.controlLeft = false;
                Player.controlJump = false;
            }
            base.SetControls();
        }


        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (DoubleTapStart && HasTabletEquipped)
            {
                DoubleTapTimer++;
                if (DoubleTapTimer > 15)
                {
                    DoubleTapStart = false;
                    DoubleTapTimer = 0;
                }
            }
            if (VampKnives.HoodUpDownHotkey.JustPressed)
            {
                if (HoodKeyPressed == false)
                {
                    HoodKeyPressed = true;
                    HoodIsVisible = true;
                }
                else
                {
                    HoodKeyPressed = false;
                    HoodIsVisible = false;
                }
            }
            if (VampKnives.VampDashHotKey.JustPressed && HasTabletEquipped)
            {
                DoubleTapStart = true;
            }
            if (DoubleTapTimer > 1 && VampKnives.VampDashHotKey.JustPressed)
            {
                if (Transform == false)
                {
                    Transform = true;
                    DoubleTapStart = false;
                    DoubleTapTimer = 0;
                }
                else if (Transform == true)
                {
                    Transform = false;
                    DoubleTapStart = false;
                    DoubleTapTimer = 0;
                }
            }
        }
        public static void OvalDust(Vector2 position, float width, float height, Color color, int DustType, float size, bool scattered = false, bool NoGravity = true, bool IsCut = false, Vector2? CutPositionsAndVelocityMult = null)
        {
            float VelocityX = 0;
            float VelocityY = 0;
            float End1;
            float End2;
            float End1Final = 0;
            float End2Final = 0;
            float a = width;
            float b = height;
            float Circle = 360f;
            float DivisorFactor = 360f;
            float Divisor = Circle / DivisorFactor;
            float FirstLower = 0;
            float FirstUpper = 90f / Divisor;
            float SecondLower = 270f / Divisor;
            float SecondUpper = 360f / Divisor;
            float ThirdLower = 90f / Divisor;
            float ThirdUpper = 270f / Divisor;
            float HalfOfFirstUpper = FirstUpper / 2;
            float DoubleOfFirstUpper = FirstUpper * 2;
            int ScatteredWeight = 0;
            //bool IsCut = false;
            //if(CutPositions != null)
            //{
            //    IsCut = true;
            //}
            if (scattered)
            {
                ScatteredWeight = 96;
            }

            for (int iteration = 0; iteration < SecondUpper; iteration += 12)
            {
                if (Main.rand.Next(1, 100) > ScatteredWeight)
                {
                    if (IsCut)
                    {
                        End1 = CutPositionsAndVelocityMult.Value.X;
                        End2 = CutPositionsAndVelocityMult.Value.Y;
                        if (End1 > End2)
                        {
                            End1Final = End2;
                            End2Final = End1;
                            if (iteration > End1Final && iteration <= End2Final)
                            {
                                float radian = MathHelper.ToRadians(iteration);
                                Vector2 vector = radian.ToRotationVector2() * new Vector2(a, b);
                                int DustID3 = Dust.NewDust(position, 1, 1, DustType, 0f, 0f, 10, color, size);
                                Main.dust[DustID3].noGravity = NoGravity;
                                Main.dust[DustID3].velocity = vector;
                            }
                        }
                        else
                        {
                            End1Final = End1;
                            End2Final = End2;
                            if (iteration < End1Final || iteration >= End2Final)
                            {
                                float radian = MathHelper.ToRadians(iteration);
                                Vector2 vector = radian.ToRotationVector2() * new Vector2(a, b);
                                int DustID3 = Dust.NewDust(position, 1, 1, DustType, 0f, 0f, 10, color, size);
                                Main.dust[DustID3].noGravity = NoGravity;
                                Main.dust[DustID3].velocity = vector;
                            }
                        }
                    }

                    else if (!IsCut)
                    {
                        if ((iteration >= FirstLower && iteration < FirstUpper) || iteration > SecondLower && iteration <= SecondUpper)
                        {
                            VelocityX = (float)((a * b) / Math.Sqrt(Math.Pow(b, 2) + Math.Pow(a, 2) * Math.Pow(Math.Tan(MathHelper.ToRadians(iteration)), 2)));
                            VelocityY = (float)((a * b * Math.Tan(MathHelper.ToRadians(iteration))) / Math.Sqrt(Math.Pow(b, 2) + Math.Pow(a, 2) * Math.Pow(Math.Tan(MathHelper.ToRadians(iteration)), 2)));
                        }
                        else if (iteration >= ThirdLower && iteration < ThirdUpper)
                        {
                            VelocityX = -(float)((a * b) / Math.Sqrt(Math.Pow(b, 2) + Math.Pow(a, 2) * Math.Pow(Math.Tan(MathHelper.ToRadians(iteration)), 2)));
                            VelocityY = -(float)((a * b * Math.Tan(MathHelper.ToRadians(iteration))) / Math.Sqrt(Math.Pow(b, 2) + Math.Pow(a, 2) * Math.Pow(Math.Tan(MathHelper.ToRadians(iteration)), 2)));
                        }
                        int DustID3 = Dust.NewDust(position, 1, 1, DustType, 0f, 0f, 10, color, size);
                        Main.dust[DustID3].noGravity = NoGravity;
                        Main.dust[DustID3].velocity = new Vector2(VelocityX, VelocityY);
                    }
                }
            }
        }
    }
}