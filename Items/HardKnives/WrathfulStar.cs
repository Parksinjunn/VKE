using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Items.PreKnives;
using VKE.Tiles;

namespace VKE.Items.HardKnives
{
    public class WrathfulStar : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrathful Star");
            Tooltip.SetDefault("Summons large knives from up above like it's wrathful brother");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 180;        
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 100000; 
            Item.rare = 9;  
          
 
            Item.UseSound = SoundID.Item60 with { Volume = 0.05f}; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("WrathfulStarProj").Type;
            Item.shootSpeed = 15f;

            Item.useAnimation = 9;
            Item.useTime = 2;
            Item.reuseDelay = 11;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProjectiles2 = 1 + player.GetModPlayer<VampPlayer>().ExtraProj;
            for (int x = 0; x < numProjectiles2; x += 1)
            {
                Vector2 StartPosition = Main.MouseWorld;
                StartPosition.X += Main.rand.Next(-200, 200);
                StartPosition.Y = player.position.Y - Main.screenHeight/1.5f;
                Vector2 ProjectileVelocity;

                float shootToX = Main.MouseWorld.X - StartPosition.X + Main.rand.Next(-75,75);
                float shootToY = Main.MouseWorld.Y - StartPosition.Y;
                float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                distance = 5.4f / distance;

                //Multiply the distance by a multiplier if you wish the projectile to have go faster
                shootToX *= distance * 5;
                shootToY *= distance * 5;

                //Set the velocities to the shoot values
                ProjectileVelocity.X = shootToX;
                ProjectileVelocity.Y = shootToY;

                Projectile.NewProjectile(source, StartPosition.X, StartPosition.Y, ProjectileVelocity.X, ProjectileVelocity.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StarWrath);
            recipe.AddIngredient(ModContent.ItemType<StarfuryKnives>());
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StarWrath);
            recipe.AddIngredient(ModContent.ItemType<StarfuryKnives>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
