using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Materials.Plates
{
    public class CrimtanePlate : PlateItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimtane Plate");
            BarType = ItemID.CrimtaneBar;
            Rarity = 1;
        }
    }
}