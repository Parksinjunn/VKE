using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.Ammo;
using VKE.Projectiles.Ammo;
using VKE.Tiles;

namespace VKE.Items.MaterialKnives
{
    public class CobaltKunai : KnifeMaterialItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Kunai");
            Tooltip.SetDefault("Fires 10 kunai or 7 knives at once\nDecreases the defense of hit enemies");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 8;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 2, 5, 20);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("IronProj").Type;
            Item.shootSpeed = 15f;
            Item.useAmmo = ModContent.ItemType<ThrowingKnivesIron>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProjectiles2;
            if (type == ModContent.ProjectileType<KunaiProj>())
            {
                numProjectiles2 = NumProj + player.GetModPlayer<VampPlayer>().ExtraProj + 5;
            }
            else
                numProjectiles2 = NumProj + player.GetModPlayer<VampPlayer>().ExtraProj + 2; 
            int ran = Main.rand.Next(10, 35);
            float spread = MathHelper.ToRadians(ran);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;

            for (int j = 0; j < numProjectiles2; j++)
            {
                offsetAngle = startAngle + deltaAngle * j;
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle)), type, (int)(damage * 0.75f), knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 8);
            recipe.AddTile(TileID.Furnaces);
                        recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 6);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
                        recipe.Register();
        }
    }

}
