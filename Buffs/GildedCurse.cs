using Terraria;
using Terraria.ModLoader;
using VKE.NPCs;

namespace VKE.Buffs
{
    public class GildedCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Definitely on Fire");
            Description.SetDefault("Losing life");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<VampGlobalNPC>().gildedCurse = true;
            npc.velocity.X = npc.velocity.X * 0.5f;
            npc.velocity.Y = npc.velocity.Y * 0.5f;
            npc.extraValue = (int)(npc.value * 5f);
        }
    }
}