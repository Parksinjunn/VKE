using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace VKE.Projectiles.PostMLProj
{
    public class PsiKnivesProj : KnifeProjectile
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
        public override void SafeSetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 240;
            Main.projFrames[Projectile.type] = 7;
            Projectile.scale = 0.95f;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (HitTile || HitNPC)
            {
                behindNPCsAndTiles.Add(index);
                return;
            }
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Hoods(n);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
                // Inflate some target hitboxes if they are beyond 8,8 size
                if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
                {
                    targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
                }
                // Return if the hitboxes intersects, which means the knive collides or not
            return projHitbox.Intersects(targetHitbox);
        }
        public bool isStickingToTarget
        {
            get { return Projectile.ai[0] == 1f; }
            set { Projectile.ai[0] = value ? 1f : 0f; }
        }

        // WhoAmI of the current target
        public float targetWhoAmI
        {
            get { return Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(Mode == 0 && !HitTile)
            {
                HitNPC = true;
                Projectile.tileCollide = false;
                targetWhoAmI = (float)target.whoAmI; // Set the target whoAmI
                Projectile.velocity =
                    (target.Center - Projectile.Center) *
                    0.75f; // Change velocity based on delta center of targets (difference between entity centers)
                Projectile.netUpdate = true; // netUpdate this knive
                //projectile.damage = 0;
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        SoundStyle PsiBeep = new SoundStyle("VKE/Sounds/Item/PsiBeep") with { Volume = 0.3f};
        public override void AI()
        {
            if(Mode == 2 && Projectile.timeLeft <= DefenseInitiateTime)
            {
                if(!RotationStored)
                {
                    RotationStore = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) - 1.57f;
                    RotationStored = true;
                }
                if (Projectile.frame >= 3)
                {
                    LightR = 0.029f + (Projectile.frame / 12f);
                    LightG = 0.045f + (Projectile.frame / 12f);
                    LightB = 0.046f + (Projectile.frame / 12f);
                }
                Lighting.AddLight(Projectile.position, LightR, LightG, LightB);
                Projectile.penetrate = 1;
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= (int)(4f))
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                    if (Projectile.frame > 3)
                    {
                        Projectile.frame = 4;
                        Projectile.velocity *= 0f;
                        Projectile.rotation = RotationStore;
                    }
                    else
                    {
                        Projectile.velocity *= 0.1f;
                        Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                    }
                }
            }
            else if(Mode == 2 && Projectile.timeLeft > DefenseInitiateTime)
            {
                VelocityStore = Projectile.velocity;
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            }
            else if(Mode == 1 || Mode == 0)
            {
                if (Mode == 1 && !HitTile)
                {
                    Projectile.velocity.Y += 0.5f;
                }
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                if (Projectile.frame >= 3)
                {
                    LightR = 0.029f + (Projectile.frame / 12f);
                    LightG = 0.045f + (Projectile.frame / 12f);
                    LightB = 0.046f + (Projectile.frame / 12f);
                }
                Lighting.AddLight(Projectile.position, LightR, LightG, LightB);
                if (HitTile || HitNPC || Projectile.timeLeft <= 90)
                {
                    Projectile.frameCounter++;
                    if (Projectile.frameCounter == 1)
                    {
                        VelocityStore = Projectile.velocity;
                        //Main.NewText("Velocity: " + (VelocityStore * 100));
                    }
                    if (Projectile.frameCounter >= (int)(40f))
                    {
                        if(Projectile.frame == 1 || Projectile.frame == 3 || Projectile.frame == 5)
                            SoundEngine.PlaySound(PsiBeep, Projectile.position);
                        Projectile.frame++;
                        Projectile.frameCounter = 36;
                        if (Projectile.frame > 6)
                        {
                            Projectile.Kill();
                        }
                    }
                    Projectile.velocity = Projectile.velocity * 0.8f;
                    if (HasDoneEffect == false)
                    {
                        HasDoneEffect = true;
                        for (int i = 0; i < 10; i++)
                        {
                            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 1, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * -0.2f, 10, Color.Gray, 1f);
                        }
                    }
                }
            }
            if (HitNPC)
            {
                bool hitEffect = false;
                Projectile.localAI[0] += 1f;
                hitEffect = Projectile.localAI[0] % 30f == 0f;
                int projTargetIndex = (int)targetWhoAmI;
                if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage)
                {
                    Projectile.Center = Main.npc[projTargetIndex].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
                    if (hitEffect)
                    {
                        Main.npc[projTargetIndex].HitEffect(0, 1.0);
                    }
                }
                else 
                {
                    Projectile.Kill();
                }
            }
        }
        Vector2 collidePoint;
        int i;
        public override void Kill(int timeLeft)
        {
            if (HitTile)
            {
                i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Mode == 2 ? (Projectile.Center + (VelocityStore * 8)) : Mode == 0 ? (Projectile.Center - (VelocityStore * 8)) : (collidePoint), Mode == 2 ? VelocityStore / 16 : Projectile.velocity * -0.01f, ModContent.ProjectileType<PsiKnivesProjExplosion>(), 1, 5f, Projectile.owner);
            }
            else if (HitNPC)
            {
                i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), (Main.npc[(int)targetWhoAmI].position), Projectile.velocity * new Vector2(0, -0.1f), ModContent.ProjectileType<PsiKnivesProjExplosion>(), 1, 5f, Projectile.owner);
            }
            else
            {
                i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Mode == 2 ? (Projectile.Center + (VelocityStore * 8)) : (Projectile.Center - (VelocityStore * 8)), Mode == 2 ? VelocityStore / 16 : Projectile.velocity * -0.01f, ModContent.ProjectileType<PsiKnivesProjExplosion>(), 1, 50f, Projectile.owner);
            }
            var p = Main.projectile[i].ModProjectile as Projectiles.PostMLProj.PsiKnivesProjExplosion;
            p.OriginDmg = Projectile.damage;
            base.Kill(timeLeft);
        }

        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            //Main.NewText("Velocity: " + projectile.velocity);
            HitTile = true;
            Projectile.hide = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            collidePoint = Projectile.position;
            //if (projectile.velocity.Y >= 6f || projectile.velocity.Y < -6f)
            //{
            //    projectile.velocity.X *= 0f;
            //}
            //else
            //{
            //    projectile.velocity.Y *= 0f;
            //}
            return false;
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
}