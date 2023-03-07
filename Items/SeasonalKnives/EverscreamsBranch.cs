using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace VKE.Items.SeasonalKnives
{ 
	public class EverscreamsBranch : KnifeItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Everscream's Branch");
            //Tooltip.SetDefault("This mystic tool gives the user the power of the pumpking");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(14, 8));
        }
        public override void SafeSetDefaults()
		{
			Item.damage = 18;            
			Item.width = 48;
			Item.height = 48;
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
            Item.shoot = Mod.Find<ModProjectile>("EverscreamsBranchHeldProj").Type;
            Item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
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
