using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials.Plates
{
    public class IronPlate : PlateItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Plate");
            BarType = ItemID.IronBar;
            Rarity = 0;
        }
    }
}