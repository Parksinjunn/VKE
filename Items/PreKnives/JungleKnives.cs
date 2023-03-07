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
	public class JungleKnives : KnifeItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viney Knives");
			Tooltip.SetDefault("Knives made of jungle material that poison enemies");
        }
		public override void SafeSetDefaults()
		{
            Item.damage = 7;    
			Item.width = 78;
			Item.height = 78;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.useStyle = 1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 0, 25, 45) ;
			Item.rare = 8;
			Item.UseSound = SoundID.Item39;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("JungleKnivesProj").Type;
            Item.shootSpeed = 12f;
			NumProj = 3;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.JungleSpores, 16);
            recipe.AddIngredient(ItemID.Stinger, 16);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.JungleSpores, 12);
            recipe.AddIngredient(ItemID.Stinger, 12);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
	}

}
