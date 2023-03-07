using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class StarfuryKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Knifefury");
            Tooltip.SetDefault("It's a bird! It's a plane? It's a... kni- *thwump*\nSummons knives from up above like it's swung brother");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 22;        
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 100000; 
            Item.rare = 2;  
          
 
            Item.UseSound = SoundID.Item60 with { Volume = 0.15f}; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("StarfuryKnivesProj").Type;
            Item.shootSpeed = 25f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProjectiles2 = NumProj + player.GetModPlayer<VampPlayer>().ExtraProj;
            for (int x = 0; x < numProjectiles2; x+= 2)
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

                Projectile.NewProjectile(source, new Vector2(StartPosition.X, StartPosition.Y), new Vector2(ProjectileVelocity.X, ProjectileVelocity.Y), type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Starfury);
            recipe.AddIngredient(ModContent.ItemType<WeakVampireKnives>());
            recipe.AddIngredient(ItemID.SunplateBlock, 15);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Starfury);
            recipe.AddIngredient(ModContent.ItemType<WeakVampireKnives>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
