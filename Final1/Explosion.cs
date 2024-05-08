using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final1
{
    public class Explosion
    {
        Animation explosionAnimation;
        Vector2 Position;
        public bool Active;
        int timeToLive;

        public int Width => explosionAnimation.FrameWidth;
        public int Height => explosionAnimation.FrameHeight;

        public void Initialize(Animation animation, Vector2 position)
        {
            explosionAnimation = animation;
            Position = position;
            Active = true;
            timeToLive = 100; // Duration of the explosion in frames
        }

        public void Update(GameTime gameTime)
        {
            explosionAnimation.Update(gameTime);
            timeToLive -= 1;
            if (timeToLive <= 0)
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                explosionAnimation.Draw(spriteBatch);
            }
        }
    }
}
