using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace VKE.Projectiles.SeasonalProj
{
    public class HarvestersSoulProj2 : KnifeProjectile
    {
        int TrailLength = 17;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = TrailLength; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SafeSetDefaults()
        {
            Projectile.Name = "HarvestersSoulLargeProj";
            Projectile.width = 92;
            Projectile.height = 90;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 130;
        }

        public override void AI()
        {
            //this is projectile dust
            //int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width - 3, projectile.height - 3, 158, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 10, Color.LightBlue, 1f);
            //Main.dust[DustID2].noGravity = true;
            Projectile.rotation += Main.rand.NextFloat(0.1f, 0.4f);
            Projectile.localAI[0] += 1f;
            Lighting.AddLight(Projectile.position, 0.69f, 0.149f, 1f);
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 40; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(20, 20);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.PurpleTorch, Vel.X, Vel.Y, Scale: 3f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        int EffectTimer;
        public override bool PreDraw(ref Color lightColor)
        {
            EffectTimer++;
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor = new Color(255, 255, 255);
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);

            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos = Projectile.oldPos[(TrailLength -1)] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - 1)) / (float)Projectile.oldPos.Length);
            Vector2 drawOrigin2 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos2 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength-1)/4) * 1)))] - Main.screenPosition + drawOrigin2 + new Vector2(0f, Projectile.gfxOffY);
            Color color2 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 1)))) / (float)Projectile.oldPos.Length);
            Vector2 drawOrigin3 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos3 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength - 1) / 4) * 2)))] - Main.screenPosition + drawOrigin3 + new Vector2(0f, Projectile.gfxOffY);
            Color color3 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 2)))) / (float)Projectile.oldPos.Length);
            Vector2 drawOrigin4 = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos4 = Projectile.oldPos[(TrailLength - (1 + (((TrailLength - 1) / 4) * 3)))] - Main.screenPosition + drawOrigin4 + new Vector2(0f, Projectile.gfxOffY);
            Color color4 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - (TrailLength - (1 + (((TrailLength - 1) / 4) * 3)))) / (float)Projectile.oldPos.Length);

            if (EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 3))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/HarvestersSoulProj2").Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            if(EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 2))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/HarvestersSoulProj2").Value, drawPos2, null, color2, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            if(EffectTimer > (TrailLength - (1 + (((TrailLength - 1) / 4) * 1))))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/HarvestersSoulProj2").Value, drawPos3, null, color3, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            if(EffectTimer > (TrailLength - 1))
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/HarvestersSoulProj2").Value, drawPos4, null, color4, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}