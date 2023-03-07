using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace vke.UI
{
    class EntranceBackgroundPanel : UIPanel
    {
        public EntranceBackgroundPanel()
        {

        }

        //protected override void DrawSelf(SpriteBatch spriteBatch)
        //{
        //    CalculatedStyle dimensions = GetDimensions();
        //    Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
        //    int width = (int)Math.Ceiling(dimensions.Width);
        //    int height = (int)Math.Ceiling(dimensions.Height);
        //    spriteBatch.Draw(_backgroundTexture, new Rectangle(point1.X, point1.Y, width, height), BackgroundColor);
        //}
    }
}