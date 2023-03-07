using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace VKE.Dusts
{
    public class AmethystDust : ModDust
    {
        Color col;
        public override void OnSpawn(Dust dust)
        {
            col = Main.hslToRgb(Main.rand.NextFloat((167f / 360f), (318f / 360f)), 1f, Main.rand.NextFloat(0.4f, 0.9f));
            base.OnSpawn(dust);
        }
        public override bool Update(Dust dust)
        {
            //Lighting.AddLight(dust.position, new Vector3(FinColR / 255f, FinColG / 255f, FinColB / 255f));
            //dust.color = new Color(FinColR / 255f, FinColG / 255f, FinColB / 255f);
            if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
            {
                dust.velocity *= 0.25f;
            }
            else if(!dust.noGravity)
            {
                dust.velocity.Y += 0.35f;
            }
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.1f;
            dust.scale *= 0.99f;
            if (dust.scale < 0.3f)
            {
                dust.active = false;
            }
            return false;
        }
        //public override Color? GetAlpha(Dust dust, Color lightColor)
        //{
        //    return col;
        //}
    }
}