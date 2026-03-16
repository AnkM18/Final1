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
       
        private float restartButtonScale = 8.0f;

        public EndScreens(Game1 game, Texture2D restartButtonTexture, Texture2D exitButtonTexture)
        {
            _game = game;
            _exitButtonTexture = exitButtonTexture;
            _restartButtonTexture = restartButtonTexture;
        }

        public void LoadContent()
        {

            // Get screen size
            int screenWidth = 1920;
            int screenHeight = 1080;

            // Calculate center position
            int centerX = screenWidth / 2;
            int centerY = screenHeight / 2;

            // Set button positions
            restartButtonPosition = new Vector2(centerX - 100, centerY - 50);
            exitButtonPosition = new Vector2(centerX + 100, centerY - 50);

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
