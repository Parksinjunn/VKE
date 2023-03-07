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

namespace VKE.Projectiles.PostMLProj
{
    public class ThetaKnivesProj : KnifeProjectile
    {
        public int Mode; //0 = Sticky, 1 = Bombing, 2 = Defense
        public float shootToX;
        public float shootToY;
        public Vector2 TargetPos;

        private const int NumAnimationFrames = 4;

        // This value controls how many frames it takes for the Prism to reach "max charge". 60 frames = 1 second.
        public const float MaxCharge = 1000f;
        float ChargeCounter = 350;
        float ChargeDivisionFactor = 40f;
        static int ShootTimerMax = 50;
        int ShootTimer;
        static int DustTimerMax = 17;
        int DustTimer;
        int HealthTimer;

        // This value controls how many frames it takes for the beams to begin dealing damage. Before then they can't hit anything.
        public const float DamageStart = 1f;

        // This value controls how sluggish the Prism turns while being used. Vanilla Last Prism is 0.08f.
        // Higher values make the Prism turn faster.
        private const float AimResponsiveness = 0.3f;

        // This value controls how frequently the Prism emits sound once it's firing.
        private const int SoundInterval = 20;

        // These values place caps on the mana consumption rate of the Prism.
        // When first used, the Prism consumes mana once every MaxManaConsumptionDelay frames.
        // Every time mana is consumed, the pace becomes one frame faster, meaning mana consumption smoothly increases.
        // When capped out, the Prism consumes mana once every MinManaConsumptionDelay frames.
        private const float MaxManaConsumptionDelay = 15f;
        private const float MinManaConsumptionDelay = 5f;

        // This property encloses the internal AI variable projectile.ai[0]. It makes the code easier to read.
        private float FrameCounter
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        // This property encloses the internal AI variable projectile.ai[1].
        private float NextManaFrame
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        // This property encloses the internal AI variable projectile.localAI[0].
        // localAI is not automatically synced over the network, but that does not cause any problems in this case.
        private float ManaConsumptionRate
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Example Last Prism");
            Main.projFrames[Projectile.type] = NumAnimationFrames;

            // Signals to Terraria that this projectile requires a unique identifier beyond its index in the projectile array.
            // This prevents the issue with the vanilla Last Prism where the beams are invisible in multiplayer.
            ProjectileID.Sets.NeedsUUID[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            ShootTimer = 50;
            DustTimer = 17;
            // Use CloneDefaults to clone all basic projectile statistics from the vanilla Last Prism.
            Projectile.CloneDefaults(ProjectileID.LastPrism);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (ChargeCounter < 360)
            {
                ChargeCounter++;
            }

            Vector2 rrp = player.Center - new Vector2(0f, 40f);

            // Update the frame counter.
            FrameCounter += 1f;

            // Update projectile visuals and sound.
            UpdateAnimation();
            PlaySounds();

            // Update the Prism's position in the world and relevant variables of the player holding it.
            UpdatePlayerVisuals(player, rrp);

            // Update the Prism's behavior: project beams on frame 1, consume mana, and despawn if out of mana.
            if (Projectile.owner == Main.myPlayer)
            {
                // Slightly re-aim the Prism every frame so that it gradually sweeps to point towards the mouse.
                
                //UpdateAim(rrp, player.HeldItem.shootSpeed);

                // The Prism immediately stops functioning if the player is Cursed (player.noItems) or "Crowd Controlled", e.g. the Frozen debuff.
                // player.channel indicates whether the player is still holding down the mouse button to use the item.
                bool stillInUse = player.channel && !player.noItems && !player.CCed;

                // Spawn in the Prism's lasers on the first frame if the player is capable of using the item.
                if (stillInUse)
                {
                    DustTimer++;
                    if (DustTimer >= DustTimerMax)
                    {

                        //Main.NewText("End1: " + End1);
                        //Main.NewText("End2: " + End2);
                    }
                    float numProjectiles2 = player.GetModPlayer<VampPlayer>().NumProj + player.GetModPlayer<VampPlayer>().ExtraProj;
                    int ShootTimerAdd = (int)(1f * (ChargeCounter / (MaxCharge / ChargeDivisionFactor + numProjectiles2)));
                    if (ShootTimerAdd < 1)
                    {
                        ShootTimer++;
                    }
                    else
                    {
                        ShootTimer += ShootTimerAdd;
                    }
                    if (ShootTimer > ShootTimerMax)
                    {
                        for (int x = 0; x < numProjectiles2; x++)
                        {
                            int i;
                            Vector2 pos = (Projectile.Center).Floor();
                            if(Mode == 0)
                                i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos.X - ((-2 + x) * 3), pos.Y, ((-2 + x) * 6f), -3f, ModContent.ProjectileType<ThetaKnivesProjSpear>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                            else if(Mode == 1)
                            {
                                i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos.X, pos.Y - 20f, 0f, 0f, ModContent.ProjectileType<ThetaKnivesProjSpear>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                                Main.projectile[i].ai[0] = x;
                            }
                            else
                            {
                                i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos.X, pos.Y, ((-2 + x) * 6f), -3f, ModContent.ProjectileType<ThetaKnivesProjSpear>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                                Main.projectile[i].ai[0] = x;
                            }
                            var p = Main.projectile[i].ModProjectile as ThetaKnivesProjSpear;
                            p.Mode = Mode;
                            p.TargetPos = Main.MouseWorld;
                            if (Mode == 0)
                                p.Projectile.tileCollide = false;
                        }
                        ShootTimer = 0;
                    }
                }
                else if (!stillInUse)
                {
                    Projectile.Kill();
                }
            }
            Projectile.timeLeft = 2;
        }

        private void UpdateAnimation()
        {
            Projectile.frameCounter++;

            int framesPerAnimationUpdate = FrameCounter >= MaxCharge ? 1 : FrameCounter >= (MaxCharge * 0.80f) ? 2 : FrameCounter >= (MaxCharge * 0.60f) ? 3 : FrameCounter >= (MaxCharge * 0.40f) ? 4 : FrameCounter >= (MaxCharge * 0.20f) ? 5 : 6;

            if (Projectile.frameCounter >= framesPerAnimationUpdate)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= NumAnimationFrames)
                {
                    Projectile.frame = 0;
                }
            }
        }

        private void PlaySounds()
        {
            // The Prism makes sound intermittently while in use, using the vanilla projectile variable soundDelay.
            if (Projectile.soundDelay <= 0)
            {
                Projectile.soundDelay = SoundInterval;

                // On the very first frame, the sound playing is skipped. This way it doesn't overlap the starting hiss sound.
                if (FrameCounter > 1f)
                {
                    SoundEngine.PlaySound(SoundID.Item39, Projectile.position);
                }
            }
        }

        private void UpdatePlayerVisuals(Player player, Vector2 playerHandPos)
        {
            // Place the Prism directly into the player's hand at all times.
            Projectile.Center = playerHandPos;
            // The beams emit from the tip of the Prism, not the side. As such, rotate the sprite by pi/2 (90 degrees).
            Projectile.rotation = -MathHelper.PiOver4 + player.fullRotation;
            Vector2 dir = Vector2.Normalize(Main.MouseWorld - player.position);
            if (dir.X > 0)
                Projectile.direction = 1;
            else
                Projectile.direction = -1;
            Projectile.spriteDirection = 1;

            // The Prism is a holdout projectile, so change the player's variables to reflect that.
            // Constantly resetting player.itemTime and player.itemAnimation prevents the player from switching items or doing anything else.
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;

            // If you do not multiply by projectile.direction, the player's hand will point the wrong direction while facing left.
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
        }

        private void UpdateAim(Vector2 source, float speed)
        {
            // Get the player's current aiming direction as a normalized vector.
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);
            if (aim.HasNaNs())
            {
                aim = -Vector2.UnitY;
            }

            // Change a portion of the Prism's current velocity so that it points to the mouse. This gives smooth movement over time.
            aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(Projectile.velocity), aim, AimResponsiveness));
            aim *= speed;

            if (aim != Projectile.velocity)
            {
                Projectile.netUpdate = true;
            }

            Projectile.velocity = aim;
        }

        // Because the Prism is a holdout projectile and stays glued to its user, it needs custom drawcode.
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            // The Prism is always at full brightness, regardless of the surrounding light. This is equivalent to it being its own glowmask.
            // It is drawn in a non-white color to distinguish it from the vanilla Last Prism.
            Color drawColor = new Color(255, 255, 255);
            Main.EntitySpriteDraw(texture, (Projectile.Center - Main.screenPosition).Floor(), new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation + MathHelper.ToRadians((float)(Math.PI / 4f)), new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            return false;
        }

        //public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //    Player player = Main.player[projectile.owner];

        //    spriteBatch.Draw(mod.GetTexture("Items/ForPeople/Zephrion/ThetaKnivesProjSingle"), player.position, new Rectangle(0, 0, 24, 24), lightColor);
        //}
    }
}