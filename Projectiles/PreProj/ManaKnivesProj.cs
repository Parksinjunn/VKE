using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;
using Terraria.Graphics.Shaders;

namespace VKE.Projectiles.PreProj
{
	public class ManaKnivesProj : KnifeProjectile, IPixelPrimitiveDrawer
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
        float SwitchSpeed;
        float Color1Fade;
        bool Color1Switch;
        float Color2Fade;
        bool Color2Switch;
        public Color ColorFunction(float completionRatio)
        {
            Color ColorFade = Color.Lerp(Color.DeepSkyBlue, Color.Lerp(Color.ForestGreen, Color.LightGoldenrodYellow, Color2Fade), Color1Fade);
            Color ColorFade2 = Color.Lerp(Color.ForestGreen, Color.LightGoldenrodYellow, Color2Fade);
            return Color.Lerp(ColorFade, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SafeSetDefaults()
        {
            Projectile.Name = "Mana Knife";
            Projectile.width = 14;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 250;
            KnifeMomentum = 11f;
            CanHaveGravity = true;
            KnifeDirection = Main.rand.NextBool();
            SwitchSpeed = Main.rand.NextFloat(0.01f, 0.05f);
        }

		public override void SafeAI()
		{
            if (Main.rand.NextBool(3))
            {
                int DustID2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, Main.rand.Next(3) switch { 0 => DustID.GreenTorch, 1 => DustID.BlueCrystalShard, _ => DustID.YellowTorch }, 0f, 0f, 10, default(Color), 1.3f);
                Main.dust[DustID2].noGravity = true;
            }
            if(!Color1Switch)
            {
                Color1Fade += SwitchSpeed;
                if (Color1Fade >= 1f)
                    Color1Switch = true;
            }
            else if(Color1Switch)
            {
                Color1Fade -= SwitchSpeed;
                if (Color1Fade <= 0f)
                    Color1Switch = false;
            }
            if (!Color2Switch)
            {
                Color2Fade += SwitchSpeed + 0.01f;
                if (Color2Fade >= 1f)
                    Color2Switch = true;
            }
            else if (Color2Switch)
            {
                Color2Fade -= SwitchSpeed + 0.01f;
                if (Color2Fade <= 0f)
                    Color2Switch = false;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(5, 5);
                int dust1 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, Main.rand.Next(3) switch { 0 => DustID.GreenTorch, 1 => DustID.BlueCrystalShard, _ => DustID.YellowTorch }, Vel.X, Vel.Y, 10, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if (!n.boss)
            {
                if (Main.rand.Next(0, HealProjChanceScale) <= VampKnives.HealProjectileSpawn)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                }
            }
            else if (n.boss)
            {
                if (Main.rand.Next(0, HealProjBossChanceScale) <= HealProjBossChance)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                }
            }
            Hoods(n);
        }
    }
}