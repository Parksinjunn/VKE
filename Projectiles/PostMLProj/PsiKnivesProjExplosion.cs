using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PostMLProj
{
    public class PsiKnivesProjExplosion : KnifeProjectile
    {
        int Type;
        public float OriginDmg;
        public override void SafeSetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 132;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                    
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                       
            Projectile.tileCollide = false;                
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Main.projFrames[Projectile.type] = 14;
            Projectile.scale = 0.90f;
            Type = Main.rand.Next(0, 4);
            if (Type == 2 || Type == 3)
            {
                Projectile.frame = 8;
            }
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Inflate(74/2, 0);
            base.ModifyDamageHitbox(ref hitbox);
        }
        bool Hit;
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(128f * (OriginDmg / 12f));
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.damage = 0;
            Hit = true;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Hoods(n);
        }
        Vector2 DustPos;
        SoundStyle PsiExplosion = new SoundStyle("VKE/Sounds/Item/PsiExplosion") with { Volume = 0.7f, PitchVariance = 0.45f, MaxInstances = 3, SoundLimitBehavior = SoundLimitBehavior.IgnoreNew};
        public override void AI()
        {
            if(Projectile.frame == 0 || Projectile.frame == 8)
            {
                //int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width - 3, projectile.height - 3, 158, projectile.velocity.X * Main.rand.NextFloat(-3f, 3f), projectile.velocity.Y * Main.rand.NextFloat(-10f, 10f), 10, Color.LightBlue, 1f);
                Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, ModContent.DustType<Dusts.PsiExplosionDust>(), Scale: Main.rand.NextFloat(0.8f, 1.25f), newColor: Color.Cyan);
                dust.rotation = Main.rand.NextFloat(6.28f);
                Dusts.PsiExplosionDust.LightColor(Color.Cyan.ToVector3(), dust);
                dust.customData = Projectile;
                DustPos = Projectile.Center + Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.scale * 50;
                dust.position = DustPos;
                if (Hit && Main.rand.Next(0, 6) == 3)
                {
                    int healamnt = (int)((180f * (OriginDmg / 12f) * 0.15f));
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), DustPos.X, DustPos.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, healamnt, 0, Projectile.owner); //Creates a new Projectile
                }

                SoundEngine.PlaySound(PsiExplosion, Projectile.position);
            }
            //this is projectile dust
            if (Projectile.timeLeft <= 278)
            {
                Projectile.damage = 0;
            }
            if (Type == 1 || Type == 3)
                Projectile.spriteDirection = -1;
            else
                Projectile.spriteDirection = 1;

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Lighting.AddLight(Projectile.Center, 0.48f, 0.9f, 0.9f);
            Projectile.frameCounter++;
            if (Type == 0 || Type == 1)
            {
                if (Projectile.frameCounter >= (int)(6f))
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                    if (Projectile.frame > 6)
                    {
                        Projectile.Kill();
                    }
                }
            }
            if (Type == 2 || Type == 3)
            {
                if (Projectile.frameCounter >= (int)(6f))
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                    if (Projectile.frame > 13)
                    {
                        Projectile.Kill();
                    }
                }
            }
            
        }
    }
}