using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.HardProj
{
    public class ExcaliburProj : KnifeProjectile
    {
        int TrailLength = 6;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = TrailLength; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SafeSetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
        }
        int SpriteRotation = 45;
        public override void SafeAI()
        {
            if (!ZenithActive)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(6, 6);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Enchanted_Gold, Vel.X, Vel.Y, newColor: Color.Gold, Scale: 1.5f);
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
            Vector2 drawPos = Projectile.oldPos[0] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - 4) / (float)Projectile.oldPos.Length);

            Color color2 = Projectile.GetAlpha(drawColor) * ((float)(Projectile.oldPos.Length - 5) / (float)Projectile.oldPos.Length);

            if (EffectTimer >= 1 && EffectTimer < 5)
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/ExcaliburProj").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.3f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= 5 && EffectTimer < 10)
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/ExcaliburProj").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.3f, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/ExcaliburProj").Value, drawPos, null, color2, Projectile.rotation, drawOrigin, 1.5f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= 10 && EffectTimer < 15)
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/ExcaliburProj").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.3f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= 15)
            {
                EffectTimer = -5;
            }
            return false;
        }
    }
}