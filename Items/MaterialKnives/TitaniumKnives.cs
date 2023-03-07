using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Projectiles.Ammo;
using VKE.Tiles;

namespace VKE.Items.MaterialKnives
{
    public class TitaniumKnives : KnifeMaterialItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Knives");
            Tooltip.SetDefault("Decreases the defense of hit enemies");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 15;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 3, 85, 60);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.shoot = Mod.Find<ModProjectile>("TitaniumProj").Type;
            Item.autoReuse = true;
            Item.shootSpeed = 8f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddTile(TileID.Furnaces);
                        recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 6);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
                        recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            int numProjectiles2 = NumProj + player.GetModPlayer<VampPlayer>().ExtraProj;

            float spread = MathHelper.ToRadians(180);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;

            for (int j = 0; j < numProjectiles2; j++)
            {
                offsetAngle = startAngle + deltaAngle * j;
                int i = Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle)), type, damage, knockback, player.whoAmI);
                var p = Main.projectile[i].ModProjectile as TitaniumProj;
                p.TargetPos = Main.MouseWorld;
            }
            return false;
        }
    }

}
