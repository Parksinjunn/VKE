using Terraria.ID;
using System;
using Terraria;
using Terraria.ModLoader;
using VKE.NPCs;

namespace VKE.Buffs
{
    public class PenetratingPoison : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Penetrating Poison");
            Description.SetDefault("So potent it degrades bones");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<VampPlayer>().PenetratingPoison = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<VampGlobalNPC>().PenetratingPoison = true;
        }
    }
}
