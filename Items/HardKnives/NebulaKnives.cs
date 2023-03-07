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
    public class NebulaKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nebula Knives");
            Tooltip.SetDefault("Crafted from pure nebula, spawns a nebula blaze upon hitting an enemy");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 68;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 10, 50, 40);
            Item.rare = 11;
            Item.UseSound = SoundID.Item39;
            Item.shoot = Mod.Find<ModProjectile>("NebulaProj").Type;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.Text = "[c/E00BEB:Ne][c/C10BCC:bu][c/A20BAD:la] [c/830A8D:Kn][c/640A6E:iv][c/450A4F:es]";
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
            recipe.AddIngredient(ItemID.FragmentNebula, 18);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
