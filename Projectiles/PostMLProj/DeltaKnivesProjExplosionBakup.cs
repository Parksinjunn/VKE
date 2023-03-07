using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PostMLProj
{
    public class DeltaKnivesProjExplosionBakup : KnifeProjectile
    {
        public float OriginDmg;
        public override void SafeSetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 116;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                    
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                       
            Projectile.tileCollide = false;                
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.scale = 0.75f;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Inflate(56/2, 0);
            base.ModifyDamageHitbox(ref hitbox);
        }
        bool Hit;
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(70f * (OriginDmg / 12f));
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Hoods(n);
        }
    }
}