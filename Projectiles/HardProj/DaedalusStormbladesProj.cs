using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Steamworks;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.HardProj
{
    public class DaedalusStormbladesProj : KnifeProjectile, IPixelPrimitiveDrawer
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
            Color Rainbow = Color.DeepSkyBlue;
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.SpikyTrail1);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SafeSetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Lighting.AddLight(Projectile.Center, 0.05f, 0.1f, 0.2f);
            Projectile.alpha = 65;
            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 15, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.White, 1f);
            Main.dust[DustID2].noGravity = true;

            if (Projectile.ai[1] == 0f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.ai[1] = 1f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[1] != 0f)
            {
                Projectile.tileCollide = true;
            }
        }
        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(1, 10) == 4)
            {
                SoundEngine.PlaySound(SoundID.Item10 with { Volume = 0.05f }, Projectile.position);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(n.Center.X, n.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<DaedalusStormblades_Lightning>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 2);
            }
        }
        public override void Kill(int timeLeft)
        {
            //if(DefenseKnivesProj.ProjCount.GetLightningActiveCount() <= 1)
            //{
            //    if (Main.raining)
            //    {
            //        if (Main.rand.Next(1, 101) >= 95)
            //        {
            //            Player owner = Main.player[Projectile.owner];
            //            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y - 390, 0, 20f, Mod.Find<ModProjectile>("LightningProj").Type, (int)(Projectile.damage * 0.55), 0, owner.whoAmI);
            //            DefenseKnivesProj.ProjCount.LightningActiveCount += 1;
            //        }
            //    }
            //    else
            //    {
            //        if (Main.rand.Next(1, 101) == 50)
            //        {
            //            Player owner = Main.player[Projectile.owner];
            //            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y - 390, 0, 20f, Mod.Find<ModProjectile>("LightningProj").Type, (int)(Projectile.damage * 0.35), 0, owner.whoAmI);
            //            DefenseKnivesProj.ProjCount.LightningActiveCount += 1;
            //        }
            //    }
            //}
        }
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            //Main.PlaySound(SoundID.Tink, (int)projectile.position.X, (int)projectile.position.Y, 1, 0.5f);
            if (Main.rand.Next(1, 10) == 4)
            {
                SoundEngine.PlaySound(SoundID.Item10 with { Volume = 0.05f }, Projectile.position);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(0f, 0f), ModContent.ProjectileType<DaedalusStormblades_Lightning>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 1);
            }
            for (int x = 0; x < 7; x++)
            {
                int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 15, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.White, 0.75f);
                Main.dust[DustID2].noGravity = true;
            }
            return true;
        }
    }
}