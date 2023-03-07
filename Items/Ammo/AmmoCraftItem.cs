using System.Collections.Generic;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Ammo
{
    public abstract class AmmoCraftItem : KnifeItem
    {
        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }
        public bool crafted;
        int NumberCrafted;
        public short BarType;
        public override void OnCraft(Recipe recipe)
        {
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            crafted = true;
            p.NumCrafted += 1;
            NumberCrafted = p.NumCrafted;
        }
        public override void UpdateInventory(Player player)
        {
            crafted = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            if (BarType == ItemID.IronBar || BarType == ItemID.LeadBar)
                recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            else
                recipe.AddIngredient(BarType, 1);

            recipe.AddTile(ModContent.TileType<KnifeBench>());
            recipe.Register();

            recipe = CreateRecipe(20);
            if (BarType == ItemID.IronBar || BarType == ItemID.LeadBar)
                recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            else
                recipe.AddIngredient(BarType, 1);

            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();

            //AmmoRecipe2 recipe2 = new AmmoRecipe2(Mod, this.Type, 20);
            //recipe2.AddIngredient(BarType, 1);
            //recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            //recipe2.AddTile(null, "KnifeBench");
            //recipe2.AddRecipe();

            //recipe2 = new AmmoRecipe2(Mod, this.Type, 30);
            //recipe2.AddIngredient(BarType, 1);
            //recipe.AddRecipeGroup(RecipeGroupID.IronBar);
            //recipe2.AddTile(null, "VampTableTile");
            //recipe2.AddRecipe();

            //AmmoRecipe3 Recipe3 = new AmmoRecipe3(Mod, this.Type, 30);
            //Recipe3.AddIngredient(BarType, 1);
            //Recipe3.anyIronBar = true;
            //Recipe3.AddTile(null, "KnifeBench");
            //Recipe3.AddRecipe();

            //Recipe3 = new AmmoRecipe3(Mod, this.Type, 40);
            //Recipe3.AddIngredient(BarType, 1);
            //Recipe3.anyIronBar = true;
            //Recipe3.AddTile(null, "VampTableTile");
            //Recipe3.AddRecipe();

            //AmmoRecipe4 Recipe4 = new AmmoRecipe4(Mod, this.Type, 35);
            //Recipe4.AddIngredient(BarType, 1);
            //Recipe4.anyIronBar = true;
            //Recipe4.AddTile(null, "KnifeBench");
            //Recipe4.AddRecipe();

            //Recipe4 = new AmmoRecipe4(Mod, this.Type, 40);
            //Recipe4.AddIngredient(BarType, 1);
            //Recipe4.anyIronBar = true;
            //Recipe4.AddTile(null, "VampTableTile");
            //Recipe4.AddRecipe();

            //AmmoRecipe5 Recipe5 = new AmmoRecipe5(Mod, this.Type, 50);
            //Recipe5.AddIngredient(BarType, 1);
            //Recipe5.anyIronBar = true;
            //Recipe5.AddTile(null, "KnifeBench");
            //Recipe5.AddRecipe();

            //Recipe5 = new AmmoRecipe5(Mod, this.Type, 60);
            //Recipe5.AddIngredient(BarType, 1);
            //Recipe5.anyIronBar = true;
            //Recipe5.AddTile(null, "VampTableTile");
            //Recipe5.AddRecipe();

            //AmmoRecipe6 Recipe6 = new AmmoRecipe6(Mod, this.Type, 65);
            //Recipe6.AddIngredient(BarType, 1);
            //Recipe6.anyIronBar = true;
            //Recipe6.AddTile(null, "KnifeBench");
            //Recipe6.AddRecipe();

            //Recipe6 = new AmmoRecipe6(Mod, this.Type, 75);
            //Recipe6.AddIngredient(BarType, 1);
            //Recipe6.anyIronBar = true;
            //Recipe6.AddTile(null, "VampTableTile");
            //Recipe6.AddRecipe();

            //AmmoRecipe7 Recipe7 = new AmmoRecipe7(Mod, this.Type, 75);
            //Recipe7.AddIngredient(BarType, 1);
            //Recipe7.anyIronBar = true;
            //Recipe7.AddTile(null, "KnifeBench");
            //Recipe7.AddRecipe();

            //Recipe7 = new AmmoRecipe7(Mod, this.Type, 85);
            //Recipe7.AddIngredient(BarType, 1);
            //Recipe7.anyIronBar = true;
            //Recipe7.AddTile(null, "VampTableTile");
            //Recipe7.AddRecipe();

            //AmmoRecipe8 Recipe8 = new AmmoRecipe8(Mod, this.Type, 85);
            //Recipe8.AddIngredient(BarType, 1);
            //Recipe8.anyIronBar = true;
            //Recipe8.AddTile(null, "KnifeBench");
            //Recipe8.AddRecipe();

            //Recipe8 = new AmmoRecipe8(Mod, this.Type, 95);
            //Recipe8.AddIngredient(BarType, 1);
            //Recipe8.anyIronBar = true;
            //Recipe8.AddTile(null, "VampTableTile");
            //Recipe8.AddRecipe();

            //AmmoRecipe9 Recipe9 = new AmmoRecipe9(Mod, this.Type, 125);
            //Recipe9.AddIngredient(BarType, 1);
            //Recipe9.anyIronBar = true;
            //Recipe9.AddTile(null, "KnifeBench");
            //Recipe9.AddRecipe();

            //Recipe9 = new AmmoRecipe9(Mod, this.Type, 150);
            //Recipe9.AddIngredient(BarType, 1);
            //Recipe9.anyIronBar = true;
            //Recipe9.AddTile(null, "VampTableTile");
            //Recipe9.AddRecipe();

            //AmmoRecipe10 Recipe10 = new AmmoRecipe10(Mod, this.Type, 500);
            //Recipe10.AddIngredient(BarType, 1);
            //Recipe10.anyIronBar = true;
            //Recipe10.AddTile(null, "KnifeBench");
            //Recipe10.AddRecipe();

            //Recipe10 = new AmmoRecipe10(Mod, this.Type, 999);
            //Recipe10.AddIngredient(BarType, 1);
            //Recipe10.anyIronBar = true;
            //Recipe10.AddTile(null, "VampTableTile");
            //Recipe10.AddRecipe();
        }
    }
}
