using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Projectiles.PreProj
{
    public class ChainProj : KnifeProjectile
    {
        public override void SafeSetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 6;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            TurnAroundTime = Main.rand.Next(170, 180);
        }
        float Rotation;
        int TurnAroundTime;
        public override void AI()
        {
            if (Projectile.timeLeft < TurnAroundTime)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center) * (1600/Projectile.timeLeft), 1f);
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
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(Projectile.timeLeft > 170)
                Projectile.timeLeft = 140;
            Projectile.tileCollide = false;
            Projectile.velocity *= 0f;
            return false;
        }
        public override void SafeOnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            if (Projectile.timeLeft > 170)
                Projectile.timeLeft = 140;
            Projectile.velocity *= 0f;
        }
        //public override bool PreDrawExtras()
        //{
        //    Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
        //    Vector2 center = Projectile.Center;
        //    Vector2 distToProj = playerCenter - Projectile.Center;
        //    float projRotation = distToProj.ToRotation() - 1.57f;
        //    float distance = distToProj.Length();
        //    while (distance > 34f && !float.IsNaN(distance))
        //    {
        //        distToProj.Normalize();                 //get unit vector
        //        distToProj *= 24f;                      //speed = 24
        //        center += distToProj;                   //update draw position
        //        distToProj = playerCenter - center;    //update distance
        //        distance = distToProj.Length();

        //        //Draw chain
        //        Main.EntitySpriteDraw(ModContent.Request<Texture2D>("vke/Projectiles/PreProj/ChainProjChain").Value, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
        //            new Rectangle(0, 0, 10, 34), new Color(179, 160, 154), projRotation,
        //            new Vector2(10f / 2f, 34f / 2f), 1f, SpriteEffects.None, 0);
        //    }
        //    return false;
        //}
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);

            // This fixes a vanilla GetPlayerArmPosition bug causing the chain to draw incorrectly when stepping up slopes. The flail itself still draws incorrectly due to another similar bug. This should be removed once the vanilla bug is fixed.
            playerArmPosition.Y -= Main.player[Projectile.owner].gfxOffY;

            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("vke/Projectiles/PreProj/ChainProjChain3");

            Rectangle? chainSourceRectangle = null;
            // Drippler Crippler customizes sourceRectangle to cycle through sprite frames: sourceRectangle = asset.Frame(1, 6);
            float chainHeightAdjustment = 0f; // Use this to adjust the chain overlap. 

            Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
            Vector2 chainDrawPosition = Projectile.Center;
            Vector2 vectorFromProjectileToPlayerArms = playerArmPosition.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
            Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
            float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
            if (chainSegmentLength == 0)
            {
                chainSegmentLength = 10; // When the chain texture is being loaded, the height is 0 which would cause infinite loops.
            }
            float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
            int chainCount = 0;
            float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength / 2f;

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (chainLengthRemainingToDraw > 0f)
            {
                // This code gets the lighting at the current tile coordinates
                Color chainDrawColor = Lighting.GetColor((int)chainDrawPosition.X / 16, (int)(chainDrawPosition.Y / 16f));

                // Flaming Mace and Drippler Crippler use code here to draw custom sprite frames with custom lighting.
                // Cycling through frames: sourceRectangle = asset.Frame(1, 6, 0, chainCount % 6);
                // This example shows how Flaming Mace works. It checks chainCount and changes chainTexture and draw color at different values

                var chainTextureToDraw = chainTexture;
                // Here, we draw the chain texture at the coordinates
                Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, chainDrawColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);

                // chainDrawPosition is advanced along the vector back to the player by the chainSegmentLength
                chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
                chainCount++;
                chainLengthRemainingToDraw -= chainSegmentLength;
            }
            return true;
        }
    }
}