using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace VKE.Dusts
{
    public class MelBoardDust : ModDust
    {
        bool White;
        float CyanR = 35f;
        float CyanG = 243f;
        float CyanB = 245f;
        bool Cyan = true;
        float PurpleR = 241f;
        float PurpleG = 89f;
        float PurpleB = 255f;
        bool Purple;
        bool SwitchColors;
        float Divisor = 300;
        float FinColR;
        float FinColG;
        float FinColB;
        int ColDelay;
        int ColDelayMax = 30;
        bool YVelBigger;
        bool CheckedYVel;
        public override void OnSpawn(Dust dust)
        {
            YVelBigger = false;
            CheckedYVel = false;
            dust.alpha = 230;
            base.OnSpawn(dust);
        }
        public override bool Update(Dust dust)
        {
            if (Math.Abs(dust.velocity.Y) > Math.Abs(dust.velocity.X) && !CheckedYVel)
            {
                CheckedYVel = true;
                YVelBigger = true;
            }
            else if (!CheckedYVel)
                CheckedYVel = true;
            if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
            {
                if (YVelBigger)
                {
                    if (dust.scale > 0.5f)
                    {
                        dust.velocity.Y *= 0.25f;
                        dust.velocity.X += Main.rand.NextFloat(-0.2f, 0.2f);
                    }
                    if (dust.scale <= 0.5f)
                    {
                        dust.velocity.Y -= 0.15f;
                        dust.velocity.X += Main.rand.NextFloat(-0.2f, 0.2f);
                    }
                }
                else
                {
                    if (dust.scale > 0.5f)
                    {
                        dust.velocity.X = 0f;
                        dust.velocity.Y += Main.rand.NextFloat(-0.2f, 0.2f);
                    }
                    if (dust.scale <= 0.5f)
                    {
                        if (dust.velocity.X > 0f)
                        {
                            dust.velocity.X -= 0.15f;
                        }
                        else if (dust.velocity.X < 0f)
                        {
                            dust.velocity.X += 0.15f;
                        }
                        dust.velocity.Y += Main.rand.NextFloat(-0.2f, 0.2f);
                    }
                }
            }
            dust.position += dust.velocity;
            dust.scale -= 0.03f;


            ColDelay++;
            if (ColDelay > ColDelayMax)
            {
                ColDelay = 0;
                if (!White && !Purple && Cyan && FinColR > CyanR)
                {
                    FinColR -= (255f - CyanR) / Divisor;
                    FinColG -= (255f - CyanG) / Divisor;
                    FinColB -= (255f - CyanB) / Divisor;
                }
                else if (!White && !Purple && Cyan && FinColR <= CyanR)
                {
                    FinColR = CyanR;
                    FinColG = CyanG;
                    FinColB = CyanB;
                    White = true;
                    Cyan = false;
                }
                if (White && !Purple && !Cyan && FinColR < 255f)
                {
                    if (!SwitchColors)
                    {
                        FinColR += (255f - CyanR) / Divisor;
                        FinColG += (255f - CyanG) / Divisor;
                        FinColB += (255f - CyanB) / Divisor;
                    }
                    else
                    {
                        FinColR += (255f - PurpleR) / Divisor;
                        FinColG += (255f - PurpleG) / Divisor;
                        FinColB += (255f - PurpleB) / Divisor;
                    }
                }
                else if (White && !Purple && !Cyan && FinColR >= 255f)
                {
                    FinColR = FinColG = FinColB = 255f;
                    White = false;
                    if (SwitchColors)
                    {
                        SwitchColors = false;
                        Cyan = true;
                    }
                    else if (!SwitchColors)
                    {
                        SwitchColors = true;
                        Purple = true;
                    }
                }
                if (!White && Purple && !Cyan && FinColG > PurpleG)
                {
                    FinColR -= (255f - PurpleR) / Divisor;
                    FinColG -= (255f - PurpleG) / Divisor;
                    FinColB -= (255f - PurpleB) / Divisor;
                }
                else if (!White && Purple && !Cyan && FinColG <= PurpleG)
                {
                    FinColR = PurpleR;
                    FinColG = PurpleG;
                    FinColB = PurpleB;
                    White = true;
                    Purple = false;
                }
            }
            Lighting.AddLight(dust.position, new Vector3(FinColR / 255f, FinColG / 255f, FinColB / 255f));
            
            dust.color = new Color(FinColR / 255f, FinColG / 255f, FinColB / 255f);
            if (dust.scale < 0.3f)
            {
                dust.active = false;
            }
            if(dust.alpha > 110)
                dust.alpha -= 10;
            return false;
        }
        //public override Color? GetAlpha(Dust dust, Color lightColor)
        //{
            
        //    return new Color(FinColR / 255f, FinColG / 255f, FinColB / 255f, 25f);
        //}
    }
}