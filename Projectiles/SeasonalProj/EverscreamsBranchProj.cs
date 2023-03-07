using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.SeasonalProj
{
    public class EverscreamsBranchProj : KnifeProjectile
    {
        int ProjFrame;
        bool HitTile;
        bool HasDoneEffect;
        float RMax = 1f;
        float Rmin = 0.4f;
        float BMax = 1f;
        float Bmin = 0.4f;
        float GMax = 0.8f;
        float Gmin = 0.2f;
        float FadePos;
        bool Dec;
        bool Red;
        bool Blue;
        float LightR = 0f;
        float LightG = 0f;
        float LightB = 0f;
        public override void SafeSetDefaults()
        {
            Projectile.Name = "AbyssalKnives";
            Projectile.width = 20;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Main.projFrames[Projectile.type] = 16;
            Projectile.scale = 1f;
            ProjFrame = Main.rand.Next(0, 16);
            if(ProjFrame >= 8)
            {
                Red = Main.rand.NextBool();
                if (!Red)
                    Blue = true;
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (HitTile)
            {
                behindNPCsAndTiles.Add(index);
                return;
            }
        }
        public override void AI()
        {
            //this is projectile dust
            //int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width - 3, projectile.height - 3, 158, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 10, Color.LightBlue, 1f);
            //Main.dust[DustID2].noGravity = true;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            if(!HitTile)
                Projectile.velocity.Y += 0.4f;
            //projectile.velocity *= 0f;
            if(ProjFrame >= 0 && ProjFrame < 4)
            {
                if (LightR < RMax && !Dec)
                    FadePos += 0.05f;
                else if (LightR > Rmin && Dec)
                    FadePos -= 0.05f;
                if (LightR >= 1f && !Dec)
                    Dec = true;
                else if (LightR <= Rmin && Dec)
                    Dec = false;
                LightR = Rmin + (RMax - Rmin) * FadePos;
            }
            else if(ProjFrame >= 4 && ProjFrame <= 7)
            {
                if (LightB < BMax && !Dec)
                    FadePos += 0.05f;
                else if (LightB > Bmin && Dec)
                    FadePos -= 0.05f;
                if (LightB >= 1f && !Dec)
                    Dec = true;
                else if (LightB <= Bmin && Dec)
                    Dec = false;
                LightB = Bmin + (BMax - Bmin) * FadePos;
                LightG = Gmin + (GMax - Gmin) * FadePos;
            }
            else
            {
                if(Red)
                {
                    if (LightR < RMax && !Dec)
                        FadePos += 0.05f;
                    else if (LightR > Rmin && Dec)
                        FadePos -= 0.05f;
                    if (LightR >= 1f && !Dec)
                        Dec = true;
                    else if (LightR <= Rmin && Dec)
                    {
                        Blue = true;
                        Red = false;
                        Dec = false;
                    }
                    LightR = Rmin + (RMax - Rmin) * FadePos;
                }
                else if(Blue)
                {
                    if (LightB < BMax && !Dec)
                        FadePos += 0.05f;
                    else if (LightB > Bmin && Dec)
                        FadePos -= 0.05f;
                    if (LightB >= 1f && !Dec)
                        Dec = true;
                    else if (LightB <= Bmin && Dec)
                    {
                        Blue = false;
                        Red = true;
                        Dec = false;
                    }
                    LightB = Bmin + (BMax - Bmin) * FadePos;
                    LightG = Gmin + (GMax - Gmin) * FadePos;
                }
            }
            Lighting.AddLight(Projectile.position, LightR, LightG, LightB);

            if (HitTile)
            {
                Projectile.velocity = Projectile.velocity * 0.2f;
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
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            HitTile = true;
            Projectile.hide = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.velocity = oldVelocity;
            Projectile.timeLeft = 45;
            return false;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            if (ProjFrame >= 0 && ProjFrame < 4)
            {
                if (!n.boss)
                {
                    if (Main.rand.Next(0, HealProjChanceScale) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, owner.whoAmI);
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, HealProjBossChanceScale) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, owner.whoAmI);
                    }
                }
            }
            else if (ProjFrame >= 4 && ProjFrame <= 7)
            {
                if (!n.boss)
                {
                    if (Main.rand.Next(0, HealProjChanceScale) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, (int)(Projectile.damage * 1.25f), 0, owner.whoAmI);
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, HealProjBossChanceScale) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, (int)(Projectile.damage * 1.25f), 0, owner.whoAmI);
                    }
                }
            }
            else
            {
                if (!n.boss)
                {
                    if (Main.rand.Next(0, HealProjChanceScale) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.35), 0, owner.whoAmI);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, (int)(Projectile.damage * 0.78), 0, owner.whoAmI);
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, HealProjBossChanceScale) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.35), 0, owner.whoAmI);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ManaHeal").Type, (int)(Projectile.damage * 0.78), 0, owner.whoAmI);
                    }
                }
            }

            Hoods(n);
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
            Projectile.frame = ProjFrame;
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), lightColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            Main.EntitySpriteDraw(ModContent.Request<Texture2D>("VKE/Projectiles/SeasonalProj/EverscreamsBranchProj_LightMap").Value, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            return false;
        }
    }
}