using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects.Primitives;
using VKE.Effects;

namespace VKE.Projectiles.PostMLProj
{
    public class DeltaKnivesProj : KnifeProjectile
    {
        bool HitTile;
        bool HitNPC;
        bool HasDoneEffect;
        float FadePos;
        float LightR = 0f;
        float LightG = 0f;
        float LightB = 0f;
        float RotationStore;
        Vector2 VelocityStore;
        bool RotationStored;
        int DefenseInitiateTime = 220;
        public int Mode; //0 = Sticky, 1 = Bombing, 2 = Defense
        bool HasChanneled;
        float SpeedMult;
        int Delay;
        public float shootToX;
        public float shootToY;
        public Vector2 TargetPos;
        float distance;
        Vector2 SavedVelocity;
        bool VelocitySaved;
        bool PastTarget;
        public override void SafeSetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            //if (Mode == 0 || Mode == 1)
            //    projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            //else if(Mode == 2)
            //{
            //    shootToX = Main.mouseX;
            //    shootToY = Main.mouseY;
            //}
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 240;
            Main.projFrames[Projectile.type] = 8;
            Projectile.scale = 0.95f;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.frame >= 2)
            {
                LightR = 0.189f + (Projectile.frame / 6f);
                LightG = 0.098f + (Projectile.frame / 6f);
                LightB = 0.203f + (Projectile.frame / 6f);
            }
            //int dust1 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), (int)((1)), (int)((1)), 132, -projectile.velocity.X, -projectile.velocity.Y, newColor: Color.Purple);
            //Dust dust;
            //dust = Main.dust[Terraria.Dust.NewDust(projectile.Center - new Vector2(projectile.velocity.X, projectile.velocity.Y), 1, 1, 132, -projectile.velocity.X / 2f, -projectile.velocity.Y / 2f, 0, new Color(255, 255, 255), 1f)];
            //dust.noGravity = true;
            //dust.shader = GameShaders.Armor.GetSecondaryShader(36, Main.LocalPlayer);
            for (int i = 0; i < 4; i++)
            {
                Vector2 projectilePosition = Projectile.Center;
                projectilePosition -= Projectile.velocity * ((float)i * 0.25f);
                int dust = Dust.NewDust(projectilePosition, 1, 1, 135, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = projectilePosition;
                Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                Main.dust[dust].velocity *= 0.2f;
                Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(38, Main.LocalPlayer);
            }

            Lighting.AddLight(Projectile.position, LightR, LightG, LightB);
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 7)
                {
                    Projectile.frame = 2;
                }
            }
            if(Mode == 0)
            {
                //projectile.velocity = projectile.velocity.RotatedBy(-Math.Sin(0.6f) * 0.02f);
            }
            if (Mode == 1)
            {
                Projectile.velocity.Y += 0.28f;
            }
            else if (Mode == 2)
            {
                Delay++;
                if (Delay > 20)
                {
                    float shootToX = TargetPos.X - Projectile.position.X;
                    float shootToY = TargetPos.Y - Projectile.position.Y;

                    distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                    if (Main.myPlayer == Projectile.owner && distance > 75 && Projectile.timeLeft > 200)
                    {
                        distance = 3f / distance;

                        shootToX *= distance * 2;
                        shootToY *= distance * 2;

                        if (!PastTarget)
                        {
                            Projectile.velocity.X += shootToX;
                            Projectile.velocity.Y += shootToY;
                        }
                        if (!VelocitySaved)
                        {
                            VelocitySaved = true;
                            VelocityStore = Projectile.velocity;
                        }
                    }
                    else
                    {
                        Projectile.tileCollide = true;
                        //projectile.velocity = VelocityStore;
                        PastTarget = true;
                    }
                }
                Projectile.netUpdate = true;
            }
        }
        int i;
        float multiplier;
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            //Main.NewText("Velocity: " + projectile.velocity);
            HitTile = true;
            Projectile.hide = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Kill();
            return false;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Hoods(n);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    multiplier = Main.rand.NextFloat(0.3f, 1.6f);
                    int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)((Projectile.width - 3) * multiplier), (int)((Projectile.height - 3) * multiplier), ModContent.DustType<Dusts.DeltaExplosionDust>(), (float)(Math.Sin(-Projectile.velocity.X) * 6f * multiplier), (float)(Math.Sin(-Projectile.velocity.Y) * 6f * multiplier), Scale: 1.5f);
                    Main.dust[dust1].velocity = Main.dust[dust1].velocity - new Vector2(1.5f, 1.5f);
                    //Dusts.PsiExplosionDust.LightColor(Color.Purple.ToVector3(), Main.dust[dust1]);
                }
                //int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), (int)((projectile.width - 8) * multiplier), (int)((projectile.height - 8) * multiplier), 130, -projectile.velocity.X * multiplier, -projectile.velocity.Y * multiplier, newColor: Color.Purple);
                //Main.dust[dust2].velocity = Main.dust[dust2].velocity - new Vector2(1.5f, 1.5f);
            }
            SoundStyle DeltaExplosion = new SoundStyle("VKE/Sounds/Item/DeltaExplosion") with { Volume = 0.7f, PitchVariance = 0.45f };
            SoundEngine.PlaySound(DeltaExplosion, Projectile.position);
            i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity *= 0, ModContent.ProjectileType<DeltaKnivesProjExplosion>(), 1, 5f, Projectile.owner);
            var p = Main.projectile[i].ModProjectile as DeltaKnivesProjExplosion;
            p.OriginDmg = Projectile.damage;
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
            Color drawColor = new Color(255, 255, 255);
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), lightColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            return false;
        }
    }

    public class DeltaKnivesProjExplosion : ModProjectile
    {
        public float OriginDmg;
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 116;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.scale = 0.75f;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Inflate(56 / 2, 0);
            base.ModifyDamageHitbox(ref hitbox);
        }
        bool Hit;
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(40f * (OriginDmg / 12f));
            if(Main.rand.Next(100) <= 16)
            {
                target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(60,120));
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}