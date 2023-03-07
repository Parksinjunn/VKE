using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PreProj
{
    public class ShadowProj : KnifeProjectile
    {
        int TrailLength = 5;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = TrailLength; 
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
        }
        int ReturnTime;
        public override void SafeSetDefaults()
        {
            Projectile.Name = "Shadow Knives";
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.penetrate = 2;                      
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                       
            Projectile.tileCollide = true;                
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 140;
            CanHaveGravity = true;
            KnifeMomentum = 30f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        bool NoDust;
        public override void SafeAI()
        {
            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, DustID.PurpleCrystalShard, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.DarkViolet, 1f);
            Main.dust[DustID2].noGravity = true;

            SpriteRotFix = Projectile.direction == 1 ? 0.784f : -0.784f;
            if (CanHaveGravity)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (1.57f - SpriteRotFix);
            }
            else if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= (KnifeMomentum * (1f / (float)Projectile.timeLeft));
            }
            else if (Projectile.spriteDirection == 1)
            {
                Projectile.rotation += (KnifeMomentum * (1f / (float)Projectile.timeLeft));
            }
            if (!CanHaveGravity)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center) * 16f, 0.2f);
                if (Main.myPlayer == Projectile.owner)
                {
                    Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                    Rectangle value12 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
                    if (rectangle.Intersects(value12))
                    {
                        NoDust = true;
                        Projectile.Kill();
                    }
                }
                if(Projectile.timeLeft <= 50)
                    Projectile.timeLeft = 50;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (!NoDust)
            {
                for (int j = 0; j < 5; j++)
                {
                    Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                    int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.PurpleCrystalShard, Vel.X, Vel.Y, newColor: Color.DarkViolet, Scale: 1.5f);
                    Main.dust[dust1].noGravity = true;
                }
            }
            base.Kill(timeLeft);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.tileCollide = false;
            CanHaveGravity = false;
            return false;
        }
        int EffectTimer;
        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            EffectTimer++;
            Projectile.spriteDirection = Projectile.direction;
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor;
            if(Main.dayTime)
                drawColor = new Color(90, 90, 90);
            else
                drawColor = new Color(180, 180, 180);
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);

            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos = Projectile.oldPos[(TrailLength - 1)] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - 1)) / (float)Projectile.oldPos.Length);
            Vector2 drawOrigin2 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos2 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength - 1) / 4) * 1)))] - Main.screenPosition + drawOrigin2 + new Vector2(0f, Projectile.gfxOffY);
            Color color2 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 1)))) / (float)Projectile.oldPos.Length);
            Vector2 drawOrigin3 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos3 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength - 1) / 4) * 2)))] - Main.screenPosition + drawOrigin3 + new Vector2(0f, Projectile.gfxOffY);
            Color color3 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 2)))) / (float)Projectile.oldPos.Length);
            Vector2 drawOrigin4 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos4 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength - 1) / 4) * 3)))] - Main.screenPosition + drawOrigin4 + new Vector2(0f, Projectile.gfxOffY);
            Color color4 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 3)))) / (float)Projectile.oldPos.Length);

            if (EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 3))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/PreProj/ShadowProj").Value, drawPos, null, color * 0.7f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            if (EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 2))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/PreProj/ShadowProj").Value, drawPos2, null, color2 * 0.8f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            if (EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 1))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/PreProj/ShadowProj").Value, drawPos3, null, color3 * 0.9f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            if (EffectTimer > (TrailLength - 1))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/PreProj/ShadowProj").Value, drawPos4, null, color4, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}