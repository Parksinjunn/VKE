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

namespace VKE.Projectiles.HardProj
{
    public class StardustProjCell : KnifeProjectile
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
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 60;
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
        }
        public override void AI()
        {
            //this is projectile dust
            //int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width - 3, projectile.height - 3, 244, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 10, Color.LightGreen, 1.8f);
            //Main.dust[DustID2].noGravity = false;
            //this make that the projectile faces the right way 
            Projectile.rotation += 0.5f;
            Projectile.velocity *= 0.999f;

            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 3) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 3) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
        }

        public override void Kill(int timeLeft)
        {
            if(DivFactor <= 0)
            {
                for(int m = 0; m < 3; m++)
                {
                    int i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(8f, 8f), ModContent.ProjectileType<StardustProjCell>(), Projectile.damage / 2, 4, Projectile.owner);
                    var p = Main.projectile[i].ModProjectile as StardustProjCell;
                    p.DivFactor = 1;
                    p.Projectile.scale = 0.8f;
                }
            }
            else if(DivFactor == 1)
            {
                for (int m = 0; m < 2; m++)
                {
                    int i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(8f, 8f), ModContent.ProjectileType<StardustProjCell>(), Projectile.damage / 2, 2, Projectile.owner);
                    var p = Main.projectile[i].ModProjectile as StardustProjCell;
                    p.DivFactor = 2;
                    p.Projectile.scale = 0.6f;
                }
            }
            else
            {
                for (int j = 0; j < 5; j++)
                {
                    Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                    int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.IceRod, Vel.X, Vel.Y, Scale: 1.5f);
                    Main.dust[dust1].noGravity = true;
                }
            }
            base.Kill(timeLeft);
        }
        //public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        //{
        //    Player owner = Main.player[Projectile.owner];
        //    Projectile.NewProjectile(Projectile.GetSource_FromThis(), n.Center, new Vector2(0, 0), ProjectileID.StardustCellMinionShot, Projectile.damage, 8, owner.whoAmI);
        //}

        int EffectTimer;
        int EffectTimerMult = 3;
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
            if(Projectile.timeLeft < 10)
            {
                Color drawColorDying = Projectile.GetAlpha(drawColor) * 0.4f;
                Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColorDying, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            }
            else
                Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);

            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos = Projectile.oldPos[0] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(drawColor) * 0.33f;

            Color color2 = Projectile.GetAlpha(drawColor) * 0.16f;

            if (EffectTimer >= (1 * EffectTimerMult) && EffectTimer < (2 * EffectTimerMult))
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/StardustProjCellBack").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.2f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= (2 * EffectTimerMult) && EffectTimer < (3 * EffectTimerMult))
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/StardustProjCellBack").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.2f, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/StardustProjCellBack").Value, drawPos, null, color2, Projectile.rotation, drawOrigin, 1.35f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= (3 * EffectTimerMult) && EffectTimer < (4 * EffectTimerMult))
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/StardustProjCellBack").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.2f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= (4 * EffectTimerMult))
            {
                EffectTimer = -1 * EffectTimerMult;
            }
            return false;
        }
    }
}