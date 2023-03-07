using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.Ammo
{
    public class KunaiProj : AmmoProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 2;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            ArmorPiercingMult = 1.1f;
        }
        public float rotate = 20;
        public override void AI()
        {
            rotate -= 0.008f;
            Projectile.rotation += rotate;
            Projectile.localAI[0] += 1f;
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
        }
    }
}