using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Final1
{
    public class FinalBoss
    {
        private Texture2D _texture;
        private Vector2 _position;
        private int _health;
        private int _damage;
        private bool _active;
        private List<EnemyLaser> _bossLasers;
        private TimeSpan _laserSpawnTime;
        private TimeSpan _previousLaserSpawnTime;

        public FinalBoss(Texture2D texture, Vector2 position, Texture2D laserTexture)
        {
            _texture = texture;
            _position = position;
            _health = 100; // Set initial health
            _damage = 10; // Set damage value
            _active = true;
            _bossLasers = new List<EnemyLaser>();
            _laserSpawnTime = TimeSpan.FromSeconds(2); // Time between laser shots
            _previousLaserSpawnTime = TimeSpan.Zero;
        }

        public bool Active => _active;
        public int Health => _health;
        public int Damage => _damage;
        public List<EnemyLaser> BossLasers => _bossLasers;

        public void Update(GameTime gameTime)
        {
            if (_active)
            {
                // Logic for boss movement or behavior can be added here

                // Handle laser firing
                if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
                {
                    _previousLaserSpawnTime = gameTime.TotalGameTime;
                    FireLaser();
                }
            }
        }

        private void FireLaser()
        {
            EnemyLaser laser = new EnemyLaser();
            laser.Initialize(_texture, _position); // Assuming the laser uses the boss texture for simplicity
            _bossLasers.Add(laser);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_active)
            {
                spriteBatch.Draw(_texture, _position, Color.White);
                foreach (var laser in _bossLasers)
                {
                    laser.Draw(spriteBatch);
                }
            }
        }
    }
}