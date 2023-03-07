using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.DataStructures;
using Terraria.Localization;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;
using System.Linq;

namespace VKE.Items.HardKnives
{
    public class PillarKnives : KnifeItem
    {
        //Mod Calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astronomical Singularity");
            Tooltip.SetDefault("Shoots a random assortment of all astral knives");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 86;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(1, 80, 15, 0);
            Item.rare = -13;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("NebulaProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.Text = "[c/FF3300:A][c/DE6E41:s][c/BCA881:t][c/9BE3C2:r][c/B29BD0:o][c/C953DD:n][c/E00BEB:o][c/A047EE:m][c/6083F2:i][c/20BFF5:c][c/6083F2:a][c/A047EE:l] [c/E00BEB:S][c/C953DD:i][c/B29BD0:n][c/9BE3C2:g][c/BCA881:u][c/DE6E41:l][c/FF3300:a][c/DE6E41:r][c/BCA881:i][c/9BE3C2:t][c/B29BD0:y]";
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
            recipe.AddIngredient(ModContent.ItemType<NebulaKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<VortexKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SolarKnives>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StardustKnives>(), 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            //if (Calamity != null)
            //{
            //    recipe = CreateRecipe();
            //    recipe.AddIngredient(ModLoader.GetMod("CalamityMod"), "GalacticaSingularity", 18);
            //    recipe.AddTile(ModContent.TileType<VampTableTile>());
            //    recipe.Register();
            //}

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 18);
            recipe.AddIngredient(ItemID.FragmentStardust, 18);
            recipe.AddIngredient(ItemID.FragmentVortex, 18);
            recipe.AddIngredient(ItemID.FragmentNebula, 18);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProjectiles2 = player.GetModPlayer<VampPlayer>().NumProj + player.GetModPlayer<VampPlayer>().ExtraProj;
            Random random = new Random();
            int ran = random.Next(10, 35);
            float spread = MathHelper.ToRadians(ran);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;

            for (int j = 0; j < numProjectiles2; j++)
            {
                offsetAngle = startAngle + deltaAngle * j;
                int knifeSelect = Main.rand.Next(0, 4);
                if (knifeSelect == 0)
                    Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("NebulaProj").Type, damage, knockback, player.whoAmI);
                if (knifeSelect == 1)
                    Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("SolarProj").Type, damage, knockback, player.whoAmI);
                if (knifeSelect == 2)
                    Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("StardustProj").Type, damage, knockback, player.whoAmI);
                if (knifeSelect == 3)
                    Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("VortexProj").Type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }

}
