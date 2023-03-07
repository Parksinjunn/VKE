using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.NPCs;

namespace VKE.Projectiles.HardProj
{
    public class SpectreProj : KnifeProjectile
    {
        private int NPCsHIT;

        public override void SafeSetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 34;
            Projectile.friendly = true;
            Main.projFrames[Projectile.type] = 6;           //this is projectile frames
            Projectile.penetrate = 3;
            Projectile.hostile = false;
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 110;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Terraria.Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 106, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 0, new Color(0, 255, 142), 1f)];
                dust.noGravity = true;
                dust.fadeIn = 0.5f;
                dust.shader = GameShaders.Armor.GetSecondaryShader(24, Main.LocalPlayer);
            }
            Lighting.AddLight(Projectile.Center, 0f, 0.50f, 0.3f);

            //this make that the projectile faces the right way 
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 3) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 5) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            Projectile.localAI[0] += 1f;
            //projectile.light = .04f;
            Projectile.alpha = (int)(Projectile.localAI[0] * 2);
        }
        int HitCount;
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            HitCount++;
            if (HitCount == 1)
            {
                if (!n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjChanceScale * 2f)) <= VampKnives.HealProjectileSpawn)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
                else if (n.boss)
                {
                    if (Main.rand.Next(0, (int)(HealProjBossChanceScale * 2f)) <= HealProjBossChance)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("HealProj").Type, (int)(Projectile.damage * 0.75), 0, Projectile.owner);
                    }
                }
            }
            Hoods(n);
        }

        public override bool SafePreKill(int timeLeft)
        {
            for (int x = 0; x < 18; x++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Terraria.Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width + 5, Projectile.height + 5, 106, Projectile.velocity.X * -0.2f, Projectile.velocity.Y * -0.2f, 0, new Color(0, 255, 142), 2f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(24, Main.LocalPlayer);
            }
            return base.SafePreKill(timeLeft);
        }
    }
}