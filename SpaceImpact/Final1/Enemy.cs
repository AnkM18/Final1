using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final1
{
    public class Enemy
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public bool Active { get; set; }
        public float Width => Texture.Width;
        public float Height => Texture.Height;
        public TimeSpan PreviousFireTime { get; set; }

        public Enemy()
        {
            Active = true;
            Health = 100; // Default health
            Damage = 10; // Default damage
            Speed = 2f; // Default speed
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            // Update enemy position or behavior here
            Position.X -= Speed; // Move left
            if (Position.X < -Width)
            {
                Active = false; // Deactivate if off-screen
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }
    }
}