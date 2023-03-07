using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Ammo
{
    public class ThrowingKnivesCobalt : AmmoCraftItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Kunai Bundle");
            Tooltip.SetDefault("Bundle of 8 Kunai");
        }

        public override void SafeSetDefaults()
        {
            BarType = ItemID.CobaltBar;
            Item.damage = 6;
            Item.width = 66;
            Item.height = 66;
            Item.maxStack = 999;
            Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
            Item.knockBack = 1.5f;
            Item.crit = 4;
            Item.value = Item.sellPrice(0, 0, 4, 20);
            Item.rare = 0;
            Item.shoot = Mod.Find<ModProjectile>("KunaiProj").Type;   //The projectile shoot when your weapon using this ammo
            Item.shootSpeed = 6f;                  //The speed of the projectile
            Item.ammo = ModContent.ItemType<ThrowingKnivesIron>();              //The ammo class this ammo belongs to.
            NumProj = 8;
        }
    }
}
