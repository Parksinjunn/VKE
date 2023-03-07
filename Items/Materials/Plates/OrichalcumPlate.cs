using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials.Plates
{
    public class OrichalcumPlate : PlateItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orichalcum Plate");
            BarType = ItemID.OrichalcumBar;
            Rarity = 4;
        }
    }
}