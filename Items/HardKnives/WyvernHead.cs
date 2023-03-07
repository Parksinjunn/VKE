using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.HardKnives
{
    public class WyvernHead : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wyvern's Head");
            Tooltip.SetDefault("A clean-cut head of a wyvern that summons magical feathers that continue to hit whatever enemy they first hit");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 26;
            Item.crit = 6;
            Item.width = 44;
            Item.height = 50;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 7, 62, 22);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("WyvernProj").Type;
            Item.shootSpeed = 17f;
        }
    }

}
