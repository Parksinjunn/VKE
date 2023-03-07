using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;
using VKE.Projectiles.HardProj;

namespace VKE.Items.HardKnives
{
    public class Nyives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nyives");
            Tooltip.SetDefault("A set of three knives, two smol and one big chonker\nThe chonky knife sticks into the ground and continues \nto damage enemies that walk into it");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 180; 
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 4.25f;
            Item.value = Item.sellPrice(5, 0, 0, 0);
            Item.rare = 10;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("NyivesSmallProj").Type;
            Item.shootSpeed = 15f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProjectiles2 = 3 + player.GetModPlayer<VampPlayer>().ExtraProj;
            Random random = new Random();
            int ran = 40;
            float spread = MathHelper.ToRadians(ran);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;

            for (int j = 0; j < numProjectiles2; j++)
            {
                offsetAngle = startAngle + deltaAngle * j;
                if(numProjectiles2 <= 5)
                {
                    if (j == (numProjectiles2 / 2))
                    {
                        Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<NyivesBigProj>(), damage, knockback, player.whoAmI);
                    }
                    else
                        Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
                }
                else if(numProjectiles2 > 5)
                {
                    if (j == 2 || j == 3)
                    {
                        Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<NyivesBigProj>(), damage, knockback, player.whoAmI);
                    }
                    else
                        Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Meowmere, 1);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Meowmere, 1);
            recipe.AddIngredient(ItemID.LunarBar, 14);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();


            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Meowmere, 1);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Meowmere, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
