using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.PreProj
{
    public class SengosForgottenProj : KnifeProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(220, 220, 220);
        }
        public override void SafeSetDefaults()
        {
            Projectile.Name = "Sengos";
            Projectile.width = 18;
            Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 45;
        }
        bool StartJumps;
        bool SelectTarget;
        bool AtTarget;
        bool GenNext;
        int NumJumps;
        int MaxJumps = 4;
        Vector2 TargetPos;
        Vector2 NextTarget;
        int Delay;
        float DustNum = 10f;
        public override void AI()
        {
            if(StartJumps)
            {
                Projectile.timeLeft = 2;
                if(!SelectTarget)
                {
                    TargetPos = Projectile.Center + (Main.rand.NextVector2Circular(-50, 50) * 4f);
                    NextTarget = TargetPos + (Main.rand.NextVector2Circular(-50, 50) * 4f);
                    SelectTarget = true;
                }
                if(GenNext)
                {
                    TargetPos = NextTarget;
                    if(NumJumps < MaxJumps - 1)
                        NextTarget = TargetPos + (Main.rand.NextVector2Circular(-50, 50) * 4f);
                    GenNext = false;
                }
                float num500 = TargetPos.X - Projectile.Center.X;
                float num501 = TargetPos.Y - Projectile.Center.Y;
                float num502 = (float)Math.Sqrt((double)(num500 * num500 + num501 * num501));

                for (int i = 0; i < DustNum; i++)
                {
                    Vector2 DustPos = Vector2.Lerp(Projectile.Center, TargetPos, i / DustNum);
                    Vector2 DustPos2 = Vector2.Lerp(TargetPos, NextTarget, i / DustNum);
                    if(i < 2)
                    {

                    }
                    else if(i <= DustNum - 2)
                    {
                        Dust LeadingDust = Dust.NewDustPerfect(DustPos, DustID.Clentaminator_Blue, newColor: Color.Blue, Scale: 0.75f);
                        LeadingDust.noGravity = true;
                        LeadingDust.velocity *= 0f;
                        if (NumJumps < MaxJumps - 1)
                        {
                            Dust LeadingDust2 = Dust.NewDustPerfect(DustPos2, DustID.Clentaminator_Blue, newColor: Color.Blue, Scale: 0.6f);
                            LeadingDust2.noGravity = true;
                            LeadingDust2.velocity *= 0f;
                        }
                    }
                    else
                    {
                        Dust LeadingDust = Dust.NewDustPerfect(DustPos, DustID.Clentaminator_Blue, newColor: Color.Blue, Scale: 1.5f);
                        LeadingDust.noGravity = true;
                        LeadingDust.velocity *= 0f;
                        if (NumJumps < MaxJumps - 1)
                        {
                            Dust LeadingDust2 = Dust.NewDustPerfect(DustPos2, DustID.Clentaminator_Blue, newColor: Color.Blue, Scale: 1.2f);
                            LeadingDust2.noGravity = true;
                            LeadingDust2.velocity *= 0f;
                        }
                    }
                }
                if (AtTarget)
                    Delay++;
                if(Delay >= 10)
                {
                    AtTarget = false;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(TargetPos) * 13f, 1f);
                    if (num502 < 20)
                    {
                        AtTarget = true;
                        GenNext = true;
                        Projectile.velocity *= 0f;
                        Delay = 0;
                        NumJumps++;
                    }
                }
                //else
                //{
                //    Projectile.rotation += 0.3f;
                //}
            }
            if (NumJumps > 4)
            {
                Projectile.Kill();
            }
            Projectile.rotation += 0.7f;
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 13; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(5, 5);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Clentaminator_Blue, Vel.X, Vel.Y, Scale: 1.3f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            StartJumps = true;
            AtTarget = true;
        }
    }
}