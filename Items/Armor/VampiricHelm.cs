using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VampiricHelm : KnifeItem
    {
        public int Frame;
        public int FrameCounter;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Vampiric Helm");
            Tooltip.SetDefault("");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 2;
            Item.defense = 5;
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
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            TooltipLine line = new TooltipLine(Mod, "Face", "+" + ((p.DefenseReflectChance-1))*100 + "% reflect chance (CURRENTLY DISABLED)");
            line.OverrideColor = new Color(160, 0, 0);
            if (NPC.downedBoss2)
            {
                tooltips.Add(line);
            }

            if (p.VampiricArmorSet)
            {
                TooltipLine line4 = new TooltipLine(Mod, "Face", "Set Bonus:");
                line4.OverrideColor = new Color(180, 0, 0);
                tooltips.Add(line4);
                TooltipLine line3 = new TooltipLine(Mod, "Face", "Enemies are more likely to target you\nHave a " + ((2f*p.VampiricSetScaler)/10f) + "% Chance to steal the life of the enemies around you upon being hit");
                line3.OverrideColor = new Color(180, 0, 0);
                tooltips.Add(line3);
            }

            foreach (TooltipLine line5 in tooltips)
            {
                if (line5.Mod == "Terraria" && line5.Name == "Equipable")
                {
                    line5.OverrideColor = new Color(160, 0, 0);
                }
                if (line5.Mod == "Terraria" && line5.Name == "Defense")
                {
                    line5.OverrideColor = new Color(160, 0, 0);
                }
                if (line5.Mod == "Terraria" && line5.Name == "Tooltip0")
                {
                    line5.OverrideColor = new Color(160, 0, 0);
                }
                if (line5.Mod == "Terraria" && line5.Name == "ItemName")
                {
                    if (Frame == 0)
                    {
                        line5.Text = ("[c/3B0000:Vampiric Helm]");
                    }
                    if(Frame == 1)
                    {
                        line5.Text = ("[c/730600:Vampiric Helm]");
                    }
                    if (Frame == 2)
                    {
                        line5.Text = ("[c/AD0900:Vampiric Helm]");
                    }
                    if (Frame == 3)
                    {
                        line5.Text = ("[c/730600:Vampiric Helm]");
                    }
                }
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VampiricChestplate>() && legs.type == ModContent.ItemType<VampiricGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            p.VampiricArmorSet = true;
            if (NPC.downedBoss2)
            {
                p.VampiricSetScaler = 1.5f;
            }
            if (NPC.downedBoss3)
            {
                p.VampiricSetScaler = 2f;
            }
            if (Main.hardMode)
            {
                p.VampiricSetScaler = 2.5f;
            }
            if (NPC.downedMechBoss1)
            {
                p.VampiricSetScaler = 3f;
            }
            if (NPC.downedMechBoss2)
            {
                p.VampiricSetScaler = 3.5f;
            }
            if (NPC.downedMechBoss3)
            {
                p.VampiricSetScaler = 4f;
            }
            if (NPC.downedPlantBoss)
            {
                p.VampiricSetScaler = 4.5f;
            }
            if (NPC.downedGolemBoss)
            {
                p.VampiricSetScaler = 5f;
            }
            if (NPC.downedFishron)
            {
                p.VampiricSetScaler = 5.5f;
            }
            if (NPC.downedAncientCultist)
            {
                p.VampiricSetScaler = 6f;
            }
            if (NPC.downedTowers)
            {
                p.VampiricSetScaler = 7f;
            }
            if (NPC.downedMoonlord)
            {
                p.VampiricSetScaler = 10f;
            }
        }
        public override void UpdateInventory(Player player)
        {
            FrameCounter++; //increase the frameCounter by one
            if (FrameCounter >= 8) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                FrameCounter = 0;
                Frame++; //go to the next frame
                if (Frame > 3) //if past the last frame
                    Frame = 0; //go back to the first frame
            }
        }
        public override void UpdateEquip(Player player)
        {
            FrameCounter++; //increase the frameCounter by one
            if (FrameCounter >= 8) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                FrameCounter = 0;
                Frame++; //go to the next frame
                if (Frame > 3) //if past the last frame
                    Frame = 0; //go back to the first frame
            }
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            player.aggro += 300;
            //KnifeDamagePlayer d = player.GetModPlayer<KnifeDamagePlayer>();
            if (NPC.downedBoss2)
            {
                Item.value = Item.sellPrice(0, 2, 0, 0);
                p.DefenseReflectChance = 1.2f;
                Item.defense = 5;
            }
            if (NPC.downedQueenBee)
            {
                Item.value = Item.sellPrice(0, 3, 0, 0);
                p.DefenseReflectChance = 1.3f;
            }
            if (NPC.downedBoss3)
            {
                Item.value = Item.sellPrice(0, 4, 0, 0);
                p.DefenseReflectChance = 1.5f;
                Item.defense = 7;
            }
            if (Main.hardMode)
            {
                Item.value = Item.sellPrice(0, 5, 0, 0);
                p.DefenseReflectChance = 1.6f;
                Item.defense = 10;
            }
            if (NPC.downedMechBoss1)
            {
                Item.value = Item.sellPrice(0, 6, 0, 0);
                p.DefenseReflectChance = 1.7f;
                Item.defense = 12;
            }
            if (NPC.downedMechBoss2)
            {
                Item.value = Item.sellPrice(0, 7, 0, 0);
                p.DefenseReflectChance = 1.8f;
                Item.defense = 13;
            }
            if (NPC.downedMechBoss3)
            {
                Item.value = Item.sellPrice(0, 8, 0, 0);
                p.DefenseReflectChance = 1.9f;
                Item.defense = 14;
            }
            if (NPC.downedPlantBoss)
            {
                Item.value = Item.sellPrice(0, 9, 0, 0);
                p.DefenseReflectChance = 2.1f;
                Item.defense = 16;
            }
            if (NPC.downedGolemBoss)
            {
                Item.value = Item.sellPrice(0, 10, 0, 0);
                p.DefenseReflectChance = 2.2f;
                Item.defense = 18;
            }
            if (NPC.downedFishron)
            {
                Item.value = Item.sellPrice(0, 12, 0, 0);
                p.DefenseReflectChance = 2.5f;
                Item.defense = 20;
            }
            if (NPC.downedAncientCultist)
            {
                Item.value = Item.sellPrice(0, 14, 0, 0);
                p.DefenseReflectChance = 3f;
                Item.defense = 22;
            }
            if (NPC.downedTowers)
            {
                Item.value = Item.sellPrice(0, 16, 0, 0);
                p.DefenseReflectChance = 4f;
                Item.defense = 24;
            }
            if (NPC.downedMoonlord)
            {
                Item.value = Item.sellPrice(0, 20, 0, 0);
                p.DefenseReflectChance = 5f;
                Item.defense = 32;
            }
        }
    }
}