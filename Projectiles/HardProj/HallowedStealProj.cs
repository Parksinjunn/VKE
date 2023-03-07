using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.HardProj
{
    public class HallowedStealProj : KnifeProjectile
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
        }
        //public override Color? GetAlpha(Color lightColor)
        //{
        //    //return Color.White;
        //    lightColor = new Color(255, 0, 0);
        //    return lightColor;
        //}
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Main.DiscoR/255f, Main.DiscoG/255f, Main.DiscoB/255f);

            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = Projectile.position;
                    projectilePosition -= Projectile.velocity * ((float)i * 0.25f);
                    Projectile.alpha = 255;
                    // Important, changed 173 to 178!
                    int dust = Dust.NewDust(projectilePosition, 1, 1, 182, 0f, 0f, 0, new Color(Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f), 1.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = (float)Main.rand.Next(110, 150) * 0.013f;
                    Main.dust[dust].velocity *= 0.2f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
                }
            }
            Projectile.localAI[0] += 1f;

            for (int i = 0; i < 200; i++)
            {
                Player owner = Main.player[Projectile.owner];
                float shootToX = owner.position.X + (float)owner.width * 0.5f - Projectile.Center.X;
                float shootToY = owner.position.Y + (owner.height / 2) - Projectile.Center.Y;
                float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                if (distance < 2000f && owner.active)
                {
                    distance = 4f / distance;

                    shootToX *= distance * 5;
                    shootToY *= distance * 5;

                    Projectile.velocity.X = shootToX;
                    Projectile.velocity.Y = shootToY;
                }
                float distanceX = owner.position.X - Projectile.position.X;
                float distanceY = owner.position.Y - Projectile.position.Y;
                if (distance < 70f && Projectile.position.X < owner.position.X + owner.width && Projectile.position.X + Projectile.width > owner.position.X && Projectile.position.Y < owner.position.Y + owner.height && Projectile.position.Y + Projectile.height > owner.position.Y)
                {
                    int damage = Projectile.damage;
                    VampPlayer p = Main.LocalPlayer.GetModPlayer<VampPlayer>();
                    p.VampCurrent += (int)(damage * 0.40);
                    if (((int)(damage * 0.40)) < 1)
                        p.VampCurrent += 1f;
                    p.DelayTimer = 30;
                    int statLifeCalc = (int)((p.HealAccMult) * ((p.VampCurrent / 230) + (1.21015154 * Math.Log(damage) - 0.04153757)));
                    if (statLifeCalc > 20)
                        statLifeCalc = 20 + Main.rand.Next(-3, 3) + (int)p.VampCurrent / 330;
                    if (statLifeCalc < 1)
                        statLifeCalc = 1;
                    statLifeCalc += ParentWeapon.LifeStealBonus;
                    bool ManaOrHealth = Main.rand.NextBool();
                    if (ManaOrHealth)
                    {
                        owner.statLife += (statLifeCalc);
                        if (statLifeCalc >= 1)
                            owner.HealEffect(statLifeCalc, false);
                        Projectile.Kill();
                    }
                    else if (ManaOrHealth == false)
                    {
                        owner.statMana += statLifeCalc;
                        owner.ManaEffect(statLifeCalc);
                        Projectile.Kill();
                    }
                    break;
                }
            }
        }
    }
}
