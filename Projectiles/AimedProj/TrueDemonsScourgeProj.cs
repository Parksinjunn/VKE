using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;
using VKE.NPCs;
using Terraria.Graphics.Shaders;

namespace VKE.Projectiles.AimedProj
{
    public class TrueDemonsScourgeProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Demon's Scourge");
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
            Color Rainbow = new Color(Main.DiscoR, Main.DiscoR, 0);
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        int Delay;
        bool HasChanneled;
        float SpeedMult;
        float OrbitSpeed;
        public override void SafeSetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //projectile is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //projectile make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            Projectile.scale = 0.65f;
            SpeedMult = Main.rand.NextFloat(0.90f, 1.4f);
            Main.projFrames[Projectile.type] = 5;
            OrbitSpeed = Main.rand.NextFloat(4f, 9f);
        }
        Vector2 MouseStopped;
        public override void AI()
        {
            Delay++;
            int num118 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 3.5f);
            Main.dust[num118].noGravity = true;
            Dust dust3 = Main.dust[num118];
            dust3.velocity *= 1.4f;
            num118 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1.5f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Projectile.velocity.X >= -0.10f && Projectile.velocity.Y >= -0.10f && Projectile.velocity.X <= 0.10f && Projectile.velocity.Y <= 0.10f)
            {
                HasChanneled = true;
                MouseStopped = Main.MouseWorld;
            }

            if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f && Delay > 4)
            {
                Projectile.rotation += (float)Projectile.direction * 0.8f;
                if (Main.MouseWorld == MouseStopped && Main.player[Projectile.owner].channel)
                {
                    double deg = (double)Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                    double rad = deg * (Math.PI / 180); //Convert degrees to radians
                    double dist = 50; //Distance away from the player
                    float DistanceMult = 10f;
                    float posAddX = ((Main.MouseWorld.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2) - Projectile.position.X) / DistanceMult;
                    float posAddY = ((Main.MouseWorld.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2) - Projectile.position.Y) / DistanceMult;
                    Projectile.velocity = new Vector2(posAddX, posAddY);
                    Projectile.ai[1] += OrbitSpeed;
                }
                else if (Main.player[Projectile.owner].channel)
                {
                    Projectile.rotation += (float)Projectile.direction * 0.8f;
                    float num146 = 12f;
                    Vector2 vector10 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    Projectile.rotation += (float)Projectile.direction * 0.8f;
                    float num147 = (float)Main.mouseX + Main.screenPosition.X - vector10.X;
                    Projectile.rotation += (float)Projectile.direction * 0.8f;
                    float num148 = (float)Main.mouseY + Main.screenPosition.Y - vector10.Y;
                    Projectile.rotation += (float)Projectile.direction * 0.8f;
                    if (Main.player[Projectile.owner].gravDir == -1f)
                    {
                        num148 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector10.Y;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                    }
                    float num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
                    num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
                    if (num149 > num146)
                    {
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        num149 = num146 / num149;
                        num147 *= num149;
                        num148 *= num149;
                        int num150 = (int)(num147 * 1000f);
                        int num151 = (int)(Projectile.velocity.X * 1000f);
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        int num152 = (int)(num148 * 1000f);
                        int num153 = (int)(Projectile.velocity.Y * 1000f);
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        if (num150 != num151 || num152 != num153)
                        {
                            Projectile.rotation += (float)Projectile.direction * 0.8f;
                            Projectile.netUpdate = true;
                            Projectile.rotation += (float)Projectile.direction * 0.8f;
                        }
                        Projectile.velocity.X = num147;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        Projectile.velocity.Y = num148;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                    }
                    else
                    {
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        int num154 = (int)(num147 * 1000f);
                        int num155 = (int)(Projectile.velocity.X * 1000f);
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        int num156 = (int)(num148 * 1000f);
                        int num157 = (int)(Projectile.velocity.Y * 1000f);
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        if (num154 != num155 || num156 != num157)
                        {
                            Projectile.rotation += (float)Projectile.direction * 0.8f;
                            Projectile.netUpdate = true;
                            Projectile.rotation += (float)Projectile.direction * 0.8f;
                        }
                        Projectile.velocity.X = num147;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        Projectile.velocity.Y = num148;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                    }
                    Projectile.rotation += (float)Projectile.direction * 0.8f;
                    Projectile.velocity *= SpeedMult;
                }
                else if (HasChanneled)
                {
                    Projectile.rotation += (float)Projectile.direction * 0.8f;
                    if (Projectile.ai[0] == 0f)
                    {
                        Projectile.ai[0] = 1f;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        Projectile.netUpdate = true;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        float num158 = 12f;
                        Vector2 vector11 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                        float num159 = (float)Main.mouseX + Main.screenPosition.X - vector11.X;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        float num160 = (float)Main.mouseY + Main.screenPosition.Y - vector11.Y;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        if (Main.player[Projectile.owner].gravDir == -1f)
                        {
                            num160 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector11.Y;
                            Projectile.rotation += (float)Projectile.direction * 0.8f;
                        }
                        float num161 = (float)Math.Sqrt((double)(num159 * num159 + num160 * num160));
                        if (num161 == 0f)
                        {
                            vector11 = new Vector2(Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2), Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2));
                            Projectile.rotation += (float)Projectile.direction * 0.8f;
                            num159 = Projectile.position.X + (float)Projectile.width * 0.5f - vector11.X;
                            Projectile.rotation += (float)Projectile.direction * 0.8f;
                            num160 = Projectile.position.Y + (float)Projectile.height * 0.5f - vector11.Y;
                            Projectile.rotation += (float)Projectile.direction * 0.8f;
                            num161 = (float)Math.Sqrt((double)(num159 * num159 + num160 * num160));
                        }
                        num161 = num158 / num161;
                        num159 *= num161;
                        num160 *= num161;
                        Projectile.velocity.X = num159;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        Projectile.velocity.Y = num160;
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                        {
                            Projectile.Kill();
                        }
                    }
                    Projectile.rotation += (float)Projectile.direction * 0.8f;
                }
                Projectile.rotation += (float)Projectile.direction * 0.8f;
            }
            Projectile.rotation += (float)Projectile.direction * 0.8f;
            if (Projectile.velocity.X != 0f || Projectile.velocity.Y != 0f)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) - 2.355f;
                Projectile.rotation += (float)Projectile.direction * 0.8f;
            }
            Projectile.rotation += (float)Projectile.direction * 0.8f;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
                Projectile.rotation += (float)Projectile.direction * 0.8f;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10 with { Volume = 0.5f}, Projectile.position);
            int num4;
            for (int num582 = 0; num582 < 20; num582 = num4 + 1)
            {
                int num583 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, (0f - Projectile.velocity.X) * 0.2f, (0f - Projectile.velocity.Y) * 0.2f, 100, default(Color), 2f);
                Main.dust[num583].noGravity = true;
                Dust dust = Main.dust[num583];
                dust.velocity *= 2f;
                num583 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, (0f - Projectile.velocity.X) * 0.2f, (0f - Projectile.velocity.Y) * 0.2f, 100, default(Color), 1f);
                dust = Main.dust[num583];
                dust.velocity *= 2f;
                num4 = num582;
            }
        }
        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if(n.HasBuff(BuffID.OnFire))
            {
                n.AddBuff(ModContent.BuffType<Buffs.Hellfire>(), Main.rand.Next(240, 480));
            }
            else
            {
                n.AddBuff(BuffID.OnFire,Main.rand.Next(240,480));
            }
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 2) //once the frameCounter has reached 2 - change the 2 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 4) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            return true;
        }
    }
}