using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final1
{
    public class EndScreens
    {
        
        private Texture2D _exitButtonTexture;
        private Texture2D _restartButtonTexture;
        
        private Vector2 exitButtonPosition;
        private Vector2 restartButtonPosition;

        private Game1 _game;
        
        private float exitButtonScale = 5.0f;
       
        private float restartButtonScale = 5.0f;

        public EndScreens(Game1 game, Texture2D restartButtonTexture, Texture2D exitButtonTexture)
        {
            _game = game;
            _exitButtonTexture = exitButtonTexture;
            _restartButtonTexture = restartButtonTexture;
        }

        public void LoadContent()
        {

            // Calculate positions to avoid overlap and maintain center alignment

            // Centralize restart button
            /* restartButtonPosition = new Vector2(
                  _game.GraphicsDevice.Viewport.Width / 2 - (_restartButtonTexture.Width * restartButtonScale) / 2,
                  _game.GraphicsDevice.Viewport.Height / 2 - _restartButtonTexture.Height * restartButtonScale / 2
              );
             exitButtonPosition = new Vector2(
                 _game.GraphicsDevice.Viewport.Width / 2 - (_exitButtonTexture.Width * exitButtonScale) / 2,
                 _game.GraphicsDevice.Viewport.Height / 2 + (_exitButtonTexture.Height * 0.5f)  // Adjust vertical spacing
             );*/

            float buttonSpacing = 20; // Space between buttons

            // Centralize restart button
            restartButtonPosition = new Vector2(
                960 - (_restartButtonTexture.Width * restartButtonScale) / 2, // 1920 / 2 = 960
                540 - (_restartButtonTexture.Height * restartButtonScale) / 2 - buttonSpacing // 1080 / 2 = 540
            );

            // Centralize exit button, positioned below the restart button
            exitButtonPosition = new Vector2(
                960 - (_exitButtonTexture.Width * exitButtonScale) / 2, // 1920 / 2 = 960
                540 + (_exitButtonTexture.Height * exitButtonScale) / 2 + buttonSpacing // 1080 / 2 = 540
            );


        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            // Update the rectangles for button click detection
            Rectangle restartButtonRectangle = new Rectangle(
                (int)restartButtonPosition.X,
                (int)restartButtonPosition.Y,
                (int)(_restartButtonTexture.Width * restartButtonScale),
                (int)(_restartButtonTexture.Height * restartButtonScale)
            );

            Rectangle exitButtonRectangle = new Rectangle(
                (int)exitButtonPosition.X,
                (int)exitButtonPosition.Y,
                (int)(_exitButtonTexture.Width * exitButtonScale),
                (int)(_exitButtonTexture.Height * exitButtonScale)
            );


            // Check if the play button is clicked
            if (restartButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _game.restartGame();
            }

            // Check if the exit button is clicked
            if (exitButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _game.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the restart button with scaling
            spriteBatch.Draw(_restartButtonTexture, restartButtonPosition, null, Color.White, 0f, Vector2.Zero, restartButtonScale, SpriteEffects.None, 0f);

            // Draw the exit button with scaling
            spriteBatch.Draw(_exitButtonTexture, exitButtonPosition, null, Color.White, 0f, Vector2.Zero, exitButtonScale, SpriteEffects.None, 0f);





        }
    }
}
