

using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace vke
{
    public class vamprecipesystem : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.LivingFireBlock, 5);
            recipe.AddIngredient(ItemID.Obsidian, 5);
            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddCondition(Recipe.Condition.NearLava);
            recipe.Register();

            recipe = Recipe.Create(ItemID.LivingFireBlock, 25);
            recipe.AddIngredient(ItemID.Obsidian, 25);
            recipe.AddIngredient(ItemID.Fireblossom, 5);
            recipe.AddCondition(Recipe.Condition.NearLava);
            recipe.Register();

            recipe = Recipe.Create(ItemID.LivingFireBlock, 5);
            recipe.AddIngredient(ItemID.Hellstone, 5);
            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddCondition(Recipe.Condition.NearLava);
            recipe.Register();

            recipe = Recipe.Create(ItemID.LivingFireBlock, 25);
            recipe.AddIngredient(ItemID.Hellstone, 25);
            recipe.AddIngredient(ItemID.Fireblossom, 5);
            recipe.AddCondition(Recipe.Condition.NearLava);
            recipe.Register();
        }
    }
}
//    public class AmmoRecipe1 : Recipe
//    {
//        public AmmoRecipe1(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 0 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 20)
//                return true;
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe2 : Recipe
//    {
//        public AmmoRecipe2(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 19 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 50)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe3 : Recipe
//    {
//        public AmmoRecipe3(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 49 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 100)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe4 : Recipe
//    {
//        public AmmoRecipe4(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 99 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 150)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe5 : Recipe
//    {
//        public AmmoRecipe5(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 149 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 225)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe6 : Recipe
//    {
//        public AmmoRecipe6(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 224 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 300)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe7 : Recipe
//    {
//        public AmmoRecipe7(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 299 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 400)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe8 : Recipe
//    {
//        public AmmoRecipe8(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 399 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 500)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe9 : Recipe
//    {
//        public AmmoRecipe9(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 499 && Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted < 1000)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//    public class AmmoRecipe10 : Recipe
//    {
//        public AmmoRecipe10(Mod mod) : base(mod)
//        {
//        }

//        public override bool RecipeAvailable()
//        {
//            if (Main.LocalPlayer.GetModPlayer<VampPlayer>().NumCrafted >= 999)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//}