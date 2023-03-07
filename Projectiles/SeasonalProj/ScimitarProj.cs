using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.SeasonalProj
{
    public class ScimitarProj : KnifeProjectile
    {
        bool Instantiated = false;
        public override void SafeSetDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.penetrate = 6;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 200;
        }
        int SpriteRotation = 45;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation()+MathHelper.ToRadians(SpriteRotation); // projectile faces sprite right
            Projectile.localAI[0] += 1f;
            //projectile.light = .04f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            if(Instantiated == false)
            {
                Projectile.frame = Main.rand.Next(0, 5);
                Instantiated = true;
            }
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 2) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 5) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            return true;
        }

        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            n.AddBuff(ModContent.BuffType<Buffs.BleedingOut2>(), 300);
        }
        public override bool SafeOnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Tink.WithVolumeScale(0.5f), Projectile.position);
            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 1, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.Gray, 1f);
            return true;
        }
    }
}