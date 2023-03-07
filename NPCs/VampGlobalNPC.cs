using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.Accessories;
using VKE.Items.Accessories.Hoods;
using VKE.Items.HardKnives;
using VKE.Items.Materials;
using VKE.Items.Misc;
using VKE.Items.PostMLKnives;
using VKE.Items.PreKnives;
using VKE.Items.SeasonalKnives;

namespace VKE.NPCs
{
    public class VampGlobalNPC : GlobalNPC
    {
        public bool VortexBuff = false;
        public bool IsNearAltar;
        public int Kills;
        int NPCLastInteract;
        public Entity attacker = Main.LocalPlayer;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Cyborg)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Materials.ProcessingUnit>());
                nextSlot++;
            }
            base.SetupShop(type, shop, ref nextSlot);
        }
        public override void OnKill(NPC npc)
        {
            VampPlayer p = Main.player[npc.lastInteraction].GetModPlayer<VampPlayer>();
            //for (int iterations = 0; iterations < VampireWorld.AltarBeingUsed.Count; iterations += 2)
            //{
            //    float shootToX = (VampireWorld.AltarBeingUsed[iterations] + 16) - (npc.Center.X / 16f);
            //    float shootToY = (VampireWorld.AltarBeingUsed[iterations + 1]) - (npc.Center.Y / 16f);
            //    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
            //    if (distance <= 44)
            //    {
            //        IsNearAltar = true;
            //    }
            //}
            if (!npc.SpawnedFromStatue && IsNearAltar)
            {
                p.BloodPoints += 2 + npc.lifeMax / 50;
                for (int x = 0; x < 50; x++)
                {
                    int DustID3 = Dust.NewDust(npc.Center, 1, 1, 5, 0f, -8f, 10, Color.Red, 1.5f);
                }
            }
            else if (npc.boss && IsNearAltar)
            {
                p.BloodPoints += 2 + npc.lifeMax / 100;
                for (int x = 0; x < 50; x++)
                {
                    int DustID3 = Dust.NewDust(npc.Center, 1, 1, 5, 0f, -8f, 10, Color.Red, 1.5f);
                }
            }
            else if (!npc.boss && !npc.SpawnedFromStatue && !IsNearAltar)
            {
                p.BloodPoints += 2 + npc.lifeMax / 100;
            }
            else if (npc.boss && !IsNearAltar)
            {
                p.BloodPoints += 2 + npc.lifeMax / 400;
            }

            //if (npc.lastInteraction != 255)
            //{
            //    if (p.VampNecklace)
            //    {
            //        if (Main.netMode == 0)
            //        {
            //            p.NeckProgress++;
            //        }
            //        else
            //        {
            //            ModPacket packet = Mod.GetPacket();
            //            packet.Write(VKE.SendBloodPoints);
            //            //Main.NewText("Packet3 Written");
            //            packet.Send(npc.lastInteraction); // sends the packet to the killer only
            //        }
            //    }
            //}






            if (gildedCurse && npc.lifeMax > 5 && npc.value > 0f)
            {
                for (int x = 0; x < 800; x++)
                {
                    int randomCopper = Main.rand.Next(0, 60);
                    int randomSilver = Main.rand.Next(0, 4000);
                    int randomGold = Main.rand.Next(0, 40000);
                    int RandomPlatinum = Main.rand.Next(0, 500000);
                    if (randomCopper == 2)
                        Item.NewItem(npc.GetSource_DropAsItem(), npc.getRect(), ItemID.CopperCoin);
                    if (randomSilver == 2)
                        Item.NewItem(npc.GetSource_DropAsItem(), npc.getRect(), ItemID.SilverCoin);
                    if (randomGold == 2)
                        Item.NewItem(npc.GetSource_DropAsItem(), npc.getRect(), ItemID.GoldCoin);
                    if (RandomPlatinum == 2)
                        Item.NewItem(npc.GetSource_DropAsItem(), npc.getRect(), ItemID.PlatinumCoin);
                }
            }
            if (npc.type == NPCID.Plantera)
            {
                if (Main.rand.Next(20) > 10)
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<BloomingTerror>());
                for (int x = 0; x < 5; x++)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<Superglue>());
                }
                for (int x = 0; x < Main.rand.Next(45, 76); x++)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<PlantFiber>());
                }
            }
            if (npc.type == NPCID.MartianDrone || npc.type == NPCID.SkeletronPrime || npc.type == NPCID.TheDestroyer || npc.type == NPCID.Retinazer || npc.type == NPCID.DeadlySphere || npc.type == NPCID.MartianWalker || npc.type == NPCID.MartianEngineer || npc.type == NPCID.Cyborg || npc.type == NPCID.MartianTurret || npc.type == NPCID.MartianSaucer)
            {
                if (Main.rand.Next(20) > 15)
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<ProcessingUnit>());
            }
            if (npc.type == NPCID.WyvernHead)
            {
                if (Main.rand.Next(0, 101) > 88)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<WyvernHead>());
                }
                //else
                    //Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<DamagedWyvernHead>()); // FIX SHARDS
            }
            if (npc.type == NPCID.Harpy)
            {
                if (Main.rand.Next(50) <= 4)
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<HarpyKnives>());
            }
            if (npc.type == NPCID.MartianTurret)
            {
                if (Main.rand.Next(50) == 4)
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<RukasusTeslaKnives>());
            }
            if (npc.type == NPCID.CultistBoss)
            {
                if (Main.rand.Next(10) > 7)
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<MagesHood>());
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CrimsonCrystal>());
            }
            if (npc.type == NPCID.Butcher)
            {
                if (NPC.downedPlantBoss == true)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<LivingTissue>());
                }
            }
            if (npc.type == NPCID.EaterofWorldsHead)
            {
                Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CorruptionCrystal>());
            }
            if (npc.type == NPCID.EaterofSouls || npc.type == NPCID.Corruptor || npc.type == NPCID.CorruptSlime || npc.type == NPCID.Slimeling || npc.type == NPCID.Slimer || npc.type == NPCID.Slimer2 || npc.type == NPCID.PigronCorruption || npc.type == NPCID.SandsharkCorrupt || npc.type == NPCID.DevourerHead)
            {
                Random random = new Random();
                int ran = random.Next(0, 11);
                if (ran == 5)
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CorruptionShard>()); // FIX SHARDS
            }
            if ((npc.type == NPCID.EaterofSouls || npc.type == NPCID.BigEater || npc.type == NPCID.LittleEater) && Main.hardMode)
            {
                if (Main.rand.Next(1, 50) == 25)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CorruptionNestKnives>());
                }
            }
            if (npc.type == NPCID.BigMimicCorruption)
            {
                for (int x = 0; x < Main.rand.Next(1, 7); x++)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CorruptionShard>()); // FIX SHARDS
                }
            }
            if (npc.type == NPCID.FaceMonster || npc.type == NPCID.BloodCrawler || npc.type == NPCID.BloodCrawlerWall || npc.type == NPCID.BloodJelly || npc.type == NPCID.Crimera || npc.type == NPCID.BigCrimera || npc.type == NPCID.LittleCrimera || npc.type == NPCID.Herpling || npc.type == NPCID.Crimslime || npc.type == NPCID.BigCrimslime || npc.type == NPCID.LittleCrimslime || npc.type == NPCID.BloodFeeder || npc.type == NPCID.FloatyGross || npc.type == NPCID.IchorSticker || npc.type == NPCID.CrimsonAxe || npc.type == NPCID.PigronCrimson)
            {
                Random random = new Random();
                int ran = random.Next(0, 11);
                if (ran == 5)
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CrimsonShard>()); // FIX SHARDS
            }
            if ((npc.type == NPCID.Crimera || npc.type == NPCID.BigCrimera || npc.type == NPCID.LittleCrimera) && Main.hardMode)
            {
                if (Main.rand.Next(1, 50) == 25)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CrimsonNestKnives>());
                }
            }
            if (npc.type == NPCID.BigMimicCrimson)
            {
                for (int x = 0; x < Main.rand.Next(1, 7); x++)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CrimsonShard>()); // FIX SHARDS
                }
            }
            //if (npc.type == NPCID.MoonLordCore)
            //{
            //    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<LuminiteKnives>());
            //}
            if (npc.type == NPCID.KingSlime)
            {
                Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<Superglue>());
                if (Main.rand.Next(0, 5) >= 3)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<RicochetEssence>());
                }
            }
            if (npc.type == NPCID.BlueSlime || npc.type == NPCID.BlackSlime || npc.type == NPCID.RedSlime || npc.type == NPCID.BlueSlime || npc.type == NPCID.PurpleSlime || npc.type == NPCID.RainbowSlime || npc.type == NPCID.SandSlime || npc.type == NPCID.SlimedZombie || npc.type == NPCID.Slimeling || npc.type == NPCID.SlimeMasked || npc.type == NPCID.Slimer || npc.type == NPCID.Slimer2 || npc.type == NPCID.SlimeRibbonGreen || npc.type == NPCID.SlimeRibbonRed || npc.type == NPCID.SlimeRibbonWhite || npc.type == NPCID.SlimeRibbonYellow || npc.type == NPCID.SlimeSpiked || npc.type == NPCID.SmallSlimedZombie || npc.type == NPCID.SpikedIceSlime || npc.type == NPCID.SpikedJungleSlime || npc.type == NPCID.UmbrellaSlime || npc.type == NPCID.YellowSlime || npc.type == NPCID.MotherSlime || npc.type == NPCID.LavaSlime || npc.type == NPCID.JungleSlime || npc.type == NPCID.IceSlime || npc.type == NPCID.IlluminantSlime || npc.type == NPCID.GreenSlime || npc.type == NPCID.DungeonSlime || npc.type == NPCID.CorruptSlime || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BigSlimedZombie || npc.type == NPCID.ArmedZombieSlimed || npc.type == NPCID.BabySlime || npc.type == NPCID.Pinky || npc.type == NPCID.BlueJellyfish || npc.type == NPCID.GreenJellyfish || npc.type == NPCID.BloodJelly || npc.type == NPCID.PinkJellyfish || npc.type == NPCID.StardustJellyfishBig || npc.type == NPCID.StardustJellyfishSmall || npc.type == NPCID.Crimslime || npc.type == NPCID.BigCrimslime || npc.type == NPCID.LittleCrimslime)
            {
                if (!Main.hardMode)
                {
                    if (Main.rand.Next(0, 1000) <= 25 * npc.lifeMax)
                    {
                        Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<RicochetEssence>());
                    }
                }
                else if (Main.hardMode)
                {
                    if (Main.rand.Next(0, 1000) <= 25 * npc.lifeMax + 50)
                    {
                        Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<RicochetEssence>());
                    }
                }
            }
            if (npc.type == NPCID.SkeletonSniper || npc.type == NPCID.CultistArcherBlue || npc.type == NPCID.CultistArcherWhite || npc.type == NPCID.ElfArcher || npc.type == NPCID.GoblinArcher || npc.type == NPCID.SkeletonArcher || npc.type == NPCID.AngryBones || npc.type == NPCID.AngryBonesBig || npc.type == NPCID.AngryBonesBigHelmet || npc.type == NPCID.AngryBonesBigMuscle || npc.type == NPCID.BlueArmoredBones || npc.type == NPCID.BlueArmoredBonesMace || npc.type == NPCID.BlueArmoredBonesNoPants || npc.type == NPCID.BlueArmoredBonesSword || npc.type == NPCID.HellArmoredBones || npc.type == NPCID.HellArmoredBonesMace || npc.type == NPCID.HellArmoredBonesSpikeShield || npc.type == NPCID.HellArmoredBonesSword || npc.type == NPCID.RustyArmoredBonesAxe || npc.type == NPCID.RustyArmoredBonesFlail || npc.type == NPCID.RustyArmoredBonesSword || npc.type == NPCID.RustyArmoredBonesSwordNoArmor)
            {
                if (!Main.hardMode)
                {
                    if (Main.rand.Next(0, 1000) <= 1 * npc.lifeMax)
                    {
                        Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<PiercingTip>());
                    }
                }
                else if (Main.hardMode)
                {
                    if (Main.rand.Next(0, 1000) <= 1 * (npc.lifeMax / 2))
                    {
                        Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<PiercingTip>());
                    }
                }
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<CritEmblem>());
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<ExtraFinger>());
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<MechanicalFingers>());
                for (int x = 0; x < Main.rand.Next(1, 3); x++)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<PiercingTip>());
                }
            }
            if (npc.type == NPCID.Mothron)
            {
                Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<BrokenHeroKnives>());
            }
            if (npc.type == NPCID.Vampire)
            {
                if (Main.rand.Next(0, 26) == 25)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<AncientVampiricTablet>());
                }
            }
            if (npc.type == NPCID.Everscream)
            {
                if (Main.rand.Next(0, 25) == 5)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<EverscreamsBranch>());
                }
            }
            if (npc.type == NPCID.Pumpking)
            {
                if (Main.rand.Next(0, 6) == 5)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<HarvestersSoul>());
                }
            }
            //if (npc.type == NPCID.Bunny || npc.type == NPCID.BunnySlimed || npc.type == NPCID.GoldBunny)
            //{
            //    if (Main.rand.Next(0, 101) == 5)
            //    {
            //        Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<Items.SeasonalStuff.EasterBasket>());
            //    }
            //}
            if (npc.type == NPCID.MartianEngineer || npc.type == NPCID.MartianSaucer || npc.type == NPCID.MartianDrone)
            {
                if (Main.rand.Next(0, 25) == 5)
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), new Vector2((int)npc.position.X, (int)npc.position.Y), new Vector2(npc.width, npc.height), ModContent.ItemType<Items.Materials.ProcessingUnit>());
                }
            }
        }

        public override bool CheckDead(NPC npc)
        {
            if (VortexBuff)
            {
                for (int x = 0; x < Main.rand.Next(8, 16); x++)
                {
                    float ran1 = Main.rand.NextFloat(-2, 2);
                    float ran2 = Main.rand.NextFloat(-2, 2);
                    if (ran1 == 0)
                        ran1 = -1;
                    if (ran2 == 0)
                        ran2 = -2;
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.position.X, npc.position.Y, ran1, ran2, Mod.Find<ModProjectile>("VortexProj2").Type, 8, 4, Main.LocalPlayer.whoAmI);
                }
                return true;
            }
            else
                return true;
        }

        public bool PenetratingPoison = false;
        public bool bleedingOut = false;
        public bool bleedingOut2 = false;
        public bool hellfire = false;
        public bool cursedFire = false;
        public bool gildedCurse = false;
        public bool ichorUproar = false;
        public bool partyBuff = false;
        public bool potentPoison = false;
        public bool ShroomitePoison = false;
        public bool VeiLove = false;
        public bool MouseMilkDebuff;

        public override void ResetEffects(NPC npc)
        {
            PenetratingPoison = false;
            bleedingOut = false;
            bleedingOut2 = false;
            hellfire = false;
            cursedFire = false;
            gildedCurse = false;
            ichorUproar = false;
            partyBuff = false;
            potentPoison = false;
            VortexBuff = false;
            ShroomitePoison = false;
            VeiLove = false;
            Latched = false;
            MouseMilkDebuff = false;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (PenetratingPoison)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (npc.lifeRegen > 0)
                    {
                        npc.lifeRegen = 0;
                    }
                    npc.lifeRegen -= 12;
                    if (damage < 6)
                    {
                        damage = 6;
                    }
                }
            }
            if(ShroomitePoison)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (npc.lifeRegen > 0)
                    {
                        npc.lifeRegen = 0;
                    }
                    npc.lifeRegen -= 18;
                    if (damage < 9)
                    {
                        damage = 11 + Main.rand.Next(-3,4);
                    }
                }
            }
            if(MouseMilkDebuff)
            {
                for(int x = 0; x < 4; x++)
                {
                    if (npc.lifeRegen > 0)
                        npc.lifeRegen = 0;
                    npc.lifeRegen -= 6;
                    if (damage < 3)
                        damage = 3 + Main.rand.Next(-1, 4);
                }
                npc.velocity *= 0.98f;
                npc.defense -= 5;
                npc.damage = (int)(npc.damage * 0.9f);
            }
            if (bleedingOut)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                int exampleKnifeCount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == ModContent.ProjectileType<Projectiles.PreProj.ButchersKnivesProj>() && p.ai[0] == 1f && p.ai[1] == npc.whoAmI)
                    {
                        exampleKnifeCount++;
                    }
                }
                npc.lifeRegen -= exampleKnifeCount * 2 * 3;
                if (damage < exampleKnifeCount * 3)
                {
                    damage = exampleKnifeCount * 3;
                }
            }
            if (bleedingOut2)
            {
                for(int y = 0; y < 25; y++)
                {
                    if (npc.lifeRegen > 0)
                    {
                        npc.lifeRegen = 0;
                    }
                    int RandomUpAndDown = 1 + Main.rand.Next(0, 4);
                    npc.lifeRegen -= RandomUpAndDown;
                    if (damage != RandomUpAndDown)
                        damage = RandomUpAndDown;
                }
            }
            if (hellfire)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (npc.lifeRegen > 0)
                    {
                        npc.lifeRegen = 0;
                    }
                    npc.lifeRegen -= Main.rand.Next(5, 12);
                    if (damage < 5)
                        damage = damage + x;
                }
            }
            if (cursedFire)
            {
                for (int x = 0; x < 15; x++)
                {
                    if (npc.lifeRegen > 0)
                    {
                        npc.lifeRegen = 0;
                    }
                    npc.lifeRegen -= Main.rand.Next(8, 18);
                    if (damage < 8)
                        damage = damage + x;
                }
            }
        }
        int DamageStore;
        bool Init;
        int VeiDelay;
        public bool Latched;
        public bool IsFalling;
        public int FallDamage;

        public override bool PreAI(NPC npc)
        {
            if (VeiLove == true)
            {
                return false;
            }
            else if(Latched == true)
            {
                if(!Main.tile[(int)npc.Bottom.X / 16, (int)(npc.Bottom.Y) / 16].HasTile && !npc.noGravity)
                {
                    IsFalling = true;
                }
                return false;
            }
            else
                return true;
        }
        public override void AI(NPC npc)
        {
            if(VeiLove == true && !npc.boss)
            {
                
            }
        }
        public override void PostAI(NPC npc)
        {
            if (!Latched && IsFalling && !Main.tile[(int)npc.Bottom.X / 16, (int)(npc.Bottom.Y) / 16].HasTile)
            {
                FallDamage++;
            }
            else if (IsFalling && Main.tile[(int)npc.Bottom.X / 16, (int)(npc.Bottom.Y) / 16].HasTile)
            {
                npc.StrikeNPC(FallDamage, 0, 0);
                IsFalling = false;
                FallDamage = 0;
            }
            if (VeiLove == true && !npc.boss)
            {
                if(!Init)
                {
                    Init = true;
                    DamageStore = npc.damage;
                }
                npc.defense = 0;
                npc.damage = 0;
                npc.velocity *= 0;
                VeiDelay++;
                if(VeiDelay >= 5)
                {
                    int num589 = Dust.NewDust(new Vector2(npc.Top.X, npc.Top.Y), 1, 1, ModContent.DustType<Dusts.HeartDust>(), 0f, -3f, 100, Color.Pink, 1f);
                    Main.dust[num589].noGravity = true;
                    VeiDelay = 0;
                }
            }
            else if(!VeiLove && Init == true)
            {
                npc.damage = DamageStore;
                Init = false;
            }
            if(Latched)
            {
                if (!Init)
                {
                    Init = true;
                    DamageStore = npc.damage;
                }
                npc.damage = 0;
                npc.velocity *= 0;
            }
            else if(!Latched && Init)
            {
                npc.damage = DamageStore;
                Init = false;
            }
            base.PostAI(npc);
        }
        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (ichorUproar)
            {
                if (npc.defense > 5)
                    damage = (int)(damage * (npc.defense / 3));
                if (npc.defense < 5 && npc.defense > 0)
                    damage = (int)(damage * (npc.defense / 2));
            }
            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        private bool resetBatchInPost;

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //if (npc.HasBuff(ModContent.BuffType<Buffs.VTuberBuffs.ImEnsured>()) && Main.netMode != NetmodeID.Server) // The netmode check might be redundant but I can't verify whether or not it is.
            //{
            //    resetBatchInPost = true; // We're using a dedicated bool for this in the *very* unlikely case your buff somehow gets purged during drawing.

            //    spriteBatch.End();
            //    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix); // SpriteSortMode needs to be set to Immediate for shaders to work.

            //    GameShaders.Misc["Technique1"].Apply(); // If you need to set any parameters, you can so before Apply (e.g. Misc["EffectName'].UseColor(something).Apply() )
            //}

            return true;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (resetBatchInPost)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                resetBatchInPost = false;
            }
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if(MouseMilkDebuff)
            {
                if (Main.rand.Next(0, 10) == 4)
                {
                    int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), (int)(npc.width), (int)(npc.height), 268, 0f, 0f, 100, Color.Brown, 1.4f);
                    Main.dust[dustIndex].velocity.Y *= 1.2f;
                }

            }
            if (PenetratingPoison)
            {
                int DustID2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 273, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 10, Color.DarkGreen, 1.8f);
                Main.dust[DustID2].noGravity = true;
                int DustID3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 244, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 10, Color.LightGreen, 1.8f);
                Main.dust[DustID3].noGravity = true;
            }
            if (ShroomitePoison)
            {
                int DustID2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 1, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 10, new Color(126,138,255), 1.8f);
                Main.dust[DustID2].noGravity = true;
            }
            if (bleedingOut)
            {
                Random ran = new Random();
                int blood = ran.Next(0, 2);
                if (blood == 1)
                {
                    int DustID2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 5, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 10, Color.Red, 1f);
                    Main.dust[DustID2].noGravity = true;
                }
            }
            if (bleedingOut2)
            {
                bool blood = Main.rand.NextBool();
                if (blood)
                {
                    for(int x = 0; x < 5 * (npc.Size.X/8); x++)
                    {
                        int DustID2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 268, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 140, Color.Red, 1f);
                        Main.dust[DustID2].noGravity = false;
                    }
                }
            }
            if(Latched)
            {
                if(Main.rand.Next(0,12) <= 3)
                {
                    int DustID2 = Dust.NewDust(new Vector2(npc.Top.X, npc.Top.Y + 5), 4, 4, 261, 0f, 0f, 140, Color.White, 1f);
                    Main.dust[DustID2].noGravity = false;
                }
            }
            if (hellfire)
            {
                int DustID3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 186, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 40, Color.Black, 2f);
                Main.dust[DustID3].noGravity = false;
                Main.dust[DustID3].velocity.Y = -6;
                int DustID2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 6, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 10, Color.Red, 3f);
                Main.dust[DustID2].noGravity = false;
                Main.dust[DustID2].velocity.Y = -5;
            }
            if (cursedFire)
            {
                int DustID3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y - (npc.height / 2)), npc.width - 3, npc.height - 3, 186, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 40, Color.Black, 2f);
                Main.dust[DustID3].noGravity = false;
                Main.dust[DustID3].velocity.Y = -6;
                int DustID2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 228, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 10, Color.Green, 2f);
                Main.dust[DustID2].noGravity = false;
                Main.dust[DustID2].velocity.Y = -5;
            }
            if (gildedCurse)
            {
                int DustID3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 10, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 40, Color.Gold, 1.2f);
                Main.dust[DustID3].noGravity = true;
                int DustID2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 19, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 40, Color.Gold, 0.8f);
                Main.dust[DustID2].noGravity = true;
                npc.color = Color.Gold;
            }
            if (ichorUproar)
            {
                int DustID3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y - (npc.height / 2)), npc.width - 3, npc.height - 3, 186, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 40, Color.White, 2f);
                Main.dust[DustID3].noGravity = false;
                Main.dust[DustID3].velocity.Y = -6;
                int DustID2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width - 3, npc.height - 3, 228, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 10, Color.Orange, 2f);
                Main.dust[DustID2].noGravity = false;
                Main.dust[DustID2].velocity.Y = -5;
            }
            if (partyBuff)
            {
                int colorSelect = Main.rand.Next(0, 25);
                if (colorSelect == 0)
                    npc.color = Color.Red;
                if (colorSelect == 1)
                    npc.color = Color.Orange;
                if (colorSelect == 2)
                    npc.color = Color.Yellow;
                if (colorSelect == 3)
                    npc.color = Color.Green;
                if (colorSelect == 4)
                    npc.color = Color.Blue;
                if (colorSelect == 5)
                    npc.color = Color.Indigo;
                if (colorSelect == 6)
                    npc.color = Color.Violet;
            }
            if(VortexBuff)
            {
                if (Main.rand.NextFloat() < 0.4605263f)
                {
                    Dust dust;
                    Vector2 position = Main.LocalPlayer.Center;
                    dust = Main.dust[Terraria.Dust.NewDust(npc.position, npc.width, npc.height, 217, 0f, 0f, 0, new Color(0, 242, 255), 0.86f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1.381579f;
                }
            }
        }
    }
}
