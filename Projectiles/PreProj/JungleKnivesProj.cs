using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PreProj
{
	public class JungleKnivesProj : KnifeProjectile
	{
        int TurnAroundTime;
		public override void SafeSetDefaults()
		{
			Projectile.Name = "Jungle Knife";
			Projectile.width = 22;
			Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.penetrate = 2;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 3;           //this is projectile frames
            Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
			Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.frame = Main.rand.Next(3);
            TurnAroundTime = Main.rand.Next(170, 180);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(150, 150, 150);
        }
        public override void AI()
		{
            if(Projectile.penetrate == 1)
            {
                Projectile.damage = 0;
            }
            if (Projectile.timeLeft < TurnAroundTime)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center) * (1600 / Projectile.timeLeft), 1f);
                Projectile.rotation = (float)Math.Atan2((double)-Projectile.velocity.Y, (double)-Projectile.velocity.X) + (1.57f);
                if (Main.myPlayer == Projectile.owner)
                {
                    Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                    Rectangle value12 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
                    if (rectangle.Intersects(value12))
                    {
                        Projectile.Kill();
                    }
                }
            }
            else
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (1.57f);
            }
        }

        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if (Projectile.timeLeft > 170)
                Projectile.timeLeft = 140;
            Projectile.velocity *= 0f;
            n.AddBuff(ModContent.BuffType<Buffs.PenetratingPoison>(), 300); //confused 2
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 170)
                Projectile.timeLeft = 140;
            Projectile.tileCollide = false;
            Projectile.velocity *= 0f;
            return false;
        }
        public override bool PreDrawExtras()
        {
            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 distToProj = playerCenter - Projectile.Center;
            float projRotation = distToProj.ToRotation() - 1.57f;
            float distance = distToProj.Length();
            while (distance > 34f && !float.IsNaN(distance))
            {
                distToProj.Normalize();                 //get unit vector
                distToProj *= 24f;                      //speed = 24
                center += distToProj;                   //update draw position
                distToProj = playerCenter - center;    //update distance
                distance = distToProj.Length();

                //Draw chain
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("vke/Projectiles/PreProj/JungleKnivesProjChain").Value, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                    new Rectangle(0, 0, 22, 34), new Color(150, 150, 150), projRotation,
                    new Vector2(22f / 2f, 34f / 2f), 1f, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}