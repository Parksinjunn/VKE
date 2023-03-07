using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class CorruptionNestKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corruption Nest");
            Tooltip.SetDefault("Nest filled with larval eaters");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 16; 
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item39; 
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("CorruptionNestKnivesProj").Type;
            Item.shootSpeed = 12f;
            Item.scale = 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Toxikarp);
            recipe.AddIngredient(ItemID.DemoniteBar, 12);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Toxikarp);
            recipe.AddIngredient(ItemID.DemoniteBar, 7);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
