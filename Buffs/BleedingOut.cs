using Terraria;
using Terraria.ModLoader;
using VKE.NPCs;

namespace VKE.Buffs
{
    public class BleedingOut : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bleeding Out");
            Description.SetDefault("Losing life");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<VampGlobalNPC>().bleedingOut = true;
        }
    }
}