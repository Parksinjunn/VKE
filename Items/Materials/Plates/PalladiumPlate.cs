using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials.Plates
{
    public class PalladiumPlate : PlateItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Plate");
            BarType = ItemID.PalladiumBar;
            Rarity = 4;
        }
    }
}