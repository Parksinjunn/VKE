using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;
using System.Linq;
using VKE.Projectiles.PostMLProj;

namespace VKE.Items.PostMLKnives
{
    public class MoonLordsHand : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hand of the Moon Lord");
            Tooltip.SetDefault("Life Stealing Knives Crafted from Fragments of the Moon Lord himself");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(52, 13));
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 126;
            Item.width = 66;
            Item.height = 66;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 45, 0, 0);
            Item.rare = 9;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<MoonLordsHandProj>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity *= Main.rand.NextFloat(0.7f, 1.4f);
            int numProjectiles2 = NumProj + player.GetModPlayer<VampPlayer>().ExtraProj;
            Random random = new Random();
            int ran = random.Next(0, 110);
            float spread = MathHelper.ToRadians(Main.rand.Next(90));
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;
            offsetAngle = startAngle + deltaAngle * Main.rand.Next(1, numProjectiles2);

            Projectile.NewProjectile(source, position, new Vector2(baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle)), ModContent.ProjectileType<MoonLordsHandProj>(), damage, knockback, player.whoAmI);
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int R = 102;
            int G = 255;
            int B = 255;
            bool GDecrease = false;
            bool RDecrease = false;
            if (R >= 102)
                RDecrease = true;
            if (R <= 51)
                RDecrease = false;
            if (RDecrease)
                R++;
            if (!RDecrease)
                R--;

            if (G >= 255)
                GDecrease = true;
            if (G <= 102)
                GDecrease = false;
            if (GDecrease)
                G++;
            if (!GDecrease)
                G--;
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(R, G, B);
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
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddIngredient(ItemID.FragmentVortex, 8);
            recipe.AddIngredient(ItemID.FragmentNebula, 8);
            recipe.AddIngredient(ItemID.FragmentSolar, 8);
            recipe.AddIngredient(ItemID.FragmentStardust, 8);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LuminiteKnives>());
            recipe.Register();

            //if (Calamity != null)
            //{
            //    recipe = CreateRecipe();
            //    recipe.AddIngredient(ItemID.LunarBar, 15);
            //    recipe.AddIngredient(ModLoader.GetMod("CalamityMod"), "GalacticaSingularity", 8);
            //    recipe.AddIngredient(ItemID.VampireKnives, 1);
            //    recipe.AddTile(ModContent.TileType<VampTableTile>());
            //    recipe.Register();
            //}
        }
    }

}
