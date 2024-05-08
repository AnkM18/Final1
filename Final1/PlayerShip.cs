using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Timers;

namespace Final1
{
    class Player
    {
        // Textures representing the player states
        public Texture2D TextureFullHealth;
        public Texture2D TextureDamaged;
        public Texture2D TextureSlightDamage;
        public Texture2D TextureVeryDamaged;
        public Texture2D ShieldTexture; // Texture for the active shield effect
        public bool shieldActive{ get; private set; }
        private TimeSpan shieldDuration;
        private TimeSpan shieldStartTime;
       

        // Current texture based on health
        private Texture2D currentTexture;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;

        // State of the player
        public bool Active;

        // Amount of hit points that player has
        private int health;
        public float Speed;
        public float Scale = 3.0f;
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                UpdateTextureBasedOnHealth();
            }
        }

        // Get the width of the player ship
        public int Width { get { return currentTexture.Width; } }

        // Get the height of the player ship
        public int Height { get { return currentTexture.Height; } }

        public void Initialize(Texture2D textureFullHealth, Texture2D textureDamaged, Texture2D textureSlightDamage, Texture2D textureVeryDamaged, Vector2 position)
        {
            TextureFullHealth = textureFullHealth;
            TextureDamaged = textureDamaged;
            TextureSlightDamage = textureSlightDamage;
            TextureVeryDamaged = textureVeryDamaged;

            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;

            Speed = 500f;

            // Initialize texture based on full health
            currentTexture = TextureFullHealth;
        }

        private void UpdateTextureBasedOnHealth()
        {
            if (Health > 75)
                currentTexture = TextureFullHealth;
            else if (Health > 50)
                currentTexture = TextureSlightDamage;
            else if (Health > 25)
                currentTexture = TextureDamaged;
            else
                currentTexture = TextureVeryDamaged;
        }

        public void InitializeShield(Texture2D shieldTexture)
        {
            ShieldTexture = shieldTexture;
            shieldActive = false;
        }

        public void ActivateShield(int durationInSeconds)
        {
            shieldActive = true;
            shieldDuration = TimeSpan.FromSeconds(durationInSeconds);
            shieldStartTime = TimeSpan.MaxValue; // Set to a large value initially
        }

        public void Update(GameTime gameTime)
        {
            // Update player state here (e.g., check for damage)
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W))
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (state.IsKeyDown(Keys.S))
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (state.IsKeyDown(Keys.A))
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (state.IsKeyDown(Keys.D))
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update shield status
            if (shieldActive)
            {
                if (shieldStartTime == TimeSpan.MaxValue)
                {
                    shieldStartTime = gameTime.TotalGameTime; // Set start time when shield is first activated
                }
                else if (gameTime.TotalGameTime - shieldStartTime > shieldDuration)
                {
                    shieldActive = false;
                    shieldStartTime = TimeSpan.MaxValue; // Reset start time
                }
            }

        }

        public void TakeDamage(int damage)
        {
            if (!shieldActive)
            {
                Health -= damage;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(Width / 2, Height / 2);
            Vector2 drawPosition = new Vector2(Position.X - Width / 2, Position.Y - Height / 2);
            spriteBatch.Draw(currentTexture, Position, null, Color.White, MathHelper.PiOver2, origin, Scale, SpriteEffects.None, 0f);

            // Draw the shield if active
            if (shieldActive)
            {
                spriteBatch.Draw(ShieldTexture, Position, null, Color.White * 0.5f, 0f, new Vector2(ShieldTexture.Width / 2, ShieldTexture.Height / 2), Scale, SpriteEffects.None, 0f);
            }
        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X - (int)(Width * Scale / 2),
                    (int)Position.Y - (int)(Height * Scale / 2),
                    (int)(Width * Scale),
                    (int)(Height * Scale)
                );
            }
        }
    }
}
