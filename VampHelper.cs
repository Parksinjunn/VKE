using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using VKE.NPCs;

namespace VKE
{
    public static class VampHelper
    {
        public static Vector2 TurnRight(this Vector2 vec) => new(-vec.Y, vec.X);
        public static Vector2 TurnLeft(this Vector2 vec) => new(vec.Y, -vec.X);
        public static Vector2 PolarVector(float radius, float theta) =>
            new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        public static VampGlobalNPC Redemption(this NPC npc) => npc.GetGlobalNPC<VampGlobalNPC>(true);

        public static void DamageNPC(NPC npc, int dmgAmt, float knockback, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false, bool crit = false, Item item = null)
        {
            int hitDirection = damager == null ? 0 : damager.direction;
            DamageNPC(npc, dmgAmt, knockback, hitDirection, damager, dmgVariation, hitThroughDefense, crit, item);
        }

        /*
         *  Damages the NPC by the given amount.
         *  
         *  dmgAmt : The amount of damage to inflict.
         *  knockback : The amount of knockback to inflict.
         *  hitDirection : The direction of the damage.
         *  damager : the thing actually doing damage (Player, Projectile or null)
         *  dmgVariation : If true, the damage will vary based on Main.DamageVar().
         *  hitThroughDefense : If true, boosts damage to get around npc defense.
         */
        public static void DamageNPC(NPC npc, int dmgAmt, float knockback, int hitDirection, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false, bool crit = false, Item item = null)
        {
            if (item == null)
                item = new Item(ItemID.WoodenSword);
            if (npc.dontTakeDamage || (npc.immortal && npc.type != NPCID.TargetDummy))
                return;
            if (hitThroughDefense) { dmgAmt += (int)(npc.defense * 0.5f); }
            if (damager == null || damager is NPC)
            {
                int parsedDamage = dmgAmt; if (dmgVariation) { parsedDamage = Main.DamageVar(dmgAmt); }

                if (damager is NPC)
                {
                    NPCLoader.ModifyHitNPC(damager as NPC, npc, ref parsedDamage, ref knockback, ref crit);
                    NPCLoader.OnHitNPC(damager as NPC, npc, parsedDamage, knockback, crit);
                    npc.Redemption().attacker = damager;
                }

                npc.StrikeNPC(parsedDamage, knockback, hitDirection, crit);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    //NetMessage.SendData(MessageID.DamageNPC, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1, knockback, hitDirection, parsedDamage);
                }
            }
            else if (damager is Projectile p)
            {
                if (p.owner == Main.myPlayer && NPCLoader.CanBeHitByProjectile(npc, p) != false)
                {
                    int parsedDamage = dmgAmt; if (dmgVariation) { parsedDamage = Main.DamageVar(dmgAmt); }
                    NPCLoader.ModifyHitByProjectile(npc, p, ref parsedDamage, ref knockback, ref crit, ref hitDirection);
                    NPCLoader.OnHitByProjectile(npc, p, parsedDamage, knockback, crit);
                    PlayerLoader.ModifyHitNPCWithProj(p, npc, ref parsedDamage, ref knockback, ref crit, ref hitDirection);
                    PlayerLoader.OnHitNPCWithProj(p, npc, parsedDamage, knockback, crit);
                    ProjectileLoader.ModifyHitNPC(p, npc, ref parsedDamage, ref knockback, ref crit, ref hitDirection);
                    ProjectileLoader.OnHitNPC(p, npc, parsedDamage, knockback, crit);

                    if (!npc.immortal && npc.canGhostHeal && p.DamageType == DamageClass.Magic && Main.player[p.owner].setNebula && Main.player[p.owner].nebulaCD == 0 && Main.rand.NextBool(3))
                    {
                        Main.player[p.owner].nebulaCD = 30;
                        int num35 = Utils.SelectRandom(Main.rand, 3453, 3454, 3455);
                        int num36 = Item.NewItem(p.GetSource_OnHit(npc), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, num35);
                        Main.item[num36].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[num36].velocity.X = Main.rand.Next(10, 31) * 0.2f * (float)hitDirection;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, num36);
                        }
                    }

                    npc.StrikeNPC(parsedDamage, knockback, hitDirection, crit);
                    if (Main.player[p.owner].accDreamCatcher)
                        Main.player[p.owner].addDPS(parsedDamage);

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        //NetMessage.SendData(MessageID.DamageNPC, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1, knockback, hitDirection, parsedDamage);

                    if (p.penetrate != 1) { npc.immune[p.owner] = 10; }
                }
            }
            else if (damager is Player player)
            {
                if (player.whoAmI == Main.myPlayer && NPCLoader.CanBeHitByItem(npc, player, item) != false)
                {
                    int parsedDamage = dmgAmt; if (dmgVariation) { parsedDamage = Main.DamageVar(dmgAmt); }
                    NPCLoader.ModifyHitByItem(npc, player, item, ref parsedDamage, ref knockback, ref crit);
                    NPCLoader.OnHitByItem(npc, player, item, parsedDamage, knockback, crit);
                    PlayerLoader.ModifyHitNPC(player, item, npc, ref parsedDamage, ref knockback, ref crit);
                    PlayerLoader.OnHitNPC(player, item, npc, parsedDamage, knockback, crit);

                    npc.StrikeNPC(parsedDamage, knockback, hitDirection, crit);
                    if (player.accDreamCatcher)
                        player.addDPS(parsedDamage);

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        //NetMessage.SendData(MessageID.DamageNPC, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1, knockback, hitDirection, parsedDamage);
                    }
                    npc.immune[player.whoAmI] = player.itemAnimation;
                }
            }
        }
    }
}