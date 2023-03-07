using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items
{
	public abstract class KnifeItem : ModItem
	{
        public int NumProj;
        public int KnifeSpread = 1;
        public bool KnifeSpreadDef;
        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<KnifeDamageClass>();
            NumProj = 5;
            SafeSetDefaults();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //if (IsMagic() == false)
            //{
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.Text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.Text = damageValue + " Knife Damage";
            }
            //}
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			int numProjectiles2 = NumProj + player.GetModPlayer<VampPlayer>().ExtraProj;
            if(!KnifeSpreadDef)
                KnifeSpread = Main.rand.Next(10, 35);
            float spread = MathHelper.ToRadians(KnifeSpread);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;

            for (int j = 0; j < numProjectiles2; j++)
            {
                offsetAngle = startAngle + deltaAngle * j;
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle)), type, damage, knockback, player.whoAmI);
            }
            return false;
        }
	}
}