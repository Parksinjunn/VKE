using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using VKE.Projectiles.PostMLProj;
using VKE.Tiles;
using VKE.Items.Materials;
using System.Linq;

namespace VKE.Items.PostMLKnives
{
	public class DeltaKnives : KnifeItem
	{
        protected override bool CloneNewInstances => true;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Delta");
			//Tooltip.SetDefault("Knives with an internal compartment \nfilled with bees that explode out on impact");
        }
		public override void SafeSetDefaults()
		{
			Item.damage = 74;            
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 15;
			Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0,0,54,20);
            SoundStyle DeltaLaunch = new SoundStyle("VKE/Sounds/Item/DeltaLaunch") with { Volume = 0.5f, PitchVariance = 0.35f };
            Item.UseSound = DeltaLaunch;
			Item.rare = 8;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("DeltaKnivesProj").Type;
            Item.shootSpeed = 9f;
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
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Fan")
                {
                    OverrideColor = Color.Green
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Shoots delta missiles in a fan-pattern")
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
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Shoots delta missiles that bomb the area surrounding the player")
                {
                    OverrideColor = Color.PaleVioletRed
                };
                tooltips.Add(line2);
            }
            else if (Mode == 2)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Target")
                {
                    OverrideColor = Color.Cyan
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Shoots delta missiles that home-in on where you aim")
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
                    Item.useStyle = 5;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Green, "Mode: Fan", true);
                }
                else if (Mode == 1)
                {
                    Item.autoReuse = true;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Red, "Mode: Bombing", true);
                }
                else if (Mode == 2)
                {
                    Item.autoReuse = true;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Cyan, "Mode: Target", true);
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
                var p = Main.projectile[i].ModProjectile as DeltaKnivesProj;
                p.Mode = Mode;
                if(Mode == 0 || Mode == 1)
                {
                    p.Projectile.tileCollide = true;
                }
                if(Mode == 1)
                {
                    p.Projectile.velocity.Y = -16f;
                    p.Projectile.velocity.X = Main.rand.Next(-5, 5);
                }
                if(Mode == 2)
                {
                    p.TargetPos = Main.MouseWorld;
                    p.Projectile.tileCollide = false;
                    p.Projectile.velocity.Y = -8f;
                    p.Projectile.velocity.X = Main.rand.Next(-5, 5);
                }
            }
            return false;

            //return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Celeb2);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.FragmentNebula, 16);
            recipe.AddIngredient(ModContent.ItemType<ProcessingUnit>());
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

}
