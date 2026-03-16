using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Final1
{
    public class CustomDialog
    {
        private Texture2D _backgroundTexture;
        private SpriteFont _font;
        private string _message;
        private bool _isVisible;

        public CustomDialog(Texture2D backgroundTexture, SpriteFont font, string message)
        {
            _backgroundTexture = backgroundTexture;
            _font = font;
            _message = message;
            _isVisible = false;
        }

        public void Show()
        {
            _isVisible = true;
        }

        public void Hide()
        {
            _isVisible = false;
        }

        public void Update(GameTime gameTime)
        {
            if (_isVisible && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Hide();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isVisible)
            {
                spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);
                Vector2 textSize = _font.MeasureString(_message);
                Vector2 textPosition = new Vector2((GraphicsDevice.Viewport.Width - textSize.X) / 2, (GraphicsDevice.Viewport.Height - textSize.Y) / 2);
                spriteBatch.DrawString(_font, _message, textPosition, Color.Black);
            }
        }
    }
}