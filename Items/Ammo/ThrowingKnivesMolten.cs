﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Ammo
{
    public class ThrowingKnivesMolten : AmmoCraftItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Throwing Knives");
        }

        public override void SafeSetDefaults()
        {
            BarType = ItemID.MeteoriteBar;
            Item.damage = 5;
            Item.width = 48;
            Item.height = 48;
            Item.maxStack = 999;
            Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
            Item.knockBack = 1.5f;
            Item.crit = 4;
            Item.value = Item.sellPrice(0, 0, 2, 80);
            Item.rare = 3;
            Item.shoot = Mod.Find<ModProjectile>("MoltenProj").Type;   //The projectile shoot when your weapon using this ammo
            Item.shootSpeed = 6f;                  //The speed of the projectile
            Item.ammo = ModContent.ItemType<ThrowingKnivesIron>();              //The ammo class this ammo belongs to.
        }
    }
}
