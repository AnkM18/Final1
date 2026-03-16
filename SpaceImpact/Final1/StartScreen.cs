using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final1
{
    public class StartScreen
    {
        private Texture2D _playButtonTexture;
        private Texture2D _exitButtonTexture;
        private Game1 _game;
        private Rectangle _playButtonRectangle;
        private Rectangle _exitButtonRectangle;

        public StartScreen(Game1 game, Texture2D playButtonTexture, Texture2D exitButtonTexture)
        {
            _game = game;
            _playButtonTexture = playButtonTexture;
            _exitButtonTexture = exitButtonTexture;

            // Set button positions
            _playButtonRectangle = new Rectangle((GraphicsDeviceManager.DefaultBackBufferWidth - _playButtonTexture.Width) / 2, 
                                                 (GraphicsDeviceManager.DefaultBackBufferHeight / 2) - 50, 
                                                 _playButtonTexture.Width, 
                                                 _playButtonTexture.Height);
            _exitButtonRectangle = new Rectangle((GraphicsDeviceManager.DefaultBackBufferWidth - _exitButtonTexture.Width) / 2, 
                                                 (GraphicsDeviceManager.DefaultBackBufferHeight / 2) + 50, 
                                                 _exitButtonTexture.Width, 
                                                 _exitButtonTexture.Height);
        }

        public void LoadContent()
        {
            // Load any additional content if necessary
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (_playButtonRectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _game.StartGame();
            }

            if (_exitButtonRectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _game.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_playButtonTexture, _playButtonRectangle, Color.White);
            spriteBatch.Draw(_exitButtonTexture, _exitButtonRectangle, Color.White);
            spriteBatch.End();
        }
    }
}