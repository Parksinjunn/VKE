using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;
using static Humanizer.In;

namespace VKE.Projectiles.HardProj
{
	public class HallowedGauntletProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        int PrimColor;
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Gauntlet");
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
            Color Rainbow;
            if (PrimColor == 0)
            {
                Rainbow = Color.LightCyan;
            }
            else if (PrimColor == 1)
            {
                Rainbow = Color.RoyalBlue;
            }
            else if (PrimColor == 2)
            {
                Rainbow = Color.SkyBlue;
            }
            else if (PrimColor == 3)
            {
                Rainbow = Color.Pink;
            }
            else if (PrimColor == 4)
            {
                Rainbow = Color.LightPink;
            }
            else if (PrimColor == 5)
            {
                Rainbow = Color.Violet;
            }
            else if (PrimColor == 6)
            {
                Rainbow = Color.LightSteelBlue;
            }
            else
            {
                Rainbow = Color.BlueViolet;
            }
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.WhispyTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        float VelocityFactor = 1f;
        bool HitTile;
        bool HealedSpawned=false;
        int EffectTimer;

		public override void SafeSetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.penetrate = 3;                       //this is the projectile penetration
            Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
			Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.scale = 0.7f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
            PrimColor = Main.rand.Next(0, 8);
        }

		public override void AI()
		{
            Player owner = Main.player[Projectile.owner];
            //this is projectile dust
            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width-3, Projectile.height-3, 15, 0f, 0f, 10, Color.White, 1f);
            Main.dust[DustID2].noGravity = true;

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            if(HitTile)
            {
                    for (int iteration = 0; iteration < 360; iteration++)
                    {
                        int DustID3 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 15, 0f, 0f, 10, Color.White, 2f);
                        Main.dust[DustID3].noGravity = true;
                        Main.dust[DustID3].velocity = new Vector2(VelocityFactor * (float)Math.Cos(iteration / VelocityFactor), VelocityFactor * (float)Math.Sin(iteration / VelocityFactor));
                    }
                    HitTile = false;
                EffectTimer++;
            }  
        }
        bool HealSpawned;
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if (HealSpawned == false)
            {
                if (!n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjChanceScale * 1.5f)) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HallowedStealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjBossChanceScale * 1.5f)) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HallowedStealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
                HealSpawned = true;
            }
            Hoods(n);
        }
        public override bool SafePreKill(int timeLeft)
        {
            if (Main.rand.Next(1, 10) == 6)
            {
                Player owner = Main.player[Projectile.owner];
                for (int NumPieces = 0; NumPieces < Main.rand.Next(1, 3); NumPieces++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-1.5f, 1.5f)), ModContent.ProjectileType<HallowedGauntletShatteredProj>(), 3 + Main.rand.Next(0, 5), 0f, owner.whoAmI);
                }
            }
            else
            {
                for (int iteration = 0; iteration < 3; iteration++)
                {
                    int Angle = Main.rand.Next(1, 361);
                    int DustVelocity = 5;
                    int DustID3 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 15, 0f, 0f, 10, Color.White, 2f);
                    Main.dust[DustID3].noGravity = true;
                    Main.dust[DustID3].velocity = new Vector2(DustVelocity * (float)Math.Cos(Angle / DustVelocity), DustVelocity * (float)Math.Sin(Angle / DustVelocity));
                }
            }
            return base.SafePreKill(timeLeft);
        }
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            if(Main.rand.Next(1,6) == 3)
            {
                HitTile = true;
                Projectile.penetrate = -1;
                Projectile.tileCollide = false;
                Projectile.velocity = oldVelocity;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}