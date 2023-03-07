using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
	public class FieryKnives : KnifeItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tormenting Spikes");
			Tooltip.SetDefault("Forged from bits of hell that set enemies on fire");
        }
		public override void SafeSetDefaults()
		{
			Item.damage = 22;
            
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 18;
			Item.useAnimation = 18;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0,0,68,52);
			Item.rare = 8;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("FieryKnivesProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddIngredient(ItemID.LivingFireBlock, 12);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.LivingFireBlock, 8);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
	}

}
