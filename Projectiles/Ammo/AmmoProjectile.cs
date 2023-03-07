using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.Ammo
{
    public abstract class AmmoProjectile : KnifeProjectile
    {
        public float ArmorPiercingMult = 1f;
        public override void SafeSetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = 3;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
        }
        public override void SafeAI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            //Main.NewText("Defense 1: " + n.defense);
            Hoods(n);
            n.defense = (int)(n.defense / ((1.05f * ArmorPiercingMult) * VampKnives.AmmoDefenseDecrease));
            //Main.NewText("Defense 2: " + n.defense);
        }
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Tink.WithVolumeScale(0.5f), Projectile.position);
            int DustType = DustID.Iron;
            if (Projectile.type == ModContent.ProjectileType<AdamantiteProj>())
                DustType = DustID.Adamantite;
            else if (Projectile.type == ModContent.ProjectileType<ChlorophyteProj>())
                DustType = DustID.Chlorophyte;
            else if (Projectile.type == ModContent.ProjectileType<CopperProj>())
                DustType = DustID.Copper;
            else if (Projectile.type == ModContent.ProjectileType<CrimtaneProj>())
                DustType = DustID.Crimstone;
            else if (Projectile.type == ModContent.ProjectileType<DemoniteProj>())
                DustType = DustID.Demonite;
            else if (Projectile.type == ModContent.ProjectileType<GoldProj>())
                DustType = DustID.Gold;
            else if (Projectile.type == ModContent.ProjectileType<IronProj>())
                DustType = DustID.Iron;
            else if (Projectile.type == ModContent.ProjectileType<KunaiProj>())
                DustType = DustID.Cobalt;
            else if (Projectile.type == ModContent.ProjectileType<MoltenProj>())
                DustType = DustID.Meteorite;
            else if (Projectile.type == ModContent.ProjectileType<MythrilProj>())
                DustType = DustID.Mythril;
            else if (Projectile.type == ModContent.ProjectileType<OrichalcumProj>())
                DustType = DustID.Orichalcum;
            else if (Projectile.type == ModContent.ProjectileType<PalladiumProj>())
                DustType = DustID.Palladium;
            else if (Projectile.type == ModContent.ProjectileType<PlatinumProj>())
                DustType = DustID.Platinum;
            else if (Projectile.type == ModContent.ProjectileType<SilverProj>())
                DustType = DustID.Silver;
            else if (Projectile.type == ModContent.ProjectileType<TinProj>())
                DustType = DustID.Tin;
            else if (Projectile.type == ModContent.ProjectileType<TitaniumProj>())
                DustType = DustID.Titanium;
            else if (Projectile.type == ModContent.ProjectileType<ToothProj>())
                DustType = DustID.Bone;
            else if (Projectile.type == ModContent.ProjectileType<TungstenProj>())
                DustType = DustID.Tungsten;

            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, DustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, default(Color), 1f);
            return true;
        }

    }
}