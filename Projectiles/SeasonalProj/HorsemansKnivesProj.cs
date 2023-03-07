using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;
using System;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using VKE.Effects.Primitives;
using VKE.Effects;
using Terraria.Graphics.Shaders;
using IL.Terraria.GameContent;

namespace VKE.Projectiles.SeasonalProj
{
    public class HorsemansKnivesProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 1;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
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
            return Color.Lerp(Color.Orange, Color.Transparent, completionRatio) * 0.7f;
        }
        int Delay;
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            if (!HitTile)
            {
                TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
                GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.CorkscrewTrail);
                TrailDrawer.DrawPixelPrims(Projectile.oldPos, (Projectile.Size * 0.5f - Main.screenPosition) - (Projectile.velocity * 3f), 25);
            }
            if (HitTile && Delay <= 12)
            {
                Delay++;
                TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
                GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.CorkscrewTrail);
                TrailDrawer.DrawPixelPrims(Projectile.oldPos, (Projectile.Size * 0.5f - Main.screenPosition) - (Projectile.velocity * 3f), 25);
            }
        }
        bool HitTile;
        bool HasDoneEffect;
        bool PumpkinsSpawned;
        float VelocityFactor = 2f;
        int ActiveTargets;
        List<int> TargetIDs = new List<int>();

        public override void SafeSetDefaults()
        {
            ProjCount.PumpkinActiveCount++;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 2;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.scale = 1f;
        }
        int SpriteRotation = 90;

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (HitTile) 
            {      
                behindNPCsAndTiles.Add(index);
                return;
            }
        }
        float DustVelocityDiv = 3f;
        public override void SafeAI()
        {
            if (!ZenithActive)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(SpriteRotation); // projectile faces sprite right
                if (HitTile)
                {
                    Projectile.velocity = Projectile.velocity * 0.65f;
                    if (HasDoneEffect == false)
                    {
                        HasDoneEffect = true;
                        for (int i = 0; i < 10; i++)
                        {
                            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 1, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * -0.2f, 10, Color.Gray, 1f);
                        }
                    }
                }
                else
                {
                    int dust1 = Dust.NewDust(new Vector2(Projectile.Center.X - (Projectile.velocity.X * DustVelocityDiv), Projectile.Center.Y - (Projectile.velocity.Y * DustVelocityDiv)), 1, 1, DustID.SolarFlare, 0*Projectile.velocity.X / DustVelocityDiv, 0*Projectile.velocity.Y / DustVelocityDiv, newColor: Color.Orange, Scale: 1.1f);
                    Main.dust[dust1].noGravity = true;
                }
            }

            if (Main.rand.Next(1, (int)(200 * (float)Math.Log(10 * ProjCount.GetPumpkinActiveCount()))) == 90)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].CanBeChasedBy())
                    {
                        ActiveTargets++;
                        TargetIDs.Add(i);
                        //Main.NewText("ID: " + i);
                    }
                }
                //Main.NewText("ActiveTargets: " + ActiveTargets);
                //Main.NewText("TargetIDs Stored: " + TargetIDs.Count);
                for (int i = 0; i < TargetIDs.Count; i++)
                {
                    //Main.NewText("FinalID: " + TargetIDs[i]);
                    if (!Main.npc[TargetIDs[i]].active)
                    {
                        TargetIDs.RemoveAt(i);
                        i++;
                    }
                    if (Main.rand.Next(1, ActiveTargets) <= ActiveTargets / 2)
                    {
                        int logicCheckScreenHeight = Main.LogicCheckScreenHeight;
                        int logicCheckScreenWidth = Main.LogicCheckScreenWidth;
                        int num = Main.rand.Next(100, 300);
                        int num2 = Main.rand.Next(100, 300);
                        num = ((Main.rand.Next(2) != 0) ? (num + (logicCheckScreenWidth / 2 - num)) : (num - (logicCheckScreenWidth / 2 + num)));
                        num2 = ((Main.rand.Next(2) != 0) ? (num2 + (logicCheckScreenHeight / 2 - num2)) : (num2 - (logicCheckScreenHeight / 2 + num2)));
                        num += (int)Projectile.position.X;
                        num2 += (int)Projectile.position.Y;
                        float num3 = 40f;
                        Vector2 vector = new Vector2((float)num, (float)num2);
                        float num4 = Main.npc[TargetIDs[i]].position.X - vector.X;
                        float num5 = Main.npc[TargetIDs[i]].position.Y - vector.Y;
                        float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                        num6 = num3 / num6;
                        num4 *= num6;
                        num5 *= num6;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), (float)num, (float)num2, num4, num5, ProjectileID.FlamingJack, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, (float)TargetIDs[i], 0f);
                    }
                }
                ActiveTargets = 0;
                TargetIDs.Clear();
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.OrangeTorch, Vel.X, Vel.Y, newColor: Color.Orange, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        public override bool SafePreKill(int timeLeft)
        {
            ProjCount.PumpkinActiveCount--;
            return base.SafePreKill(timeLeft);
        }
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft < 298)
            {
                HitTile = true;
                Projectile.hide = true;
                Projectile.penetrate = 1;
                Projectile.tileCollide = false;
                Projectile.velocity = oldVelocity;
                Projectile.timeLeft = 60;
            }
            else
                Projectile.velocity = oldVelocity;
            return false;
        }
    }
}