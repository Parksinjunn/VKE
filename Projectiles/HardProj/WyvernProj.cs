using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;
using VKE.Items;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace VKE.Projectiles.HardProj
{
    public class WyvernProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 1;
            ProjectileID.Sets.TrailCacheLength[Type] = 14;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 1.3f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            Color Rainbow = Color.LightSkyBlue;
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.DNATrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public bool HitNewTarget;
        public int delay = 40;
        public int NumHits;
        public override void SafeSetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 320;
        }
        public override void AI()
        {
            //for (int g = 0; g < 160 / Projectile.timeLeft; g++)
            //    {
            //        Vector2 position = Main.LocalPlayer.Center;
            //        Dust dust = Main.dust[Terraria.Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 45, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 0, new Color(255, 255, 255), 0.8f)];
            //        dust.noGravity = false;
            //    }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            if (Projectile.timeLeft < 60)
                Projectile.Opacity -= 0.01f;
            for (int NPCDist = 0; NPCDist < 200; NPCDist++)
            {
                if (!Main.npc[NPCDist].friendly)
                {
                    Rectangle rectangle4 = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                    Rectangle value11 = new Rectangle((int)Main.npc[NPCDist].position.X, (int)Main.npc[NPCDist].position.Y, Main.npc[NPCDist].width, Main.npc[NPCDist].height);
                    if (rectangle4.Intersects(value11))
                    {
                        HitNewTarget = true;
                        delay = 0;
                    }
                }
            }
            if (HitNewTarget)
            {
                Projectile.tileCollide = false;
                if (delay <= 30)
                {
                    delay++;
                    Projectile.velocity *= 0.95f;
                }
                else if (delay > 30)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC target = Main.npc[i];
                        if (!target.friendly)
                        {
                            float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                            float shootToY = target.position.Y - Projectile.Center.Y;
                            float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                            if (distance < 2000f && !target.friendly && target.active)
                            {
                                distance = 3f / distance;

                                shootToX *= distance * 10;
                                shootToY *= distance * 10;

                                Projectile.velocity.X = shootToX;
                                Projectile.velocity.Y = shootToY;
                            }
                        }
                    }
                }
            }
        }
        int HitCount;
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            HitCount++;
            if (HitCount == 1)
            {
                if (!n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjChanceScale * 1.5f)) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjBossChanceScale * 1.5f)) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
            }
            Projectile.damage = Projectile.damage / HitCount;
            Vector2 Vel = Main.rand.NextVector2Circular(2, 2);
            int dust1 = Dust.NewDust(new Vector2(n.position.X, n.position.Y), (int)n.Size.X, (int)n.Size.Y, DustID.Cloud, Vel.X, Vel.Y, newColor: default(Color), Scale: 1.5f);
            Main.dust[dust1].noGravity = false;
            if (Projectile.timeLeft > 30)
            {
                Projectile.idStaticNPCHitCooldown = 6;
            }
            else if (Projectile.timeLeft > 11)
            {
                Projectile.idStaticNPCHitCooldown = 3;
            }
            else
            {
                Projectile.idStaticNPCHitCooldown = 1;
            }
            Hoods(n);
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(5, 5);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Cloud, Vel.X, Vel.Y, newColor: Color.White, Scale: 1.2f);
                Main.dust[dust1].noGravity = false;
            }
            base.Kill(timeLeft);
        }
        //public override bool OnTileCollide(Vector2 oldVelocity)
        //{
        //    Main.NewText("Ricochet: " + ParentWeapon.RicochetChance);
        //    if (ParentWeapon.RicochetChance > 0.5f)
        //    {
        //        Ricochet(oldVelocity);
        //    }
        //    else
        //    {
        //        projectile.Kill();
        //    }
        //    return false;
        //}
    }
}