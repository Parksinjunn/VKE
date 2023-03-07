using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.Ammo
{
    public class PalladiumProj : AmmoProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = 2;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            ArmorPiercingMult = 1.1f;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            if (!n.boss)
            {
                if (Main.rand.Next(0, HealProjChanceScale) <= VampKnives.HealProjectileSpawn)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.65), 0, owner.whoAmI);
                }
            }
            else if (n.boss)
            {
                if (Main.rand.Next(0, HealProjBossChanceScale) <= HealProjBossChance)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.65), 0, owner.whoAmI);
                }
            }
            Hoods(n);
        }
    }
}