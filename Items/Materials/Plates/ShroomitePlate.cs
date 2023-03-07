using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials.Plates
{
    public class ShroomitePlate : PlateItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Plate");
            BarType = ItemID.ShroomiteBar;
            Rarity = 7;
        }
    }
}