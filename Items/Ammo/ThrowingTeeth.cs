using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Ammo
{
    public class ThrowingTeeth : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Teeth");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 12;
            Item.width = 48;
            Item.height = 48;
            Item.maxStack = 999;
            Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
            Item.knockBack = 1.5f;
            Item.crit = 50;
            Item.value = Item.sellPrice(0, 0, 0, 2);
            Item.rare = 3;
            Item.shoot = Mod.Find<ModProjectile>("ToothProj").Type;   //The projectile shoot when your weapon using this ammo
            Item.shootSpeed = 6f;                  //The speed of the projectile
            Item.ammo = Item.type;              //The ammo class this ammo belongs to.
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddIngredient(ItemID.Bone, 1);
            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe(35);
            recipe.AddIngredient(ItemID.Bone, 3);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }
}
