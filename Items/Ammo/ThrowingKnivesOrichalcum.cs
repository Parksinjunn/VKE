using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Ammo
{
    public class ThrowingKnivesOrichalcum : AmmoCraftItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orichalcum Spikes");
            Tooltip.SetDefault("Stick into enemies, damaging them over time");
        }

        public override void SafeSetDefaults()
        {
            BarType = ItemID.OrichalcumBar;
            Item.damage = 1;
            Item.width = 48;
            Item.height = 48;
            Item.maxStack = 999;
            Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
            Item.knockBack = 1.5f;
            Item.crit = 4;
            Item.value = Item.sellPrice(0, 0, 8, 80);
            Item.rare = 3;
            Item.shoot = Mod.Find<ModProjectile>("OrichalcumProj").Type;   //The projectile shoot when your weapon using this ammo
            Item.shootSpeed = 6f;                  //The speed of the projectile
            Item.ammo = ModContent.ItemType<ThrowingKnivesIron>();              //The ammo class this ammo belongs to.
        }
    }
}
