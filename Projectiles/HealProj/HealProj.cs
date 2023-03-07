using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.HealProj
{
    public class HealProj : KnifeProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = false;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 3600;
            //Main.NewText("HealInstantiated");
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.5f, 0f, 0f);

            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = Projectile.position;
                    projectilePosition -= Projectile.velocity * (i * 0.25f);
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 1, 1, DustID.VampireHeal, 0f, 0f, 0, default, 1.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(110, 150) * 0.013f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = Projectile.position;
                    projectilePosition -= Projectile.velocity * (i * 0.25f);
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 1, 1, 175, 0f, 0f, 0, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }
            Projectile.localAI[0] += 1f;

            int num498 = (int)Projectile.ai[0];
            float num499 = 26f;
            Vector2 vector31 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
            float num500 = Main.player[num498].Center.X - vector31.X;
            float num501 = Main.player[num498].Center.Y - vector31.Y;
            float num502 = (float)Math.Sqrt((double)(num500 * num500 + num501 * num501));
            if (num502 < 50f && Projectile.position.X < Main.player[num498].position.X + Main.player[num498].width && Projectile.position.X + (float)Projectile.width > Main.player[num498].position.X && Projectile.position.Y < Main.player[num498].position.Y + Main.player[num498].height && Projectile.position.Y + (float)Projectile.height > Main.player[num498].position.Y)
            {
                if (Projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech)
                {
                    int damage = Projectile.damage;
                    VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
                    p.VampCurrent += (int)(damage * 0.40);
                    if ((int)(damage * 0.40) < 1)
                        p.VampCurrent += 1f;
                    p.DelayTimer = 30;
                    float statLifeCalc = (int)((1.21015154 * Math.Log(damage) - 0.04153757));
                    statLifeCalc += ParentWeapon.LifeStealBonus;
                    statLifeCalc *= p.HealAccMult;
                    if (statLifeCalc > 10 && statLifeCalc < 20)
                        statLifeCalc = 10 + Main.rand.Next(-4, 3);
                    else if (statLifeCalc > 20)
                        statLifeCalc = 20 + Main.rand.Next(-7, 4);
                    else if (statLifeCalc < 1)
                        statLifeCalc = 1;
                    Main.player[Projectile.owner].statLife += (int)statLifeCalc;
                    if (statLifeCalc >= 1)
                        Main.player[Projectile.owner].HealEffect((int)statLifeCalc, false);

                    NetMessage.SendData(66, -1, -1, null, Main.myPlayer, statLifeCalc, 0f, 0f, 0, 0, 0);
                }
                Projectile.Kill();
            }
            num502 = num499 / num502;
            num500 *= num502;
            num501 *= num502;
            Projectile.velocity.X = (Projectile.velocity.X * 15f + num500) / 16f;
            Projectile.velocity.Y = (Projectile.velocity.Y * 15f + num501) / 16f;
        }
    }
}
