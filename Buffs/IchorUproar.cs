using Terraria;
using Terraria.ModLoader;
using VKE.NPCs;

namespace VKE.Buffs
{
    public class IchorUproar : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Definitely on Fire");
            Description.SetDefault("Losing life");
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<VampGlobalNPC>().ichorUproar = true;
        }
    }
}