using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class VampiricGreaves : KnifeItem
    {
        float SpeedIncrease;
        public int Frame;
        public int FrameCounter;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Vampiric Greaves");
            Tooltip.SetDefault("");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 2;
            Item.defense = 6;
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
            TooltipLine line2 = new TooltipLine(Mod, "Face", "Defense knives have " + p.DefenseExtraLives + " extra life (CURRENTLY DISABLED)");
            if(NPC.downedBoss2)
            {
                line2.Text = "Defense knives have " + p.DefenseExtraLives + " extra lives (CURRENTLY DISABLED)";
            }
            line2.OverrideColor = new Color(160, 0, 0);
            if (NPC.downedBoss2)
            {
                tooltips.Add(line2);
            }
            TooltipLine line = new TooltipLine(Mod, "Face", "+" + (int)(SpeedIncrease*100) + "% Movement Speed.");
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
                TooltipLine line3 = new TooltipLine(Mod, "Face", "Enemies are more likely to target you\nHave a " + ((2f * p.VampiricSetScaler) / 10f) + "% Chance to steal the life of the enemies around you upon being hit");
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
                        line5.Text = ("[c/3B0000:Vampiric Greaves]");
                    }
                    if (Frame == 1)
                    {
                        line5.Text = ("[c/730600:Vampiric Greaves]");
                    }
                    if (Frame == 2)
                    {
                        line5.Text = ("[c/AD0900:Vampiric Greaves]");
                    }
                    if (Frame == 3)
                    {
                        line5.Text = ("[c/730600:Vampiric Greaves]");
                    }
                }
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
            if (NPC.downedBoss2)
            {
                SpeedIncrease = 0.01f;
                Item.value = Item.sellPrice(0, 2, 0, 0);
                Item.defense = 6;
                p.DefenseExtraLives += 1;
            }
            if (NPC.downedQueenBee)
            {
                SpeedIncrease = 0.02f;
                Item.value = Item.sellPrice(0, 3, 0, 0);
            }
            if (NPC.downedBoss3)
            {
                SpeedIncrease = 0.03f;
                Item.value = Item.sellPrice(0, 4, 0, 0);
                Item.defense = 7;
                p.DefenseExtraLives += 2;
            }
            if (Main.hardMode)
            {
                SpeedIncrease = 0.04f;
                Item.value = Item.sellPrice(0, 5, 0, 0);
                Item.defense = 8;
            }
            if (NPC.downedMechBoss1)
            {
                SpeedIncrease = 0.05f;
                Item.value = Item.sellPrice(0, 6, 0, 0);
                Item.defense = 10;
            }
            if (NPC.downedMechBoss2)
            {
                SpeedIncrease = 0.06f;
                Item.value = Item.sellPrice(0, 7, 0, 0);
                Item.defense = 11;
            }
            if (NPC.downedMechBoss3)
            {
                SpeedIncrease = 0.07f;
                Item.value = Item.sellPrice(0, 8, 0, 0);
                Item.defense = 12;
                p.DefenseExtraLives += 3;
            }
            if (NPC.downedPlantBoss)
            {
                SpeedIncrease = 0.08f;
                Item.value = Item.sellPrice(0, 9, 0, 0);
                Item.defense = 14;
                p.DefenseExtraLives += 4;
            }
            if (NPC.downedGolemBoss)
            {
                SpeedIncrease = 0.09f;
                Item.value = Item.sellPrice(0, 10, 0, 0);
                Item.defense = 15;
            }
            if (NPC.downedFishron)
            {
                SpeedIncrease = 0.10f;
                Item.value = Item.sellPrice(0, 12, 0, 0);
                Item.defense = 16;
                p.DefenseExtraLives += 5;
            }
            if (NPC.downedAncientCultist)
            {
                SpeedIncrease = 0.11f;
                Item.value = Item.sellPrice(0, 14, 0, 0);
                Item.defense = 18;
                p.DefenseExtraLives += 6;
            }
            if (NPC.downedTowers)
            {
                SpeedIncrease = 0.12f;
                Item.value = Item.sellPrice(0, 16, 0, 0);
                Item.defense = 20;
                p.DefenseExtraLives += 7;
            }
            if (NPC.downedMoonlord)
            {
                SpeedIncrease = 0.15f;
                Item.value = Item.sellPrice(0, 20, 0, 0);
                Item.defense = 24;
                p.DefenseExtraLives += 10;
            }
            player.moveSpeed += SpeedIncrease;
        }
    }
}