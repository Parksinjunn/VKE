using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials
{
    public class LivingTissue : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Defaults
            DisplayName.SetDefault("Living Tissue");
            Tooltip.SetDefault("It's still moving...");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(52, 4));
        }
        public override void SetDefaults()
        {
            Item.width = 32;
                Item.height = 32;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0,5,30,0);
            base.SetDefaults();
        }
    }
}
