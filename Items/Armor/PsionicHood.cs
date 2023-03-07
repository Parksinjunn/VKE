using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class PsionicHood : KnifeItem
    {
        public int numProj;
        public int Frame;
        public int FrameCounter;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Psionic Hood");
            Tooltip.SetDefault("Threaded with pulsing energy.");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 2;
            Item.defense = 1;
        }

        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (NPC.downedBoss2)
            {
                numProj = 1;
            }
            if (NPC.downedQueenBee)
            {
                numProj = 2;
            }
            if (NPC.downedBoss3)
            {
                numProj = 3;
            }
            if (Main.hardMode)
            {
                numProj = 4;
            }
            if (NPC.downedMechBoss1)
            {
                numProj = 5;
            }
            if (NPC.downedMechBoss2)
            {
                numProj = 6;
            }
            if (NPC.downedMechBoss3)
            {
                numProj = 7;
            }
            if (NPC.downedPlantBoss)
            {
                numProj = 8;
            }
            if (NPC.downedGolemBoss)
            {
                numProj = 9;
            }
            if (NPC.downedFishron)
            {
                numProj = 10;
            }
            if (NPC.downedAncientCultist)
            {
                numProj = 11;
            }
            if (NPC.downedTowers)
            {
                numProj = 12;
            }
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            TooltipLine line = new TooltipLine(Mod, "Face", "+ " + (int)((p.HealAccMult-1)*100) + "% greater healing");
            line.OverrideColor = new Color(255, 60, 28);
            if (NPC.downedBoss2)
            {
                tooltips.Add(line);
            }

            if (p.PsionicArmorSet)
            {
                TooltipLine line4 = new TooltipLine(Mod, "Face", "Set Bonus:");
                line4.OverrideColor = new Color(255, 70, 38);
                tooltips.Add(line4);
                TooltipLine line3 = new TooltipLine(Mod, "Face", "+ 25% attack speed");
                line3.OverrideColor = new Color(255, 70, 38);
                tooltips.Add(line3);
            }

            foreach (TooltipLine line5 in tooltips)
            {
                if (line5.Mod == "Terraria" && line5.Name == "Equipable")
                {
                    line5.OverrideColor = new Color(255, 40, 20);
                }
                if (line5.Mod == "Terraria" && line5.Name == "Defense")
                {
                    line5.OverrideColor = new Color(235, 32, 12);
                }
                if (line5.Mod == "Terraria" && line5.Name == "Tooltip0")
                {
                    line5.OverrideColor = new Color(215, 20, 2);
                }
                if (line5.Mod == "Terraria" && line5.Name == "ItemName")
                {
                    if (Frame == 0)
                    {
                        line5.Text = ("[c/FF3333:Psionic Hood]");
                    }
                    if (Frame == 1)
                    {
                        line5.Text = ("[c/B48C8C:Ps][c/FF3333:ionic Hood]");
                    }
                    if (Frame == 2)
                    {
                        line5.Text = ("[c/75D6D6:Ps][c/B48C8C:io][c/FF3333:nic Hood]");
                    }
                    if (Frame == 3)
                    {
                        line5.Text = ("[c/B48C8C:Ps][c/75D6D6:io][c/B48C8C:ni][c/FF3333:c Hood]");
                    }
                    if (Frame == 4)
                    {
                        line5.Text = ("[c/FF3333:Ps][c/B48C8C:io][c/75D6D6:ni][c/B48C8C:c][c/FF3333: Hood]");
                    }
                    if (Frame == 5)
                    {
                        line5.Text = ("[c/FF3333:Psio][c/B48C8C:ni][c/75D6D6:c][c/B48C8C: Ho][c/FF3333:od]");
                    }
                    if (Frame == 6)
                    {
                        line5.Text = ("[c/FF3333:Psioni][c/B48C8C:c][c/75D6D6: Ho][c/B48C8C:od]");
                    }
                    if (Frame == 7)
                    {
                        line5.Text = ("[c/FF3333:Psionic][c/B48C8C: Ho][c/75D6D6:od]");
                    }
                    if (Frame == 8)
                    {
                        line5.Text = ("[c/FF3333:Psionic Ho][c/B48C8C:od]");
                    }
                    if (Frame == 9)
                    {
                        line5.Text = ("[c/FF3333:Psionic Hood]");
                    }
                }
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PsionicChestplate>() && legs.type == ModContent.ItemType<PsionicLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            p.PsionicArmorSet = true;
            p.PsionicPower = true;
        }
        public override void UpdateInventory(Player player)
        {
            FrameCounter++; //increase the frameCounter by one
            if (FrameCounter >= 4) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                FrameCounter = 0;
                Frame++; //go to the next frame
                if (Frame > 9) //if past the last frame
                    Frame = 0; //go back to the first frame
            }
        }
        public override void UpdateEquip(Player player)
        {
            FrameCounter++; //increase the frameCounter by one
            if (FrameCounter >= 4) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                FrameCounter = 0;
                Frame++; //go to the next frame
                if (Frame > 9) //if past the last frame
                    Frame = 0; //go back to the first frame
            }
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            //KnifeDamagePlayer d = player.GetModPlayer<KnifeDamagePlayer>();
            if (NPC.downedBoss2)
            {
                p.HealAccMult += 0.05f;
                Item.value = Item.sellPrice(0, 2, 0, 0);
                Item.defense = 2;
            }
            if (NPC.downedQueenBee)
            {
                Item.value = Item.sellPrice(0, 3, 0, 0);
            }
            if (NPC.downedBoss3)
            {
                p.HealAccMult += 0.1f;
                Item.value = Item.sellPrice(0, 4, 0, 0);
                Item.defense = 3;
            }
            if (Main.hardMode)
            {
                p.HealAccMult += 0.15f;
                Item.value = Item.sellPrice(0, 5, 0, 0);
                Item.defense = 5;
            }
            if (NPC.downedMechBoss1)
            {
                p.HealAccMult += 0.2f;
                Item.value = Item.sellPrice(0, 6, 0, 0);
                Item.defense = 6;
            }
            if (NPC.downedMechBoss2)
            {
                p.HealAccMult += 0.25f;
                Item.value = Item.sellPrice(0, 7, 0, 0);
                Item.defense = 7;
            }
            if (NPC.downedMechBoss3)
            {
                p.HealAccMult += 0.3f;
                Item.value = Item.sellPrice(0, 8, 0, 0);
            }
            if (NPC.downedPlantBoss)
            {
                p.HealAccMult += 0.35f;
                Item.value = Item.sellPrice(0, 9, 0, 0);
                Item.defense = 8;
            }
            if (NPC.downedGolemBoss)
            {
                p.HealAccMult += 0.4f;
                Item.value = Item.sellPrice(0, 10, 0, 0);
                Item.defense = 9;
            }
            if (NPC.downedFishron)
            {
                p.HealAccMult += 0.45f;
                Item.value = Item.sellPrice(0, 12, 0, 0);
                Item.defense = 10;
            }
            if (NPC.downedAncientCultist)
            {
                p.HealAccMult += 0.5f;
                Item.value = Item.sellPrice(0, 14, 0, 0);
                Item.defense = 11;
            }
            if (NPC.downedTowers)
            {
                p.HealAccMult += 0.6f;
                Item.value = Item.sellPrice(0, 16, 0, 0);
                Item.defense = 12;
            }
        }
    }
}