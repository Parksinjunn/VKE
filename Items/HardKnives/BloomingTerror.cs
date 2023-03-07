using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Projectiles.HardProj;
using VKE.Projectiles.PostMLProj;

namespace VKE.Items.HardKnives
{
    public class BloomingTerror : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blooming Terror");
            Tooltip.SetDefault("It's growing out of control\nDrops damaging seeds upon hitting an enemy");
        }
        float MandibleSpeed = 9f;
        public override void SafeSetDefaults()
        {
            Item.damage = 38; 
            Item.width = 68;
            Item.height = 68;
            Item.useTime = 15;
            Item.reuseDelay = 15;
            Item.useAnimation = 15;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.crit = 15;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.channel = true;
            Item.value = Item.sellPrice(0, 12, 50, 0); 
            Item.rare = 2;  
          
 
            Item.UseSound = SoundID.Item39; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("BloomingTerrorProj").Type;
            Item.shootSpeed = 15f;
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
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Phase 1")
                {
                    OverrideColor = Color.DeepPink
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Shoots seeds that may grow into a spiked bulb upon collision")
                {
                    OverrideColor = Color.HotPink
                };
                tooltips.Add(line2);
            }
            else if (Mode == 1)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Phase 2")
                {
                    OverrideColor = Color.DarkGreen
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Plantera's soul attacks in the direction you aim")
                {
                    OverrideColor = Color.ForestGreen
                };
                tooltips.Add(line2);
            }
            else if (Mode == 2)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Mode: Phase 3")
                {
                    OverrideColor = Color.DarkOliveGreen
                };
                tooltips.Add(line);
                TooltipLine line2 = new TooltipLine(Mod, "DamageMod", "Plantera's soul is shot out to your mouse's position")
                {
                    OverrideColor = Color.GreenYellow
                };
                tooltips.Add(line2);
            }
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.Text = "[c/33FF00:Bl][c/77AA44:oo][c/BB5588:mi][c/FF00CC:ng] [c/BB5588:Te][c/77AA44:rr][c/33FF00:or]";
                }
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
                if (Mode == 1)
                {
                    Item.autoReuse = true;
                    Item.useStyle = 5;
                    Item.reuseDelay = 30;
                    Item.shootSpeed = MandibleSpeed;
                    Item.channel = false;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.Green, "Mode: Phase 2", true);
                }
                else if (Mode == 0)
                {
                    Item.autoReuse = true;
                    Item.shootSpeed = 15f;
                    Item.reuseDelay = 5;
                    Item.channel = true;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.HotPink, "Mode: Phase 1", true);
                }
                else if (Mode == 2)
                {
                    Item.autoReuse = true;
                    Item.useStyle = 5;
                    Item.reuseDelay = 30;
                    //Item.shootSpeed = MandibleSpeed;
                    Item.channel = false;
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), Color.GreenYellow, "Mode: Phase 3", true);
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

            int numProjectiles2 = 5 + player.GetModPlayer<VampPlayer>().ExtraProj;
            int ran = 20;
            float spread = MathHelper.ToRadians(ran);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;

            if (Mode == 1)
            {
                offsetAngle = startAngle + deltaAngle * 2;
                int i = Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<BloomingTerrorMandible>(), damage * 2, knockback, player.whoAmI);
                var p = Main.projectile[i].ModProjectile as BloomingTerrorMandible;
                p.Mode = Mode;
            }

            if (Mode == 0)
            {
                for (int j = 0; j < numProjectiles2; j++)
                {
                    offsetAngle = startAngle + deltaAngle * j;
                    int i = Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<BloomingTerrorHeldProj>(), damage, knockback, player.whoAmI);

                }
            }

            if (Mode == 2)
            {
                int i = Projectile.NewProjectile(source, position.X, position.Y, baseSpeed, baseSpeed, ModContent.ProjectileType<BloomingTerrorMandible>(), damage * 2, knockback, player.whoAmI);
                //Main.projectile[i].ai[0] = x;
                var p = Main.projectile[i].ModProjectile as BloomingTerrorMandible;
                p.Mode = Mode;
                p.TargetPos = Main.MouseWorld;
            }

            return false;

            //return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
