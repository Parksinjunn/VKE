using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Tiles
{
    public class AltarPillar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = Mod.Find<ModTile>("AltarPillar").Type;
        }
    }
}