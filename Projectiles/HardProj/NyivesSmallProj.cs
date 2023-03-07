using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.HardProj
{
    public class NyivesSmallProj : KnifeProjectile
    {
        public override void SafeSetDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.scale = 1f;
        }
        int SpriteRotation = 45;
        public override void AI()
        {
            //this make that the projectile faces the right way 
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(SpriteRotation); // projectile faces sprite right
            Lighting.AddLight(Projectile.Center, (Main.DiscoR / 255f), (Main.DiscoG / 255f), (Main.DiscoB / 255f));
        }
        public override bool SafePreKill(int timeLeft)
        {
            if(Main.rand.Next(1,101) >= 80)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 502, Projectile.damage / 2, Projectile.knockBack, Projectile.owner); //Creates a new Projectile
            }
            return base.SafePreKill(timeLeft); ;
        }
        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector2 Vel = Main.rand.NextVector2Circular(6, 6);
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.RainbowTorch, Vel.X, Vel.Y, newColor: Color.Transparent, Scale: 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);

            }
            base.Kill(timeLeft);
        }
        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Projectile.frameCounter++; //increase the frameCounter by one
            if (Projectile.frameCounter >= 3) //once the frameCounter has reached 3 - change the 10 to change how fast the projectile animates
            {
                Projectile.frame++; //go to the next frame
                Projectile.frameCounter = 0; //reset the counter
                if (Projectile.frame > 7) //if past the last frame
                    Projectile.frame = 0; //go back to the first frame
            }
            return true;
        }
    }
}