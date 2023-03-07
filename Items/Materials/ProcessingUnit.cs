using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials
{
    public class ProcessingUnit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Processing Unit");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(gold: 1);      
            base.SetDefaults();
        }
    }
}
