using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Projectiles.HardProj;
using VKE.Projectiles.SeasonalProj;

namespace VKE.Items.SeasonalKnives
{ 
	public class HarvestersSoul : KnifeItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Harvester's Soul");
			Tooltip.SetDefault("This mystic tool gives the user the power of the pumpking");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 5));

		}
		public override void SafeSetDefaults()
		{
			Item.damage = 18;            
			Item.width = 42;
			Item.height = 44;
			Item.useTime = 15;
			Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.channel = true;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0,0,54,20);
			Item.rare = 8;
			//item.UseSound = SoundID.Item97;
			Item.autoReuse = false;
            Item.shoot = Mod.Find<ModProjectile>("HarvestersSoulHeldProj").Type;
            Item.shootSpeed = 23f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = Mod.Find<ModProjectile>("HarvestersSoulProj2").Type;
                Item.useTime = 24;
                Item.useAnimation = 24;
                Item.UseSound = SoundID.Item117;
                Item.autoReuse = true;
                Item.channel = false;
            }
            else
            {
                Item.autoReuse = false;
                Item.channel = true;
                Item.useTime = 2;
                Item.useAnimation = 2;
                Item.shoot = Mod.Find<ModProjectile>("HarvestersSoulHeldProj").Type;
                Item.UseSound = null;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                damage = (int)(damage * (4.5f + (4 * player.GetModPlayer<VampPlayer>().ExtraProj/10)));
                int ran = 20;
                float spread = MathHelper.ToRadians(ran);
                float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
                double deltaAngle = spread / 5f;
                double offsetAngle = startAngle + deltaAngle * 2;
                int i = Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<HarvestersSoulProj2>(), damage, knockback, player.whoAmI);
                return false;
            }
            else
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        //      public override void AddRecipes()
        //{
        //	ModRecipe recipe = new ModRecipe(mod);
        //	recipe.AddIngredient(ItemID.HoneyBlock, 50);
        //          recipe.AddIngredient(mod.GetItem("IronKnives"), 1);
        //          recipe.AddTile(mod.GetTile("KnifeBench"));
        //	recipe.SetResult(this);
        //	recipe.AddRecipe();

        //          recipe = new ModRecipe(mod);
        //          recipe.AddIngredient(ItemID.HoneyBlock, 35);
        //          recipe.AddIngredient(mod.GetItem("IronKnives"), 1);
        //          recipe.AddTile(mod.GetTile("VampTableTile"));
        //          recipe.SetResult(this);
        //          recipe.AddRecipe();
        //      }
    }

}
