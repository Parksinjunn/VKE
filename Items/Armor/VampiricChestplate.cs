using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class VampiricChestplate : KnifeItem
    {
        int StatLifeBonus;
        public int Frame = 0;
        public int FrameCounter = 0;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Vampiric Chestplate");
            Tooltip.SetDefault("");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 2;
            Item.defense = 2;
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
            TooltipLine line = new TooltipLine(Mod, "Face", "+" + (int)(DamageMult * 100f) + "% Knife Damage");
            line.OverrideColor = new Color(160, 0, 0);
            TooltipLine line2 = new TooltipLine(Mod, "Face", "+" + StatLifeBonus + " Health");
            line2.OverrideColor = new Color(160, 0, 0);
            if (NPC.downedBoss2)
            {
                tooltips.Add(line);
            }
            if(NPC.downedBoss3)
            {
                tooltips.Add(line2);
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
                        line5.Text = ("[c/3B0000:Vampiric Chestplate]");
                    }
                    if (Frame == 1)
                    {
                        line5.Text = ("[c/730600:Vampiric Chestplate]");
                    }
                    if (Frame == 2)
                    {
                        line5.Text = ("[c/AD0900:Vampiric Chestplate]");
                    }
                    if (Frame == 3)
                    {
                        line5.Text = ("[c/730600:Vampiric Chestplate]");
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
        float DamageMult;
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
            if (NPC.downedBoss2)
            {
                Item.value = Item.sellPrice(0, 2, 0, 0);
                Item.defense = 8;
                DamageMult = 0.05f;
            }
            if (NPC.downedQueenBee)
            {
                Item.value = Item.sellPrice(0, 3, 0, 0);
                Item.defense = 9;
                DamageMult = 0.06f;
            }
            if (NPC.downedBoss3)
            {
                Item.value = Item.sellPrice(0, 4, 0, 0);
                Item.defense = 10;
                DamageMult = 0.08f;
                StatLifeBonus = 10;
            }
            if (Main.hardMode)
            {
                Item.value = Item.sellPrice(0, 5, 0, 0);
                DamageMult = 0.10f;
                Item.defense = 12;
                StatLifeBonus = 20;
            }
            if (NPC.downedMechBoss1)
            {
                Item.value = Item.sellPrice(0, 6, 0, 0);
                DamageMult = 0.12f;
                Item.defense = 14;
            }
            if (NPC.downedMechBoss2)
            {
                Item.value = Item.sellPrice(0, 7, 0, 0);
                DamageMult = 0.14f;
                Item.defense = 16;
            }
            if (NPC.downedMechBoss3)
            {
                Item.value = Item.sellPrice(0, 8, 0, 0);
                DamageMult = 0.16f;
                Item.defense = 20;
                StatLifeBonus = 30;
            }
            if (NPC.downedPlantBoss)
            {
                Item.value = Item.sellPrice(0, 9, 0, 0);
                DamageMult = 0.18f;
                Item.defense = 22;
            }
            if (NPC.downedGolemBoss)
            {
                Item.value = Item.sellPrice(0, 10, 0, 0);
                DamageMult = 0.20f;
                Item.defense = 23;
                StatLifeBonus = 40;
            }
            if (NPC.downedFishron)
            {
                Item.value = Item.sellPrice(0, 12, 0, 0);
                DamageMult = 0.22f;
                Item.defense = 25;
            }
            if (NPC.downedAncientCultist)
            {
                Item.value = Item.sellPrice(0, 14, 0, 0);
                DamageMult = 0.25f;
                Item.defense = 30;
                StatLifeBonus = 50;
            }
            if (NPC.downedTowers)
            {
                Item.value = Item.sellPrice(0, 16, 0, 0);
                DamageMult = 0.28f;
                Item.defense = 34;
                StatLifeBonus = 60;
            }
            if (NPC.downedMoonlord)
            {
                Item.value = Item.sellPrice(0, 20, 0, 0);
                DamageMult = 0.34f;
                Item.defense = 40;
                StatLifeBonus = 80;
            }
            if(StatLifeBonus > 0)
            {
                player.statLifeMax2 += StatLifeBonus;
            }
            player.GetDamage<KnifeDamageClass>() += DamageMult;
        }
    }
}