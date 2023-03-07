using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials.Plates
{
    public class LuminitePlate : PlateItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminite Plate");
            BarType = ItemID.LunarBar;
            Rarity = 10;
        }
    }
}