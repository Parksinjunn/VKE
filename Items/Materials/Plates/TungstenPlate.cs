using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials.Plates
{
    public class TungstenPlate : PlateItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tungsten Plate");
            BarType = ItemID.TungstenBar;
            Rarity = 0;
        }
    }
}