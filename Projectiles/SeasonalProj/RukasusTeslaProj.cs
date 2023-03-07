using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.SeasonalProj
{
    public class RukasusTeslaProj : KnifeProjectile
    {
        bool FakeBolt;
        public override void SafeSetDefaults()
        {
            Projectile.Name = "Rukasus Tesla";
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 100;
        }

        public override void AI()
        {
            if (!ZenithActive)
            {
                //this is projectile dust
                int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 0, 0, 20, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.LightBlue, 0.5f);
                Main.dust[DustID2].noGravity = false;
                //this make that the projectile faces the right way 
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            }

        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(6, 6);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Electric, Vel.X, Vel.Y, Scale: 0.75f);
                Main.dust[dust1].noGravity = true;
            }
            base.Kill(timeLeft);
        }
        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            n.AddBuff(BuffID.Electrified, 240);
            if(Main.rand.NextBool(20))
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), n.Center, new Vector2(0, 0), ModContent.ProjectileType<RukasusTeslaBall>(), Projectile.damage, 8, Projectile.owner);
            }
            for (int j = 0; j < 32; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(20, 20);
                int dust1 = Dust.NewDust(new Vector2(n.position.X, n.position.Y), (int)n.Size.X, (int)n.Size.Y, DustID.PurificationPowder, Vel.X, Vel.Y, Scale: 0.7f);
                Main.dust[dust1].noGravity = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255);
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 3) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 3) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            return true;
        }
    }
}