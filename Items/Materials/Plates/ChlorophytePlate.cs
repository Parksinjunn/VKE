using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials.Plates
{
    public class ChlorophytePlate : PlateItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorophyte Plate");
            BarType = ItemID.ChlorophyteBar;
            Rarity = 7;
        }
    }
}