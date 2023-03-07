using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.GameContent;
using System.Buffers.Text;
using VKE.Effects;
using VKE.Effects.Primitives;

namespace VKE.Projectiles.HardProj
{
    public class DaedalusStormblades_Lightning : ModProjectile
    {
        //public override string Texture => "VKE/Items/HardKnives/DaedalusStormblades";
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ownerHitCheck = true;
            Projectile.ignoreWater = true;
        }
        public float glow;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.noItems || player.CCed || player.dead || !player.active)
                Projectile.Kill();
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, true);

            if (Projectile.owner == Main.myPlayer)
            {
                if (Projectile.ai[0] == 1)
                {
                    VampDustEffects.DrawCircle(Projectile.Center + new Vector2(0f, 60f), DustID.Electric, 0.2f, 1, 1f, 0.8f, 1, nogravity: true);
                    VampDustEffects.DrawParticleElectricity(Projectile.Center + new Vector2(0f, 60f), (Projectile.Center + new Vector2(0f, -55f)) + VampHelper.PolarVector(30 * (glow + 1), Main.rand.NextFloat(0, MathHelper.TwoPi)), new LightningParticle(), 0.4f, 10, 0.05f);
                }
                else
                {
                    VampDustEffects.DrawCircle(Projectile.Center, DustID.Electric, 0.8f, 1, 1, 0.8f, 1, nogravity: true);
                    VampDustEffects.DrawParticleElectricity(Projectile.Center + VampHelper.PolarVector(30 * (glow + 1), Main.rand.NextFloat(0, MathHelper.TwoPi)), Projectile.Center + VampHelper.PolarVector(30 * (glow + 1), Main.rand.NextFloat(0, MathHelper.TwoPi)), new LightningParticle(), 0.4f, 10, 0.05f);
                }

                int dmg = 62;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (!npc.active || npc.friendly || npc.dontTakeDamage)
                        continue;

                    if (npc.DistanceSQ(Projectile.Center) > 60 * 60)
                        continue;

                    int hitDirection = Projectile.Center.X > npc.Center.X ? -1 : 1;
                    VampHelper.DamageNPC(npc, Projectile.damage + dmg * 2, Projectile.knockBack, hitDirection, Projectile/*, crit: Projectile.HeldItemCrit()*/);
                }
                if (Projectile.localAI[1]++ >= 5)
                    Projectile.Kill();
            }

            Projectile.spriteDirection = player.direction;
            Projectile.alpha = 255;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //target.AddBuff(ModContent.BuffType<ElectrifiedDebuff>(), 180);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new(texture.Width / 2f, texture.Height / 2f);
            int shader = ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex;

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + Vector2.UnitY * Projectile.gfxOffY, null, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + Vector2.UnitY * Projectile.gfxOffY, null, Projectile.GetAlpha(Color.Orange) * glow, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

            return false;
        }
    }
}
