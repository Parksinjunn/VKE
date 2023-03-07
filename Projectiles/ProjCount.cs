using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE;

namespace VKE.Projectiles
{
    public class ProjCount
    {
        public static int MaxActive = 20;
        public static int NumberActive;
        public static int NumActiveAdamantite;
        public static int NumActiveTitanium;
        public static int NumActiveShroomite;
        public static int ShroomiteIterator;
        public static int ShroomiteActiveGasCount;
        public static int LightningActiveCount;
        public static int PumpkinActiveCount;
        public static List<int> ZenithProj = new List<int>();
        public static List<int> ZenithType = new List<int>();

        public static int GetActiveAdamantite()
        {
            return NumActiveAdamantite;
        }
        public static int GetActiveTitanium()
        {
            return NumActiveTitanium;
        }
        public static int GetActiveShroomite()
        {
            return NumActiveShroomite;
        }
        public static int GetShroomiteIterator()
        {
            return ShroomiteIterator;
        }
        public static int GetActiveShroomiteGasCount()
        {
            return ShroomiteActiveGasCount;
        }
        public static int GetActiveConut()
        {
            return NumberActive;
        }
        public static int GetLightningActiveCount()
        {
            return LightningActiveCount;
        }
        public static int GetPumpkinActiveCount()
        {
            return PumpkinActiveCount;
        }
    }
}