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
    public class VortexProjVortex : KnifeProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex");
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 2;
        }
        int DivFactor;
        public override void SafeSetDefaults()
        {
            Projectile.width = 62;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 50;
        }
        bool LockedOn = false;
        bool PosDefined;
        public override void AI()
        {
            Projectile.rotation += 0.25f;
            for(int i = 0; i < Main.maxNPCs; i++)
            {
                NPC target = Main.npc[i];
                if (!target.friendly && target.active && target.CanBeChasedBy())
                {
                    if(!LockedOn)
                    {
                        float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                        float shootToY = target.position.Y - Projectile.Center.Y;
                        float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                        if (distance < 2000f && !target.friendly && target.active)
                        {
                            if (distance < 50 && !target.friendly && target.active)
                            {
                                LockedOn = true;
                                Projectile.velocity.X = 0f;
                                Projectile.velocity.Y = 0f;
                                Projectile.Center = target.Center;
                            }
                            else
                            {
                                distance = 3f / distance;

                                shootToX *= distance * 4;
                                shootToY *= distance * 4;

                                Projectile.velocity.X = shootToX;
                                Projectile.velocity.Y = shootToY;
                            }
                        }
                    }
                    if(LockedOn)
                    {
                        if (!PosDefined)
                        {
                            double deg = (double)Projectile.ai[0] * 72; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                            double rad = deg * (Math.PI / 180); //Convert degrees to radians
                            double dist = 50; //Distance away from the player

                            Projectile.position.X = target.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                            Projectile.position.Y = target.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                            Projectile.tileCollide = false;
                            PosDefined = true;
                            Projectile.ai[1] += (float)deg;
                        }
                        else
                        {
                            double deg = (double)Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                            double rad = deg * (Math.PI / 180); //Convert degrees to radians
                            double dist = 50; //Distance away from the player

                            float posAddX = ((target.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2) - Projectile.position.X) / 5f;
                            float posAddY = ((target.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2) - Projectile.position.Y) / 5f;
                            Projectile.velocity = new Vector2(posAddX, posAddY);
                        }

                        Projectile.netUpdate = true;
                        //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                        Projectile.ai[1] += 6f;
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.IceRod, Vel.X, Vel.Y, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        //public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        //{
        //    Player owner = Main.player[Projectile.owner];
        //    Projectile.NewProjectile(Projectile.GetSource_FromThis(), n.Center, new Vector2(0, 0), ProjectileID.StardustCellMinionShot, Projectile.damage, 8, owner.whoAmI);
        //}

        int EffectTimer;
        int EffectTimerMult = 6;
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
                Color drawColorDying = Projectile.GetAlpha(drawColor) * 0.2f;
                Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColorDying, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            }
            else
                Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);

            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = Projectile.Size / 2f;
            Vector2 drawPos = Projectile.oldPos[0] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(drawColor) * 0.25f;
            Color color2 = Projectile.GetAlpha(drawColor) * 0.15f;
            Color color3 = Projectile.GetAlpha(drawColor) * 0.05f;


            if (EffectTimer >= (1 * EffectTimerMult) && EffectTimer < (2 * EffectTimerMult))
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/VortexProjVortex").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.2f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= (2 * EffectTimerMult) && EffectTimer < (3 * EffectTimerMult))
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/VortexProjVortex").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.2f, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/VortexProjVortex").Value, drawPos, null, color2, Projectile.rotation, drawOrigin, 1.4f, SpriteEffects.None, 0);
            }
            else if (EffectTimer >= (3 * EffectTimerMult))
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/VortexProjVortex").Value, drawPos, null, color, Projectile.rotation, drawOrigin, 1.2f, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/VortexProjVortex").Value, drawPos, null, color2, Projectile.rotation, drawOrigin, 1.4f, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/HardProj/VortexProjVortex").Value, drawPos, null, color3, Projectile.rotation, drawOrigin, 1.5f, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}