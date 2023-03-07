using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ID;
using VKE.Projectiles.Ammo;

namespace VKE.Items.MaterialKnives
{
    public abstract class KnifeMaterialItem : KnifeItem
    {
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProjectiles2;
            if(type == ModContent.ProjectileType<KunaiProj>())
            {
                numProjectiles2 = NumProj + player.GetModPlayer<VampPlayer>().ExtraProj + 3;
            }
            else
                numProjectiles2 = NumProj + player.GetModPlayer<VampPlayer>().ExtraProj;
            Random random = new Random();
            int ran = random.Next(10, 35);
            float spread = MathHelper.ToRadians(ran);
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
    

