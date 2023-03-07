using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.ThrowableKnives
{
    public abstract class PouchWeapon : KnifeItem
    {
        public virtual void PouchDefaults()
        {
        }
        public override void SafeSetDefaults()
        {
            PouchDefaults();
            Item.useAnimation = 18;
            Item.useTime = 4;
            Item.reuseDelay = 20;
        }
        int Delay = 60;
        int DelayTick;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProjectiles2 = player.GetModPlayer<VampPlayer>().NumProj + player.GetModPlayer<VampPlayer>().ExtraProj;
            Random random = new Random();
            int ran = random.Next(10, 35);
            float spread = MathHelper.ToRadians(ran);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / (float)numProjectiles2;
            double offsetAngle;

            offsetAngle = startAngle + deltaAngle * (player.itemAnimation / 2);
            Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle)), type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}
