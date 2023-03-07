using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Items.Misc;

namespace vke.Projectiles.BloodAltarProj
{

    class BalitiusDaggerProj : ModProjectile
    {
        int Cooldown;
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 19;
            Projectile.penetrate = -1;
            Projectile.scale = 1.2f;
            Projectile.alpha = 0;

            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
		public float movementFactor
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
        public override bool IsLoadingEnabled(Mod mod)
        {
            return base.IsLoadingEnabled(mod);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (!target.boss)
			{
				for (int i = 0; i < 58; i++)
				{
					Item crystal = Main.player[Projectile.owner].inventory[i];
					if (crystal.ModItem is BloodCrystalSoul soul && soul.NPCID == -69)
					{
						soul.NPCID = target.type;
						soul.NPCName = target.FullName;
						break;
					}
				}
			}
		}
		public override void AI()
		{
			Player projOwner = Main.player[Projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = projOwner.Center.X - (float)(Projectile.width / 2);
			Projectile.position.Y = projOwner.Center.Y - (float)(Projectile.height / 2);
			if (!projOwner.frozen)
			{
				if (movementFactor == 0f)
				{
					movementFactor = 3f;
					Projectile.netUpdate = true;
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
				{
					movementFactor -= 2.4f;
				}
				else
				{
					movementFactor += 2.1f;
				}
			}
			Projectile.position += Projectile.velocity * movementFactor;
			if (projOwner.itemAnimation == 0)
			{
				Projectile.Kill();
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}
		}
	}
}