using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;
using Terraria.Graphics.Shaders;

namespace VKE.Projectiles.PostMLProj
{
    public class MoonLordsHandProj : KnifeProjectile, IPixelPrimitiveDrawer
    {
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 1.3f;
            return MathHelper.SmoothStep(baseWidth, 1f, completionRatio);
        }
        public Color ColorFunction(float completionRatio)
        {
            return Color.Lerp(Color.Transparent, Color.Cyan, completionRatio) * 0.7f;
        }
        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
            GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(VampTextureRegistry.VortexTrail);
            TrailDrawer.DrawPixelPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 1;
            ProjectileID.Sets.TrailCacheLength[Type] = 25;
        }
        Vector2 Randomizer;
        int Delay = 0;
        public override void SafeSetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6;
            Randomizer = Main.rand.NextVector2Circular(70, 70);
        }
        short[] BTDusts = new short[] { DustID.Vortex, DustID.IceRod, DustID.MagicMirror};
        bool Stored;
        Vector2 StoredVel;
        Vector2 StoredPos;
        public override void AI()
        {
            if(!Stored)
            {
                Stored = true;
                StoredVel = Projectile.velocity;
                StoredPos = Main.MouseWorld;
            }
            for(int i = 0; i < 3; i++)
            {
                int num121 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, BTDusts[i%3], 0f, 0f, 100, default(Color), 1.4f);
                Main.dust[num121].noGravity = true;
            }
            if(Delay < 3)
            {
                Delay++;
                Vector2 velo = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center + (StoredVel * 10)) * 12f, 0.10f);
                Projectile.velocity = velo;
            }
            else if(Delay < 6)
            {
                Delay++;
                Vector2 velo = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo((Main.player[Projectile.owner].Center + (StoredVel * 10) + Randomizer)) * 12f, 0.05f);
                Projectile.velocity = velo;
            }
            else if (Delay < Main.rand.Next(12, 19))
            {
                Delay++;
                Vector2 velo = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(StoredPos) * 12f, 0.05f);
                Projectile.velocity = velo;
            }
            else
            {
                if (Projectile.ai[0] == 0f)
                {
                    bool flag = true;
                    int ProjectileType = Projectile.type;
                    if (flag)
                    {
                        Projectile.ai[1] += 1f;
                    }
                    if (Projectile.ai[1] >= 30f)
                    {
                        Projectile.ai[0] = 1f;
                        Projectile.ai[1] = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                else
                {
                    if(Delay < 45)
                    {
                        Delay++;
                        Vector2 velo = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center + (Randomizer * 2)) * 12f, 0.1f);
                        Projectile.velocity = velo;
                    }
                    else
                    {
                        Vector2 velo = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center) * 18f, 0.07f);
                        Projectile.velocity = velo;
                    }
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                        Rectangle value12 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
                        if (rectangle.Intersects(value12))
                        {
                            Projectile.Kill();
                        }
                    }
                }
            }
            Projectile.rotation += 0.4f * (float)Projectile.direction;
        }
        int HitCount;
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            HitCount++;
            if (HitCount == 1)
            {
                if (!n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjChanceScale * 1.5f)) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjBossChanceScale * 1.5f)) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
            }
            Hoods(n);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new List<Vector2>();
            //Projectile.FillWhipControlPoints(Projectile, list);

            //DrawLine(list);

            //Main.DrawWhip_WhipBland(Projectile, list);
            // The code below is for custom drawing.
            // If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
            // However, you must adhere to how they draw if you do.

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            //Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 pos = Projectile.oldPos[0];

            for (int i = 0; i < Projectile.oldPos.Length - 1; i++)
            {
                // These two values are set to suit this projectile's sprite, but won't necessarily work for your own.
                // You can change them if they don't!
                Rectangle frame = new Rectangle(0, 0, 22, 26);
                Vector2 origin = new Vector2(0, 0);
                float scale = 0.005f;

                // These statements determine what part of the spritesheet to draw for the current segment.
                // They can also be changed to suit your sprite.
                if (i == Projectile.oldPos.Length - 2)
                {
                    frame.Y = 74;
                    frame.Height = 18;

                    // For a more impactful look, this scales the tip of the whip up when fully extended, and down when curled up.
                    //Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    //float t = Timer / Projectile.timeLeft;
                    //scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
                }
                else if (i > 10)
                {
                    frame.Y = 58;
                    frame.Height = 16;
                }
                else if (i > 5)
                {
                    frame.Y = 42;
                    frame.Height = 16;
                }
                else if (i > 0)
                {
                    frame.Y = 26;
                    frame.Height = 16;
                }

                Vector2 element = Projectile.oldPos[i];
                Vector2 diff = Projectile.oldPos[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                pos += diff;
            }
            return false;
        }
    }
}