using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final1
{
    public class StartScreen
    {
        private Texture2D _playButtonTexture;
        private Texture2D _exitButtonTexture;
        
        private Texture2D _settingsButtonTexture;

        private Vector2 settingsButtonPosition;
        private Vector2 playButtonPosition;
        private Vector2 exitButtonPosition;
        
        private Game1 _game;
        private float playButtonScale = 5.0f;  
        private float exitButtonScale = 5.0f;
        private float settingsButtonScale = 5.0f;
        

        public StartScreen(Game1 game, Texture2D playButtonTexture, Texture2D exitButtonTexture, Texture2D settingsButtonTexture)
        {
            _game = game;
            _playButtonTexture = playButtonTexture;
            _exitButtonTexture = exitButtonTexture;
            _settingsButtonTexture = settingsButtonTexture;
        }

        public void LoadContent()
        {

            // Calculate positions to avoid overlap and maintain center alignment
            playButtonPosition = new Vector2(
                _game.GraphicsDevice.Viewport.Width / 2 - (_playButtonTexture.Width * playButtonScale) / 2,
                _game.GraphicsDevice.Viewport.Height / 2 - _playButtonTexture.Height * playButtonScale
            );
            exitButtonPosition = new Vector2(
                _game.GraphicsDevice.Viewport.Width / 2 - (_exitButtonTexture.Width * exitButtonScale) / 2,
                _game.GraphicsDevice.Viewport.Height / 2 + (_exitButtonTexture.Height * 0.1f)  // Adjust vertical spacing
            );
            settingsButtonPosition = new Vector2(
                _game.GraphicsDevice.Viewport.Width - _settingsButtonTexture.Width * settingsButtonScale,0
            );

        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            // Update the rectangles for button click detection
            Rectangle playButtonRectangle = new Rectangle(
                (int)playButtonPosition.X,
                (int)playButtonPosition.Y,
                (int)(_playButtonTexture.Width * playButtonScale),
                (int)(_playButtonTexture.Height * playButtonScale)
            );

            Rectangle exitButtonRectangle = new Rectangle(
                (int)exitButtonPosition.X,
                (int)exitButtonPosition.Y,
                (int)(_exitButtonTexture.Width * exitButtonScale),
                (int)(_exitButtonTexture.Height * exitButtonScale)
            );

            Rectangle settingsButtonRectangle = new Rectangle(
                (int)settingsButtonPosition.X,
                (int)settingsButtonPosition.Y,
                (int)(_settingsButtonTexture.Width * settingsButtonScale),
                (int)(_settingsButtonTexture.Height * settingsButtonScale)
);

            if (settingsButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _game.ShowControls();
            }

            // Check if the play button is clicked
            if (playButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _game.StartGame();
            }

            // Check if the exit button is clicked
            if (exitButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _game.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the play button with scaling
            spriteBatch.Draw(_playButtonTexture, playButtonPosition, null, Color.White, 0f, Vector2.Zero, playButtonScale, SpriteEffects.None, 0f);

            // Draw the exit button with scaling
            spriteBatch.Draw(_exitButtonTexture, exitButtonPosition, null, Color.White, 0f, Vector2.Zero, exitButtonScale, SpriteEffects.None, 0f);

            // Draw the exit button with scaling
            spriteBatch.Draw(_settingsButtonTexture, settingsButtonPosition, null, Color.White, 0f, Vector2.Zero, settingsButtonScale, SpriteEffects.None, 0f);



        }
    }
}
