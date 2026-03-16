// This file defines the Player class, which handles player properties, movement, and interactions.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final1
{
    public class Player
    {
        private Texture2D _texture;
        private Vector2 _position;
        private float _speed;
        private int _health;
        private bool _shieldActive;
        private float _shieldDuration;
        private float _shieldTimer;

        public Vector2 Position => _position;
        public int Health => _health;
        public bool ShieldActive => _shieldActive;

        public Player()
        {
            _speed = 5f;
            _health = 100; // Starting health
            _shieldActive = false;
            _shieldDuration = 0;
            _shieldTimer = 0;
        }

        public void Initialize(Texture2D texture, Vector2 startPosition)
        {
            _texture = texture;
            _position = startPosition;
        }

        public void Update(GameTime gameTime)
        {
            HandleInput();

            if (_shieldActive)
            {
                _shieldTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_shieldTimer >= _shieldDuration)
                {
                    _shieldActive = false;
                    _shieldTimer = 0;
                }
            }
        }

        private void HandleInput()
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W))
                _position.Y -= _speed;
            if (state.IsKeyDown(Keys.S))
                _position.Y += _speed;
            if (state.IsKeyDown(Keys.A))
                _position.X -= _speed;
            if (state.IsKeyDown(Keys.D))
                _position.X += _speed;

            // Keep player within screen bounds
            _position.X = MathHelper.Clamp(_position.X, 0, 1920 - _texture.Width);
            _position.Y = MathHelper.Clamp(_position.Y, 0, 1080 - _texture.Height);
        }

        public void ActivateShield(float duration)
        {
            _shieldActive = true;
            _shieldDuration = duration;
            _shieldTimer = 0;
        }

        public void TakeDamage(int damage)
        {
            if (!_shieldActive)
            {
                _health -= damage;
                if (_health < 0) _health = 0; // Prevent negative health
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}