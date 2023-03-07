using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.SeasonalKnives
{
    public class Scimitars : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scimitars");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 22;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 12, 50, 40);
            Item.rare = -11;
            Item.UseSound = SoundID.Item39;
            Item.shoot = Mod.Find<ModProjectile>("ScimitarProj").Type;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.Text = "[c/D4AF37:Scimitars]";
                }
            }
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.Text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.Text = damageValue + " Knife Damage";
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DyeTradersScimitar, 1);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
