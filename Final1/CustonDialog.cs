using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final1
{
    public class CustomDialog
    {
        private Texture2D _backgroundTexture;
        private Rectangle _dialogRectangle;
        private SpriteFont _font;
        private string _message;
        private Vector2 _textPosition;
        private bool _isVisible;
        private Game1 _game;

        public CustomDialog(Game1 game, Texture2D backgroundTexture, SpriteFont font, string message)
        {
            _game = game;
            _backgroundTexture = backgroundTexture;
            _font = font;
            _message = message;
            _isVisible = false;

            // Set the size of the dialog based on the background texture size
            _dialogRectangle = new Rectangle(
                (game.GraphicsDevice.Viewport.Width - backgroundTexture.Width) / 2,
                (game.GraphicsDevice.Viewport.Height - backgroundTexture.Height) / 2,
                backgroundTexture.Width,
                backgroundTexture.Height
            );

            // Calculate text position to be centered within the dialog
            Vector2 textSize = font.MeasureString(message);
            _textPosition = new Vector2(
                _dialogRectangle.X + (_dialogRectangle.Width - textSize.X) / 2,
                _dialogRectangle.Y + (_dialogRectangle.Height - textSize.Y) / 2
            );
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
            if (!_isVisible)
                return;

            // Close the dialog if the user presses the Enter key or clicks outside the dialog
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) ||
                (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                 !_dialogRectangle.Contains(Mouse.GetState().Position)))
            {
                Hide();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_isVisible)
                return;

            spriteBatch.Draw(_backgroundTexture, _dialogRectangle, Color.White);
            spriteBatch.DrawString(_font, _message, _textPosition, Color.Black);
        }
    }
}
