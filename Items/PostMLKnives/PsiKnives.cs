using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Projectiles.PostMLProj;
using VKE.Items.Materials;
using VKE.Tiles;
using System.Linq;

namespace VKE.Items.PostMLKnives
{
	public class PsiKnives : KnifeItem
	{
        protected override bool CloneNewInstances => true;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Psi");
			//Tooltip.SetDefault("Knives with an internal compartment \nfilled with bees that explode out on impact");
        }
		public override void SafeSetDefaults()
		{
			Item.damage = 160;            
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 45;
			Item.useAnimation = 45;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0,0,54,20);
            Item.UseSound = SoundID.Item39;
			Item.rare = 8;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("PsiKnivesProj").Type;
            Item.shootSpeed = 9f;
        }
        int FrameCounter;
        int Frame;
        int TimesToRepeat;
        Texture2D texture;
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if(FrameCounter >= 8)
            {
                FrameCounter = 0;
                Frame++;
                if(Frame == 3)
                {
                    TimesToRepeat = Main.rand.Next(1, 5);
                }
                if(Frame > 7 && TimesToRepeat >= 1)
                {
                    TimesToRepeat--;
                    Frame = 4;
                }
                if(Frame > 9)
                {
                    Frame = 0;
                }
            }
            if(Frame == 0)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/0").Value;
            else if (Frame == 1)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/1").Value;
            else if (Frame == 2)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/2").Value;
            else if (Frame == 3)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/3").Value;
            else if (Frame == 4)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/4").Value;
            else if (Frame == 5)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/5").Value;
            else if (Frame == 6)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/6").Value;
            else if (Frame == 7)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/7").Value;
            else if (Frame == 8)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/8").Value;
            else if (Frame == 9)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/PsiKnivesFrames/9").Value;

            spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            FrameCounter++;
            base.UpdateInventory(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        int Mode = 0;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Mode == 0)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Sticky")
                {
                    OverrideColor = Color.Green
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Shoots sticky psi bombs that attach to tiles or npcs")
                {
                    OverrideColor = Color.LightGreen
                };
                tooltips.Add(line2);
            }
            else if(Mode == 1)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Bombing")
                {
                    OverrideColor = Color.Red
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Shoots psi bombs that fall quickly and prime themselves upon collision with a tile")
                {
                    OverrideColor = Color.PaleVioletRed
                };
                tooltips.Add(line2);
            }
            else if (Mode == 2)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Defense")
                {
                    OverrideColor = Color.Cyan
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Shoots psi bombs in a defensive pattern that will only explode once they reach their primed distance")
                {
                    OverrideColor = Color.LightCyan
                };
                tooltips.Add(line2);
            }
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.Text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.Text = damageValue + " Knife Damage";
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.autoReuse = false;
                Mode++;
                if (Mode > 2)
                {
                    Mode = 0;
                }
                if (Mode == 0)
                {
                    Item.autoReuse = true;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Green, "Mode: Sticky", true);
                }
                else if (Mode == 1)
                {
                    Item.autoReuse = true;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Red, "Mode: Bombing", true);
                }
                else if (Mode == 2)
                {
                    Item.autoReuse = true;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Cyan, "Mode: Defense", true);
                }
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }

            int numProjectiles2 = 3 + player.GetModPlayer<VampPlayer>().ExtraProj;
            Random random = new Random();
            int ran = 20;
            float spread = MathHelper.ToRadians(ran);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;

            for (int j = 0; j < numProjectiles2; j++)
            {
                offsetAngle = startAngle + deltaAngle * j;
                int i = Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
                var p = Main.projectile[i].ModProjectile as PsiKnivesProj;
                p.Mode = Mode;
            }
            return false;

            //return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarFlareBook);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.FragmentVortex, 16);
            recipe.AddIngredient(ModContent.ItemType<ProcessingUnit>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
