using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.DataStructures;
using Terraria.Localization;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items;
using VKE.Buffs;
using VKE.Buffs.HoodBuffs;

namespace VKE.Projectiles
{
    public abstract class KnifeProjectile : ModProjectile
    {
        public float KnifeMomentum;
        public bool KnifeDirection;
        public bool CanHaveGravity;
        public int ShatterTarget;
        bool PenetrationBuffApplied = false;
        public static bool ZenithActive = false;
        public static int ZenithProjID;
        public static bool ProjectileNotZenith = false;
        float RandomVelocity;
        bool Instanced;
        bool TileCollision;
        float RotationSpeed;
        public static int HealProjChanceScale = 101;
        public static int HealProjBossChanceScale = 101;
        public static int HealProjBossChance = 25;
        public static int HealProjChance = HealProjChanceScale;
        public KnifeWeapon ParentWeapon = new KnifeWeapon();

        public virtual void SafeSetDefaults() {
        }
        public virtual void SafeAI() {
        }
        public virtual bool SafePreKill(int timeleft) {
            return true;
        }
        public virtual bool SafeOnTileCollide(Vector2 OldVelocity){
            return true;
        }
        public virtual void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit) {
        }
        public virtual void SafeModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
        }
        public override void SetDefaults()
        {
            SafeSetDefaults();
            ModContent.GetInstance<KnifeDamageClass>();
            if (ZenithActive)
            {
                RandomVelocity = 1f;
                Projectile.tileCollide = false;
                Projectile.aiStyle = 0;
                Projectile.penetrate = 1;
                Projectile.timeLeft = 50;
                Projectile.scale = 1.3f;
                HealProjChance = 3;
            }
            else
            {
                HealProjChance = HealProjChanceScale;
            }
        }
        public override bool PreAI()
        {
            if (ModContent.GetModItem(Main.LocalPlayer.HeldItem.type) is Items.KnifeItem)
            {
                ParentWeapon = Main.LocalPlayer.HeldItem.GetGlobalItem<KnifeWeapon>();
            }
            return true;
        }
        int AlphaDelay;
        public float SpriteRotFix = 0f;
        public override void AI()
        {
            if(SpriteRotFix != 0.784f && SpriteRotFix != -0.784f)
            {
                if (Projectile.timeLeft > 180 || KnifeMomentum == 0)
                    Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (1.57f - SpriteRotFix);
                else if (KnifeDirection)
                    Projectile.rotation += (KnifeMomentum * (1f / (float)Projectile.timeLeft));
                else
                {
                    Projectile.rotation -= (KnifeMomentum * (1f / (float)Projectile.timeLeft));
                }
            }
            SafeAI();
            if (CanHaveGravity)
            {
                if(Projectile.timeLeft < 135)
                {
                    Projectile.velocity.X *= 0.998f;
                    Projectile.velocity.Y += 0.5f;
                }
            }
        }
        public override bool PreKill(int timeLeft)
        {
            SafePreKill(timeLeft);
            return base.PreKill(timeLeft);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            SafeModifyHitNPC(target,ref damage,ref knockback,ref crit,ref hitDirection);
            //if (Projectile.penetrate != -1 && PenetrationBuffApplied == false && Mod.Find<ModItem>("" + ParentWeapon.GetType()).Type != ModContent.ItemType<CorruptionNestKnives>() && Mod.Find<ModItem>("" + ParentWeapon.GetType()).Type != ModContent.ItemType<CrimsonNestKnives>() && Mod.Find<ModItem>("" + ParentWeapon.GetType()).Type != ModContent.ItemType<PrismaticArcanum>())
            //{
            //    Projectile.penetrate += ParentWeapon.PenetrationBonus;
            //    PenetrationBuffApplied = true;
            //}
        }
        public void Hoods(NPC n)
        {
            VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
            int effectLength = Main.rand.Next(300, 600);
            if (p.pyro == true && p.HoodKeyPressed == false)
            {
                n.AddBuff(BuffID.OnFire, effectLength);
            }
            if (p.pyro == true && p.HoodKeyPressed == true)
            {
                n.AddBuff(ModContent.BuffType<Hellfire>(), effectLength);
            }
            if (p.dPyro == true && p.HoodKeyPressed == false)
            {
                n.AddBuff(BuffID.CursedInferno, effectLength);
            }
            if (p.dPyro == true && p.HoodKeyPressed == true)
            {
                n.AddBuff(ModContent.BuffType<CursedFire>(), effectLength);
            }
            if (p.Transmuter == true && p.HoodKeyPressed == false)
            {
                n.AddBuff(BuffID.Midas, effectLength);
            }
            if (p.Transmuter == true && p.HoodKeyPressed == true)
            {
                n.AddBuff(ModContent.BuffType<GildedCurse>(), 60);
            }
            if (p.Invoker == true && p.HoodKeyPressed == false)
            {
                n.AddBuff(BuffID.Ichor, effectLength);
            }
            if (p.Invoker == true && p.HoodKeyPressed == true)
            {
                n.AddBuff(ModContent.BuffType<IchorUproar>(), effectLength);
            }
            if (p.Mage == true && p.HoodKeyPressed == false)
            {
                int healamnt = (int)(Projectile.damage * 0.12f);
                if (!n.boss)
                {
                    if (Main.rand.Next(0, HealProjChanceScale) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, healamnt, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, HealProjBossChanceScale) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, healamnt, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
                    }
                }
            }
            if (p.Technomancer == true && p.HoodKeyPressed == false)
            {
                n.AddBuff(BuffID.Confused, effectLength);
            }
            if (p.Technomancer == true && p.HoodKeyPressed == true)
            {
                Random random = new Random();
                int ran1 = random.Next(-10, 10);
                int ran2 = random.Next(1, 6);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), n.position.X, n.position.Y, ran1, ran2, Mod.Find<ModProjectile>("NaniteProjectile").Type, 15, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            }
            if (p.Party == true && p.HoodKeyPressed == false)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), n.position.X, n.position.Y, 0, 0, 289, 0, 8, Projectile.owner); //Creates a new Projectile
            }
            if (p.Party == true && p.HoodKeyPressed == true)
            {
                n.AddBuff(ModContent.BuffType<PartyBuff>(), effectLength);
                //Projectile.NewProjectile(n.position.X, n.position.Y, 0, 0, 289, 0, 8, projectile.owner); //Creates a new Projectile
            }
            if (p.Shaman == true && p.HoodKeyPressed == false)
            {
                n.AddBuff(BuffID.Poisoned, effectLength);
            }
            if (p.Shaman == true && p.HoodKeyPressed == true)
            {
                n.AddBuff(ModContent.BuffType<PenetratingPoison>(), effectLength);
            }
            if (p.WitchDoctor == true && p.HoodKeyPressed == false)
            {
                n.AddBuff(BuffID.Venom, effectLength);
            }
        }
        public int NumRicochets;
        public void Ricochet(Vector2 OldVelocity)
        {
            if (Projectile.velocity.X != OldVelocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = 0f - OldVelocity.X;
            }
            if (Projectile.velocity.Y != OldVelocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = 0f - OldVelocity.Y;
            }
            NumRicochets++;
            if(NumRicochets > 5)
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            if (!n.boss)
            {
                if (Main.rand.Next(0, HealProjChanceScale) <= VampKnives.HealProjectileSpawn)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, owner.whoAmI);
                }
            }
            else if (n.boss)
            {
                if (Main.rand.Next(0, HealProjBossChanceScale) <= HealProjBossChance)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, owner.whoAmI);
                }
            }
            SafeOnHitNPC(n, damage, knockback, crit);
            Hoods(n);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (ParentWeapon.RicochetChance > Main.rand.NextFloat(0f, 1.0f))
            {
                Ricochet(oldVelocity);
            }
            SafeOnTileCollide(oldVelocity);
            if(SafeOnTileCollide(oldVelocity) == false)
            {
                TileCollision = false;
                return TileCollision;
            }
            else
            {
                return true;
            }
        }
    }
}