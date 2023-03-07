using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.HardKnives
{
    public class StardustKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Knives");
            Tooltip.SetDefault("Crafted out of pure stardust\nSummons a stardust cell upon hitting an enemy");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 69;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 10, 50, 40);
            Item.rare = 9;
            Item.UseSound = SoundID.Item39;
            Item.shoot = Mod.Find<ModProjectile>("StardustProj").Type;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.Text = "[c/20BFF5:St][c/1CA8D7:ar][c/1891B8:du][c/147A9A:st] [c/10637C:Kn][c/0C4C5D:iv][c/08353F:es]";
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
            recipe.AddIngredient(ItemID.FragmentStardust, 18);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
