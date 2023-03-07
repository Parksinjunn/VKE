using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Buffs.HoodBuffs
{
    public class MageHoodBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cultist's Curse");
            Description.SetDefault("Frozen, but un-limited pow-ah!");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            VampPlayer p = player.GetModPlayer<VampPlayer>();

            // We use blockyAccessoryPrevious here instead of blockyAccessory because UpdateBuffs happens before UpdateEquips but after ResetEffects.
            if (p.HoodIsVisible == true && p.MageAccessoryPrevious)
            {
                p.MagePower = true;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }

        }
    }
}