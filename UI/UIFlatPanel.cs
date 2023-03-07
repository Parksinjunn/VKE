using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace vke.UI
{
    class UIFlatPanel : UIElement
    {
        public Color BackgroundColor = Color.Gray;
        private static Texture2D _backgroundTexture;

        public UIFlatPanel()
        {
            if (_backgroundTexture == null)
                _backgroundTexture = ModContent.Request<Texture2D>("VKE/UI/Misc/Bg").Value;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            spriteBatch.Draw(_backgroundTexture, new Rectangle(point1.X, point1.Y, width, height), BackgroundColor);
        }
    }
}