using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Ammo
{
    public class ThrowingKnivesIron : AmmoCraftItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwing Knives");
        }

        public override void SafeSetDefaults()
        {
            BarType = ItemID.IronBar;
            Item.damage = 1;
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 999;
            Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
            Item.knockBack = 1.5f;
            Item.crit = 4;
            Item.value = Item.sellPrice(0, 0, 0, 50) ;
            Item.rare = 2;
            Item.shoot = Mod.Find<ModProjectile>("IronProj").Type;   //The projectile shoot when your weapon using this ammo
            Item.shootSpeed = 6f;                  //The speed of the projectile
            Item.ammo = Item.type;              //The ammo class this ammo belongs to.
        }
    }
}
