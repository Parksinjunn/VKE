using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace VKE.Buffs
{
    public class BleedingOutDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bleeding Out");
            Description.SetDefault("Losing life");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            //BuffID.Sets.NurseCannotRemoveDebuff /* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<VampPlayer>().SacrificialDebuff = true;
        }
    }
}