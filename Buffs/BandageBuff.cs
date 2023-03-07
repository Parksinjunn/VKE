using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace VKE.Buffs
{
    public class BandageBuff : ModBuff
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bandaged");
            Description.SetDefault("");
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<VampPlayer>().Bandaged = true;
            base.Update(player, ref buffIndex);
        }
    }
}