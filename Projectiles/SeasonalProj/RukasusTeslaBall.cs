using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;
using VKE.NPCs;

namespace VKE.Projectiles.SeasonalProj
{
    public class RukasusTeslaBall : KnifeProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Cell");
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 2;
        }
        int DivFactor;
        public override void SafeSetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 60;
            Main.projFrames[Projectile.type] = 5;           //this is projectile frames
        }
        public override void AI()
        {
            //this is projectile dust
            //int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width - 3, projectile.height - 3, 244, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 10, Color.LightGreen, 1.8f);
            //Main.dust[DustID2].noGravity = false;
            //this make that the projectile faces the right way 
            //Projectile.rotation += 0.3f;

            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= Main.rand.Next(4)) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 4) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
        }

        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 32; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(20, 20);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Electric, Vel.X, Vel.Y, Scale: 0.7f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        //public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        //{
        //    Player owner = Main.player[Projectile.owner];
        //    Projectile.NewProjectile(Projectile.GetSource_FromThis(), n.Center, new Vector2(0, 0), ProjectileID.StardustCellMinionShot, Projectile.damage, 8, owner.whoAmI);
        //}
        float GrowthMult = 0f;
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor = new Color(235, 235, 235);
            if(Projectile.timeLeft < 35)
            {
                Color drawColorDying = Projectile.GetAlpha(drawColor) * 0.98f;
                //Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColorDying, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            }
            //else
                //Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);

            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color GlowColor = new Color(255, 255, 255);
            Projectile.scale = GrowthMult;
            if (Projectile.timeLeft > 40)
            {
                Color color = Projectile.GetAlpha(drawColor) * 0.33f;
                GrowthMult += 0.06f;
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall").Value, drawPos, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), color, Projectile.rotation, drawOrigin, GrowthMult, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall_Glow").Value, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), GlowColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), GrowthMult, effects, 0);
            }
            else if (Projectile.timeLeft <= 40 && Projectile.timeLeft > 30)
            {
                Color color = Projectile.GetAlpha(drawColor) * 0.44f;
                GrowthMult += 0.005f;
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall").Value, drawPos, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), color, Projectile.rotation, drawOrigin, GrowthMult, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall_Glow").Value, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), GlowColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), GrowthMult, effects, 0);
            }
            else if (Projectile.timeLeft <= 30 && Projectile.timeLeft > 20)
            {
                Color color = Projectile.GetAlpha(drawColor) * 0.77f;
                GrowthMult += 0.02f;
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall").Value, drawPos, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), color, Projectile.rotation, drawOrigin, GrowthMult, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall_Glow").Value, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), GlowColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), GrowthMult, effects, 0);
            }
            else if (Projectile.timeLeft <= 20 && Projectile.timeLeft > 10)
            {
                Color color = Projectile.GetAlpha(drawColor) * 1f;
                GrowthMult += 0.025f;
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall").Value, drawPos, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), color, Projectile.rotation, drawOrigin, GrowthMult, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall_Glow").Value, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), GlowColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), GrowthMult, effects, 0);
            }
            else
            {
                Color color = Projectile.GetAlpha(drawColor) * 1f;
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall").Value, drawPos, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), color, Projectile.rotation, drawOrigin, 2f, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/RukasusTeslaBall_Glow").Value, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), GlowColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), 2f, effects, 0);
            }
            return false;
        }
    }
}