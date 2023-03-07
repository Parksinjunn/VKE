﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.Ammo
{
    public class OrichalcumProj : AmmoProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 2;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
            ArmorPiercingMult = 1.25f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (!ZenithActive)
            {
                // If attached to an NPC, draw behind tiles (and the npc) if that NPC is behind tiles, otherwise just behind the NPC.
                if (Projectile.ai[0] == 1f) // or if(isStickingToTarget) since we made that helper method.
                {
                    int npcIndex = (int)Projectile.ai[1];
                    if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
                    {
                        if (Main.npc[npcIndex].behindTiles)
                            behindNPCsAndTiles.Add(index);
                        else
                            behindNPCs.Add(index);
                        return;
                    }
                }
                // Since we aren't attached, add to this list
                behindProjectiles.Add(index);
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            if (!ZenithActive)
            {
                // For going through platforms and such, knives use a tad smaller size
                width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
            }
            return true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (!ZenithActive)
            {
                // Inflate some target hitboxes if they are beyond 8,8 size
                if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
                {
                    targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
                }
                // Return if the hitboxes intersects, which means the knive collides or not
            }
            return projHitbox.Intersects(targetHitbox);
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Plantera_Pink, Vel.X, Vel.Y, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
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

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            Projectile.damage = (int)(Projectile.damage * 0.5f);
            if (!ZenithActive)
            {
                // If you'd use the example above, you'd do: isStickingToTarget = 1f;
                // and: targetWhoAmI = (float)target.whoAmI;
                isStickingToTarget = true; // we are sticking to a target
                targetWhoAmI = (float)target.whoAmI; // Set the target whoAmI
                Projectile.velocity =
                    (target.Center - Projectile.Center) *
                    0.75f; // Change velocity based on delta center of targets (difference between entity centers)
                Projectile.netUpdate = true; // netUpdate this knive
                //target.AddBuff(ModContent.BuffType<Buffs.BleedingOut>(), 900); // Adds the Exampleknive debuff for a very small DoT

                //Projectile.damage = 0; // Makes sure the sticking knives do not deal damage anymore

                // The following code handles the knive sticking to the enemy hit.
                int maxStickingknives = 3; // This is the max. amount of knives being able to attach
                Point[] stickingknives = new Point[maxStickingknives]; // The point array holding for sticking knives
                int knifeIndex = 0; // The knive index
                for (int i = 0; i < Main.maxProjectiles; i++) // Loop all projectiles
                {
                    Projectile currentProjectile = Main.projectile[i];
                    if (i != Projectile.whoAmI // Make sure the looped projectile is not the current knive
                        && currentProjectile.active // Make sure the projectile is active
                        && currentProjectile.owner == Main.myPlayer // Make sure the projectile's owner is the client's player
                        && currentProjectile.type == Projectile.type // Make sure the projectile is of the same type as this knive
                        && currentProjectile.ai[0] == 1f // Make sure ai0 state is set to 1f (set earlier in ModifyHitNPC)
                        && currentProjectile.ai[1] == (float)target.whoAmI
                    ) // Make sure ai1 is set to the target whoAmI (set earlier in ModifyHitNPC)
                    {
                        stickingknives[knifeIndex++] =
                            new Point(i, currentProjectile.timeLeft); // Add the current projectile's index and timeleft to the point array
                        if (knifeIndex >= stickingknives.Length
                        ) // If the knive's index is bigger than or equal to the point array's length, break
                        {
                            break;
                        }
                    }
                }
                // Here we loop the other knives if new knive needs to take an older knive's place.
                if (knifeIndex >= stickingknives.Length)
                {
                    int oldknifeIndex = 0;
                    // Loop our point array
                    for (int i = 1; i < stickingknives.Length; i++)
                    {
                        // Remove the already existing knive if it's timeLeft value (which is the Y value in our point array) is smaller than the new knive's timeLeft
                        if (stickingknives[i].Y < stickingknives[oldknifeIndex].Y)
                        {
                            oldknifeIndex = i; // Remember the index of the removed knive
                        }
                    }
                    // Remember that the X value in our point array was equal to the index of that knive, so it's used here to kill it.
                    Main.projectile[stickingknives[oldknifeIndex].X].Kill();
                }
            }
        }

        // Added these 2 constant to showcase how you could make AI code cleaner by doing this
        // Change this number if you want to alter how long the knive can travel at a constant speed
        private const float maxTicks = 45f;

        // Change this number if you want to alter how the alpha changes
        private const int alphaReduction = 25;

        public override void AI()
        {
            if (!ZenithActive)
            {
                if (!isStickingToTarget)
                {
                     Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                }


                if (isStickingToTarget)
                {
                    // These 2 could probably be moved to the ModifyNPCHit hook, but in vanilla they are present in the AI
                    Projectile.ignoreWater = true; // Make sure the projectile ignores water
                    Projectile.tileCollide = false; // Make sure the projectile doesn't collide with tiles anymore
                    int aiFactor = 15; // Change this factor to change the 'lifetime' of this sticking javelin
                    bool killProj = false; // if true, kill projectile at the end
                    bool hitEffect = false; // if true, perform a hit effect
                    Projectile.localAI[0] += 1f;
                    // Every 30 ticks, the javelin will perform a hit effect
                    hitEffect = Projectile.localAI[0] % 30f == 0f;
                    int projTargetIndex = (int)targetWhoAmI;
                    if (Projectile.localAI[0] >= (float)(60 * aiFactor)// If it's time for this javelin to die, kill it
                        || (projTargetIndex < 0 || projTargetIndex >= 200)) // If the index is past its limits, kill it
                    {
                        killProj = true;
                    }
                    else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage) // If the target is active and can take damage
                    {
                        // Set the projectile's position relative to the target's center
                        Projectile.Center = Main.npc[projTargetIndex].Center - Projectile.velocity * 2f;
                        Projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
                        if (hitEffect) // Perform a hit effect here
                        {
                            Main.npc[projTargetIndex].HitEffect(0, 1.0);
                        }
                    }
                    else // Otherwise, kill the projectile
                    {
                        killProj = true;
                    }

                    if (killProj) // Kill the projectile
                    {
                        Projectile.Kill();
                    }
                }
            }
        }
    }
}