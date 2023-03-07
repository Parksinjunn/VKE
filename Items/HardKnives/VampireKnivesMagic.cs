using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.HardKnives
{
    public class VampireKnivesMagic : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampire Knives");
            Tooltip.SetDefault("Rapidly throws life stealing daggers");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 29; 
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 2.75f;
            Item.value = Item.sellPrice(0,20,0,0);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("WeakVampireProj").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = Recipe.Create(ItemID.VampireKnives);
            recipe.AddIngredient(this);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();
        }
    }

}
