using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;
using VKE.Effects.Primitives;
using VKE.Effects;
using Terraria.ID;
using Terraria.Graphics.Shaders;

namespace VKE.Projectiles.HardProj
{
    public class NyivesBigProj : KnifeProjectile, IPixelPrimitiveDrawer
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
            float baseWidth = Projectile.scale * Projectile.width * 0.9f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            Color Rainbow = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        int Delay = 0;
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            if(!HitTile)
            {
                TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
                GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
                TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
            }
            if (HitTile && Delay <= 12)
            {
                Delay++;
                TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
                GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
                TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
            }
        }
        bool HitTile;
        bool HasDoneEffect;
        float VelocityFactor = 2f;
        int HitCount;
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(10, 10);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.RainbowTorch, Vel.X, Vel.Y, newColor: Color.Transparent, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);
            }
            base.Kill(timeLeft);
        }
        public override void SafeSetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 86;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.scale = 1f;
        }
        int SpriteRotation = 45;

        

        public override void SafeAI()
        {            
            Lighting.AddLight(Projectile.Center, (Main.DiscoR / 255f), (Main.DiscoG / 255f), (Main.DiscoB / 255f));
            if (!ZenithActive)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                if (HitTile)
                {
                    //if(Projectile.velocity.X > Projectile.velocity.Y)
                    //{
                    //    Projectile.velocity *= 0.8f;
                    //}
                    //else
                        Projectile.velocity *= 0.7f;
                    if (HasDoneEffect == false)
                    {
                        HasDoneEffect = true;
                        for (int i = 0; i < 10; i++)
                        {
                            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 1, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * -0.2f, 10, Color.Gray, 1f);
                        }
                    }
                }
                if (HitCount > 10)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Projectile.penetrate++;
            HitCount++;
        }

        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
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
            Hoods(n);
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (HitTile)
            {
                behindNPCsAndTiles.Add(index);
                return;
            }
        }
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
                HitTile = true;
                Projectile.hide = true;
                Projectile.penetrate = 1;
                Projectile.tileCollide = false;
                Projectile.velocity = oldVelocity;
                Projectile.timeLeft = 90;
            return false;
        }
    }
}