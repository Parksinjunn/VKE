using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using System.Runtime.InteropServices;
using Terraria.Graphics.Shaders;
using VKE.Effects.Primitives;
using Microsoft.Xna.Framework.Graphics;
using VKE.Effects;

namespace VKE.Projectiles.AimedProj
{
    public class PrismaticArcanumProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Arcanum");
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
            Color Rainbow = new Color(Main.DiscoR * (Projectile.whoAmI % 5), Main.DiscoG * (Projectile.whoAmI % 5), Main.DiscoB * (Projectile.whoAmI % 5));
            if(Rainbow == Color.Black)
            {
                Rainbow = new Color(Main.DiscoG, Main.DiscoB, Main.DiscoR);
            }    
            return Color.Lerp(Rainbow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.FadedStreak);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        int Delay;
        bool HasChanneled;
        float SpeedMult;
        int NPCsHIT;
        float OrbitSpeed;
        public override void SafeSetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //projectile is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //projectile make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            Projectile.scale = 1f;
            SpeedMult = Main.rand.NextFloat(1.1f, 1.6f);
            OrbitSpeed = Main.rand.NextFloat(4f, 9f);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            NPCsHIT++;
            if(NPCsHIT == 2)
            {
                damage = 0;
            }
            if (NPCsHIT < 3)
            {
                Projectile.penetrate += 1;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(Main.player[Projectile.owner].channel)
                return false;
            else
                return true; 
        }
        Vector2 MouseStopped;
        public override void AI()
        {
            Delay++;
            if (Projectile.soundDelay == 0 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 2f)
            {
                Projectile.soundDelay = 10;
                SoundEngine.PlaySound(SoundID.Item9 with { Volume = 0.5f}, Projectile.position);
            }

            if (Projectile.velocity.X >= -0.10f && Projectile.velocity.Y >= -0.10f && Projectile.velocity.X <= 0.10f && Projectile.velocity.Y <= 0.10f )
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
                    float DistanceMult = 5f;
                    float posAddX = ((Main.MouseWorld.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2) - Projectile.position.X) / DistanceMult;
                    float posAddY = ((Main.MouseWorld.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2) - Projectile.position.Y) / DistanceMult;
                    Projectile.velocity = new Vector2(posAddX, posAddY);
                    Projectile.ai[1] += OrbitSpeed;
                }
                else if (Main.player[Projectile.owner].channel)
                {
                    Projectile.rotation += (float)Projectile.direction * 0.8f;
                    float SpeedMult = 20f;
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
                    if (num149 > SpeedMult)
                    {
                        Projectile.rotation += (float)Projectile.direction * 0.8f;
                        num149 = SpeedMult / num149;
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
                    Projectile.velocity *= this.SpeedMult;
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
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.netUpdate = true;
        }

        public override void Kill(int timeLeft)
        {
                if (Projectile.penetrate == 1)
                {
                    Projectile.maxPenetrate = -1;
                    Projectile.penetrate = -1;
                    int num590 = 60;
                    Projectile.position.X = Projectile.position.X - (float)(num590 / 2);
                    Projectile.position.Y = Projectile.position.Y - (float)(num590 / 2);
                    Projectile.width += num590;
                    Projectile.height += num590;
                    Projectile.tileCollide = false;
                    Projectile.velocity *= 0.01f;
                    Projectile.Damage();
                    Projectile.scale = 0.01f;
                }
                SoundEngine.PlaySound(SoundID.Item10 with { Volume = 0.5f}, Projectile.position);
                int num4;
                for (int num588 = 0; num588 < 20; num588 = num4 + 1)
                {
                    int num589 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2f);
                    Main.dust[num589].noGravity = true;
                    Dust dust = Main.dust[num589];
                    dust.velocity *= 4f;
                    num4 = num588;
                }
        }
    }
}

//FOR 1.4 UPDATE!!!
//namespace VKE.Projectiles
//{
//    [StructLayout(LayoutKind.Sequential, Size = 1)]
//    public struct RainbowRodDrawer
//    {
//        private static VertexStrip _vertexStrip = new VertexStrip();

//        public void Draw(Projectile proj)
//        {
//            MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
//            miscShaderData.UseSaturation(-2.8f);
//            miscShaderData.UseOpacity(4f);
//            miscShaderData.Apply(null);
//            _vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f, false);
//            _vertexStrip.DrawTrail();
//            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
//        }

//        private Color StripColors(float progressOnStrip)
//        {
//            Color value = Main.hslToRgb((progressOnStrip * 1.6f - Main.GlobalTimeWrappedHourly) % 1f, 1f, 0.5f);
//            Color result = Color.Lerp(Color.White, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
//            result.A = 0;
//            return result;
//        }

//        private float StripWidth(float progressOnStrip)
//        {
//            float num = 1f;
//            float lerpValue = Utils.GetLerpValue(0f, 0.2f, progressOnStrip, true);
//            num *= 1f - (1f - lerpValue) * (1f - lerpValue);
//            return MathHelper.Lerp(0f, 32f, num);
//        }
//    }
//}


