using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using VKE.Items.Misc;

namespace vke.Projectiles.BloodAltarProj
{
	public class BloodCrystalProj : ModProjectile
	{
        public override void SetDefaults()
		{
            Projectile.aiStyle = -1;
			Projectile.width = 18;
			Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 7;           //this is projectile frames
            Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
			Projectile.ignoreWater = true;
            Projectile.timeLeft = 180;
            //Main.NewText("Spawned");
        }
        ref float NPCID => ref Projectile.ai[0];
        public override void AI()
        {                                                  //this is projectile dust
            int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - 3, Projectile.height - 3, 244, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 10, Color.DarkRed, 1.8f);
            Main.dust[DustID2].noGravity = true;
            Projectile.light = .04f;
        }        public override void Kill(int timeLeft)
        {
            //if (Main.netMode != NetmodeID.MultiplayerClient)
            //{
                int i = Item.NewItem(Projectile.GetSource_Death(), Projectile.position, ModContent.ItemType<BloodCrystalSoul>());
                var moditem = Main.item[i].ModItem as BloodCrystalSoul;
                moditem.NPCID = (int)NPCID;
                //NetMessage.SendData(MessageID.InstancedItem, -1, NetmodeID.Server, null, i);
            //}
        }
        float rotationalOffset;
        public override bool PreDrawExtras()
        {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = ModContent.Request<Texture2D>("VampKnives/Projectiles/BloodAltarProj/BloodCrystalProjBack").Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor = new Color(255, 255, 255);
            Main.EntitySpriteDraw(texture, sheetInsertPosition - new Vector2(1.5f, -2f), new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation + rotationalOffset, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale/1.2f, effects, 0);
            return base.PreDrawExtras();
        }
        public override void PostDraw(Color lightColor)
        {
            if(Projectile.frameCounter >= 6)
            {
                Vector2 DustPosition = Main.rand.NextVector2Circular(0.5f, 1f);
                int d = Dust.NewDust(Projectile.position + DustPosition, Projectile.width, Projectile.height, 35, 0f, 0f);
            }
            Projectile.frameCounter++; 
            if (Projectile.frameCounter >= (int)(6f * ((float)Projectile.timeLeft / 180f))) 
            {
                Projectile.frame++; 
                Projectile.frameCounter = 0; 
                if (Projectile.frame > 6)
                {
                    Projectile.frame = 0;
                    rotationalOffset += 0.12f;
                }
            }
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Player player = Main.player[Projectile.owner];
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor = new Color(255, 255, 255);
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0);
            base.PostDraw(lightColor);
        }
    }
}