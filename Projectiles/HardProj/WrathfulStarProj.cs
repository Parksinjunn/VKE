using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.HardProj
{
    public class WrathfulStarProj : KnifeProjectile, IPixelPrimitiveDrawer
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
            return Color.Lerp(Color.HotPink, Color.Gold, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SafeSetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 88;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            Projectile.scale = 1f;
        }
        public override void SafeAI()
        {

            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 20 + Main.rand.Next(40);
                SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
            }
            if (!ZenithActive)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            }
            if (Main.rand.Next(16) == 0)
            {
                Vector2 value3 = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                int num66 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
                Main.dust[num66].velocity = value3 * 0.66f;
                Main.dust[num66].position = Projectile.Center + value3 * 12f;
            }
            if (Main.rand.Next(48) == 0)
            {
                int num67 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), 16, 1f);
                Gore gore = Main.gore[num67];
                gore.velocity *= 0.66f;
                gore = Main.gore[num67];
                gore.velocity += Projectile.velocity * 0.3f;
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
            Vector2 Vel = Main.rand.NextVector2Circular(2, 2);
            int dust1 = Dust.NewDust(new Vector2(n.position.X, n.position.Y), (int)n.Size.X, (int)n.Size.Y, DustID.Enchanted_Pink, Vel.X, Vel.Y, newColor: default(Color), Scale: 1.5f);
            Main.dust[dust1].noGravity = false;
            Hoods(n);
        }
        public override void Kill(int timeLeft)
        {
            for (int num540 = 0; num540 < 10; num540++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 150, default(Color), 1.2f);
            }
            SoundEngine.PlaySound(SoundID.Item10 with { Volume = 0.15f}, Projectile.position);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f), 16, 1f);
        }
    }
}