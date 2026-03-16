using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final1
{
    public class Explosion
    {
        public Animation Animation { get; private set; }
        public Vector2 Position { get; set; }
        public bool Active { get; set; }

        public Explosion()
        {
            Active = false;
        }

        public void Initialize(Animation animation, Vector2 position)
        {
            Animation = animation;
            Position = position;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (Animation.Active)
            {
                Animation.Update(gameTime);
            }
            else
            {
                Active = false; // Deactivate explosion when animation is complete
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Animation.Position = Position; // Set position for the animation
                Animation.Draw(spriteBatch);
            }
        }
    }
}