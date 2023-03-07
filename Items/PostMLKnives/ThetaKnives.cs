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
	public class ThetaKnives : KnifeItem
	{
        protected override bool CloneNewInstances => true;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Theta");
			//Tooltip.SetDefault("Knives with an internal compartment \nfilled with bees that explode out on impact");
        }
		public override void SafeSetDefaults()
		{
			Item.damage = 69;            
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 15;
			Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0,0,54,20);
			Item.rare = 8;
			Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ThetaKnivesProj>();
            Item.shootSpeed = 9f;
            Item.channel = true;
        }
        int Frame;
        Texture2D texture;
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Frame == 0)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/ThetaKnives").Value;
            else if (Frame == 1)
                texture = ModContent.Request<Texture2D>("VKE/Items/PostMLKnives/ThetaKnivesHovering").Value;

            spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
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
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Target")
                {
                    OverrideColor = Color.Green
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Shoots theta spears at your mouse cursor")
                {
                    OverrideColor = Color.LightGreen
                };
                tooltips.Add(line2);
            }
            else if(Mode == 1)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Carapace")
                {
                    OverrideColor = Color.Red
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Creates a protective carapace of spears around the player")
                {
                    OverrideColor = Color.PaleVioletRed
                };
                tooltips.Add(line2);
            }
            else if (Mode == 2)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Follow")
                {
                    OverrideColor = Color.Cyan
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Creates a moving carapace of spears that follow your mouse cursor")
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
        public override void UpdateInventory(Player player)
        {
            if(Main.mouseLeft && player.HeldItem.type == ModContent.ItemType<ThetaKnives>())
            {
                Frame = 1;
            }
            else if(Main.mouseLeftRelease && player.HeldItem.type == ModContent.ItemType<ThetaKnives>())
            {
                Frame = 0;
            }
            base.UpdateInventory(player);
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
                    Item.useStyle = 5;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Green, "Mode: Target", true);
                }
                else if (Mode == 1)
                {
                    Item.autoReuse = true;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Red, "Mode: Carapace", true);
                }
                else if (Mode == 2)
                {
                    Item.autoReuse = true;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Cyan, "Mode: Follow", true);
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

            //int numProjectiles2 = 5 + player.GetModPlayer<VampPlayer>().ExtraProj;
            //Random random = new Random();
            //int ran = 20;
            //float spread = MathHelper.ToRadians(ran);
            //float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            //double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            //double deltaAngle = spread / (float)numProjectiles2;
            //double offsetAngle;

            //for (int j = 0; j < numProjectiles2; j++)
            //{
            //    offsetAngle = startAngle + deltaAngle * j;
            //    int i = Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
            //    var p = Main.projectile[i].modProjectile as DeltaKnivesProj;
            //    p.Mode = Mode;
            //    if(Mode == 0 || Mode == 1)
            //    {
            //        p.projectile.tileCollide = true;
            //    }
            //    if(Mode == 1)
            //    {
            //        p.projectile.velocity.Y = -16f;
            //        p.projectile.velocity.X = Main.rand.Next(-5, 5);
            //    }
            //    if(Mode == 2)
            //    {
            //        p.TargetPos = Main.MouseWorld;
            //        p.projectile.tileCollide = false;
            //        p.projectile.velocity.Y = -8f;
            //        p.projectile.velocity.X = Main.rand.Next(-5, 5);
            //    }
            //}
            int i = Projectile.NewProjectile(source, position.X, position.Y, 0f, 0f, type, damage, knockback, player.whoAmI);
            var p = Main.projectile[i].ModProjectile as ThetaKnivesProj;
            p.Mode = Mode;
            p.TargetPos = Main.MouseWorld;
            return false;

            //return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddIngredient(ItemID.FragmentNebula, 2);
            recipe.AddIngredient(ItemID.FragmentVortex, 2);
            recipe.AddIngredient(ItemID.FragmentStardust, 2);
            recipe.AddIngredient(ItemID.FragmentSolar, 2);
            recipe.AddIngredient(ModContent.ItemType<ProcessingUnit>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
