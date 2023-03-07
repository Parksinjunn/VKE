using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.Ammo
{
    public class ChlorophyteProj : AmmoProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //This is the projectile penetration
            Projectile.hostile = false;
            Projectile.tileCollide = true;                 //This make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.CursedTorch, Vel.X, Vel.Y, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        bool HitTile;
        bool HasDoneEffect;
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (HitTile)
            {
                behindNPCsAndTiles.Add(index);
                return;
            }
        }
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            HitTile = true;
            Projectile.hide = true;
            Projectile.penetrate = -1;
            Projectile.damage = 0;
            Projectile.tileCollide = false;
            Projectile.velocity = oldVelocity;
            Projectile.timeLeft = 90;
            return false;
        }
        public override void SafeAI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (HitTile)
            {
                Projectile.velocity = Projectile.velocity * 0.55f;
                if(Main.rand.Next(0, 8) == 2)
                {
                    Vector2 Vel = Main.rand.NextVector2Circular(4, 4);
                    int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.CursedTorch, Vel.X, Vel.Y, Scale: 1.5f);
                    Main.dust[dust1].noGravity = true;
                }
            }
            else if(!HitTile)
            {
                //Adapted from Vanilla Chlorophyte code

                //Dust spawning code
                for (int num87 = 0; num87 < 10; num87++)
                {
                    float x2 = Projectile.Center.X - Projectile.velocity.X / 10f * (float)num87;
                    float y2 = Projectile.Center.Y - Projectile.velocity.Y / 10f * (float)num87;
                    int DustTrail = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.CursedTorch);
                    Main.dust[DustTrail].alpha = Projectile.alpha;
                    Main.dust[DustTrail].position.X = x2;
                    Main.dust[DustTrail].position.Y = y2;
                    Dust DustObj = Main.dust[DustTrail];
                    DustObj.velocity *= 0f;
                    Main.dust[DustTrail].noGravity = true;
                }
                //Targeting code
                float AvgVelocity = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
                float StoredAvgVelocity = Projectile.localAI[0];
                if (StoredAvgVelocity == 0f)
                {
                    Projectile.localAI[0] = AvgVelocity;
                    StoredAvgVelocity = AvgVelocity;
                }
                float StoredPosX = Projectile.position.X;
                float StoredPosY = Projectile.position.Y;
                float MaxDistance = 300f;
                bool TargetIdentified = false;
                int StoredTargetID = 0;

                if (Projectile.ai[1] == 0f)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC target = Main.npc[i];
                        if (target.CanBeChasedBy() && Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(i + 1))
                        {
                            float TargetPosX = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                            float TargetPosY = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                            float TargetDistance = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - TargetPosX) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - TargetPosY);
                            if (TargetDistance < MaxDistance && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                            {
                                MaxDistance = TargetDistance;
                                StoredPosX = TargetPosX;
                                StoredPosY = TargetPosY;
                                TargetIdentified = true;
                                StoredTargetID = i;
                            }
                        }
                    }
                    if (TargetIdentified)
                    {
                        Projectile.ai[1] = StoredTargetID + 1;
                    }
                    TargetIdentified = false;

                }
                if (Projectile.ai[1] > 0f)
                {
                    int SecondStoredTargetID = (int)(Projectile.ai[1] - 1f);
                    if (Main.npc[SecondStoredTargetID].active && Main.npc[SecondStoredTargetID].CanBeChasedBy(Projectile, ignoreDontTakeDamage: true) && !Main.npc[SecondStoredTargetID].dontTakeDamage)
                    {
                        float TargetPosX2 = Main.npc[SecondStoredTargetID].position.X + (float)(Main.npc[SecondStoredTargetID].width / 2);
                        float TargetPosY2 = Main.npc[SecondStoredTargetID].position.Y + (float)(Main.npc[SecondStoredTargetID].height / 2);
                        if (Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - TargetPosX2) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - TargetPosY2) < 1000f)
                        {
                            TargetIdentified = true;
                            StoredPosX = Main.npc[SecondStoredTargetID].position.X + (float)(Main.npc[SecondStoredTargetID].width / 2);
                            StoredPosY = Main.npc[SecondStoredTargetID].position.Y + (float)(Main.npc[SecondStoredTargetID].height / 2);
                        }
                    }
                    else
                    {
                        Projectile.ai[1] = 0f;
                    }
                }
                if (!Projectile.friendly)
                {
                    TargetIdentified = false;
                }
                if (TargetIdentified)
                {
                    float OrigVelocity = StoredAvgVelocity;
                    Vector2 ProjectilePos = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float DiffX = StoredPosX - ProjectilePos.X;
                    float DiffY = StoredPosY - ProjectilePos.Y;
                    float Distance = (float)Math.Sqrt(DiffX * DiffX + DiffY * DiffY);
                    Distance = OrigVelocity / Distance;
                    DiffX *= Distance;
                    DiffY *= Distance;
                    int CurveMult = 8; //Higher numbers lower velocity leading to smoother curving but less accuracy, value of 1 removes curving
                    Projectile.velocity.X = (Projectile.velocity.X * (float)(CurveMult - 1) + DiffX) / (float)CurveMult;
                    Projectile.velocity.Y = (Projectile.velocity.Y * (float)(CurveMult - 1) + DiffY) / (float)CurveMult;
                }
            }
        }
    }
}