using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials
{
    public class RicochetEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Defaults
            DisplayName.SetDefault("Ricochet Essence");
            Tooltip.SetDefault("Essence of a once very bouncy thing\nUsed in upgrading your knives");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(1, 9));
        }
        public override void SetDefaults()
        {
            Item.width = 4;
            Item.height = 23;
            Item.maxStack = 999;
            //item.value = Item.sellPrice(0, 5, 30, 0);
            base.SetDefaults();
        }
    }
}
