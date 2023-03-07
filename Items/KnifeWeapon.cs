using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace VKE.Items
{
    public class KnifeWeapon : GlobalItem
    {
        public int DamageLevel;
        public int CritLevel;
        public int LifeStealBonus;
        public float RicochetChance;
        public string OriginalOwner;
        public int PenetrationBonus;

        public int DamagePurchases;
        public int CritPurchases;
        public int LifeStealPurchases;
        public int RicochetPurchases;
        public int PenetrationPurchases;

        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;

        public KnifeWeapon()
        {
            OriginalOwner = "";
            DamageLevel = 0;
            CritLevel = 0;
            LifeStealBonus = 0;
            RicochetChance = 0;
            PenetrationBonus = 0;
            DamagePurchases = 1;
            CritPurchases = 1;
            LifeStealPurchases = 1;
            RicochetPurchases = 1;
            PenetrationPurchases = 1;
        }

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            KnifeWeapon myClone = (KnifeWeapon)base.Clone(item, itemClone);
            myClone.OriginalOwner = OriginalOwner;
            myClone.DamageLevel = DamageLevel;
            myClone.CritLevel = CritLevel;
            myClone.LifeStealBonus = LifeStealBonus;
            myClone.RicochetChance = RicochetChance;
            myClone.PenetrationBonus = PenetrationBonus;
            myClone.PenetrationPurchases = PenetrationPurchases;
            myClone.LifeStealPurchases = LifeStealPurchases;
            myClone.DamagePurchases = DamagePurchases;
            myClone.CritPurchases = CritPurchases;
            myClone.RicochetPurchases = RicochetPurchases;
            return myClone;
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.type == ItemID.CobaltOre || item.type == ItemID.PalladiumOre)
            {
                VampPlayer.HasHeldTier1 = true;
            }
            if (item.type == ItemID.OrichalcumOre || item.type == ItemID.MythrilOre)
            {
                VampPlayer.HasHeldTier1 = true;
                VampPlayer.HasHeldTier2 = true;
            }
            if (item.type == ItemID.AdamantiteOre || item.type == ItemID.TitaniumOre)
            {
                VampPlayer.HasHeldTier3 = true;
                VampPlayer.HasHeldTier2 = true;
                VampPlayer.HasHeldTier1 = true;
            }
            base.UpdateInventory(item, player);
        }
        //public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        //{
        //    mult *= 1 + DamageLevel / 10f;
        //}
        public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
        {
            if (item.crit < 10 && CritLevel > 7)
            {
                crit += (int)((10) * (1 + (CritLevel / 10f))) - item.crit;
            }
            else if (item.crit < 8 && CritLevel > 5)
            {
                crit += (int)((8) * (1 + (CritLevel / 10f))) - item.crit;
            }
            else if (item.crit < 7 && CritLevel > 4)
            {
                crit += (int)((7) * (1 + (CritLevel / 10f))) - item.crit;
            }
            else if (item.crit < 5 && CritLevel > 3)
            {
                crit += (int)((5) * (1 + (CritLevel / 10f))) - item.crit;
            }
            else if (item.crit < 4 && CritLevel > 2)
            {
                crit += (int)((4) * (1 + (CritLevel / 10f))) - item.crit;
            }
            else if (item.crit < 3 && CritLevel > 1)
            {
                crit += (int)((3) * (1 + (CritLevel / 10f))) - item.crit;
            }
            else if (item.crit < 2 && CritLevel > 0)
            {
                crit += (int)((2) * (1 + (CritLevel / 10f))) - item.crit;
            }
            else
            {
                crit += (int)(item.crit * (1 + (CritLevel / 10f))) - item.crit;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (OriginalOwner.Length > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Damage Mod: " + DamageLevel + "\nCrit Mod: " + CritLevel + "\nLifesteal: " + LifeStealBonus + "\nRicochet Chance: " + (System.Math.Truncate(RicochetChance*100)) + "%\nPenetration: " + PenetrationBonus)
                {
                    OverrideColor = Color.LimeGreen
                };
                tooltips.Add(line);

                foreach (TooltipLine line2 in tooltips)
                {
                    if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                    {
                        line2.Text = OriginalOwner + "'s " + line2.Text;
                    }
                }
            }
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            OriginalOwner = tag.GetString("OriginalOwner");
            DamageLevel = tag.GetInt("DamageLevel");
            CritLevel = tag.GetInt("CritLevel");
            LifeStealBonus = tag.GetInt("LifeStealBonus");
            PenetrationBonus = tag.GetInt("PenetrationBonus");
            RicochetChance = tag.GetFloat("RicochetChance");
            CritPurchases = tag.GetInt("CritPurchases");
            LifeStealPurchases = tag.GetInt("LifeStealPurchases");
            DamagePurchases = tag.GetInt("DamagePurchases");
            PenetrationPurchases = tag.GetInt("PenetrationPurchases");
            RicochetPurchases = tag.GetInt("RicochetPurchases");
        }
        //public override bool NeedsSaving(Item item)
        //{
        //    return OriginalOwner.Length > 0;
        //}

        public override void SaveData(Item item, TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            tag.Add("OriginalOwner", OriginalOwner);
            tag.Add("DamageLevel", DamageLevel);
            tag.Add("CritLevel", CritLevel);
            tag.Add("LifeStealBonus", LifeStealBonus);
            tag.Add("PenetrationBonus", PenetrationBonus);
            tag.Add("RicochetChance", RicochetChance);
            tag.Add("DamagePurchases", DamagePurchases);
            tag.Add("CritPurchases", CritPurchases);
            tag.Add("PenetrationPurchases", PenetrationPurchases);
            tag.Add("LifeStealPurchases", LifeStealPurchases);
            tag.Add("RicochetPurchases", RicochetPurchases);
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(OriginalOwner);
            writer.Write(DamageLevel);
            writer.Write(CritLevel);
            writer.Write(RicochetChance);
            writer.Write(LifeStealBonus);
            writer.Write(PenetrationBonus);
            writer.Write(CritPurchases);
            writer.Write(LifeStealPurchases);
            writer.Write(DamagePurchases);
            writer.Write(PenetrationPurchases);
            writer.Write(RicochetPurchases);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            OriginalOwner = reader.ReadString();
            DamageLevel = reader.ReadInt32();
            CritLevel = reader.ReadInt32();
            RicochetChance = reader.ReadSingle();
            LifeStealBonus = reader.ReadInt32();
            PenetrationBonus = reader.ReadInt32();
            CritPurchases = reader.ReadInt32();
            LifeStealPurchases = reader.ReadInt32();
            DamagePurchases = reader.ReadInt32();
            PenetrationPurchases = reader.ReadInt32();
            RicochetPurchases = reader.ReadInt32();
        }
    }
}
    

