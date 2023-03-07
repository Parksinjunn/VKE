using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials
{
    public class PlantFiber : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plant Fiber");
            Tooltip.SetDefault("Fiber of a Terrifying Plant Beast");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 0, 50, 0);
            base.SetDefaults();
        }
    }
}