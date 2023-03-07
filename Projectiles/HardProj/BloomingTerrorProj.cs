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

namespace VKE.Projectiles.HardProj
{
    public class BloomingTerrorProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 1;
            ProjectileID.Sets.TrailCacheLength[Type] = 5;
        }
        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 1.5f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            if (Projectile.frame < 2)
            {
                return Color.Lerp(Color.ForestGreen, Color.Transparent, completionRatio) * 0.9f;
            }
            else
                return Color.Lerp(Color.HotPink, Color.Transparent, completionRatio) * 0.9f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SafeSetDefaults()
        {
            Projectile.Name = "Blooming Terror";
            Projectile.width = 10;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.frame = Main.rand.Next(0, 4);
        }
        short[] BTDusts = new short[] { DustID.PlanteraBulb, DustID.Plantera_Green, DustID.Plantera_Pink, DustID.CursedTorch };
        public override void SafeAI()
        {
            if(Main.rand.Next(0, 10) > 5)
            {
                int DustTrail = Dust.NewDust(Projectile.oldPos[1], Projectile.width - 3, Projectile.height - 3, BTDusts[Main.rand.Next(0,4)], Scale: Main.rand.NextFloat(0.6f, 1.2f));
                Dust DustObj = Main.dust[DustTrail];
                DustObj.velocity = -(Projectile.velocity / 16f);
                Main.dust[DustTrail].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, BTDusts[(j % 4)], Vel.X, Vel.Y, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            if (Main.rand.Next(0, 10) > 8)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Main.rand.NextVector2Circular(6, 6), Mod.Find<ModProjectile>("BloomingTerrorSeed").Type, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            }
            base.Kill(timeLeft);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor = new Color(230, 230, 230);
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            return false;
        }
        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if(Main.rand.Next(0,10) > 7)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Main.rand.NextVector2Circular(6,6), Mod.Find<ModProjectile>("BloomingTerrorSeed").Type, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            }
        }
    }
}