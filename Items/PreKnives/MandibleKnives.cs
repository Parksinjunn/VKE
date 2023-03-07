using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;

namespace VKE.Items.PreKnives
{
    public class MandibleKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mandible Knives");
            Tooltip.SetDefault("Crafted from the remains of antlions, summons baby antlions that home in on enemies");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 5;             
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0,0,12,20);
            Item.rare = 1;  
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("MandibleProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AntlionMandible, 8);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AntlionMandible, 6);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
