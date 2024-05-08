using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static Pickup;


namespace Final1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private List<Enemy> _enemies;
        private Texture2D _enemyTexture;
        private TimeSpan _enemySpawnTime;
        private TimeSpan _previousSpawnTime;
        private Random _random;
        private StartScreen startScreen;
        private EndScreens endScreen;
        private GameState currentState;
        private GameState setState;
        private Texture2D playButtonTexture;
        private Texture2D exitButtonTexture;
        Texture2D laserTexture;
        List<Laser> laserBeams;
        TimeSpan laserSpawnTime;
        TimeSpan previousLaserSpawnTime;
        List<Explosion> explosions;
        Texture2D explosionTexture;
        Texture2D enemyLaserTexture;
        List<EnemyLaser> enemyLasers;


        private ParallaxBackground _sky;
        private ParallaxBackground _clouds1;
        private ParallaxBackground _clouds2;
        private ParallaxBackground _rocks;
        private ParallaxBackground _ground1;
        private ParallaxBackground _ground2;
        private ParallaxBackground _ground3;

        private Texture2D textureFullHealth;
        private Texture2D textureDamaged;
        private Texture2D textureSlightDamage;
        private Texture2D textureVeryDamaged;
        private Vector2 playerPosition;

        private Texture2D _numberSpritesheet;
        private Texture2D _healthBarSpritesheet;
        private Vector2 _healthBarPosition;
        private Vector2 _scorePosition;
        private int _score;

        private List<Pickup> pickups;
        private Texture2D healthPickupTexture;
        private Texture2D shieldPickupTexture;
        private Texture2D activeShieldTexture;
        private Texture2D shieldEffectTexture;

        private TimeSpan _pickupSpawnTime;
        private TimeSpan _previousPickupSpawnTime;
        private Animation shieldPickupAnimation;
        private Animation shieldEffectAnimation;

        private FinalBoss finalBoss;
        private Texture2D bossTexture;
        private Texture2D bossLaserTexture;
        private TimeSpan gameTimer;
        private bool bossSpawned;
        private TimeSpan lastEnemySpawnTime;
        private CustomDialog _controlsDialog;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            _pickupSpawnTime = TimeSpan.FromSeconds(10); // Adjust time as needed
            _previousPickupSpawnTime = TimeSpan.Zero;

            Window.AllowUserResizing = true;
            currentState = GameState.StartScreen;
        }

        protected override void Initialize()
        {
            
            
            _player = new Player();
            _enemies = new List<Enemy>();
            _previousSpawnTime = TimeSpan.Zero;
            _enemySpawnTime = TimeSpan.FromSeconds(1.5);
            _random = new Random();

            shieldEffectAnimation = new Animation();
            shieldPickupAnimation = new Animation();
            lastEnemySpawnTime = TimeSpan.Zero;

            gameTimer = TimeSpan.Zero;
            bossSpawned = false;
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D playButtonTexture = Content.Load<Texture2D>("Buttons/PlayButton");
            Texture2D exitButtonTexture = Content.Load<Texture2D>("Buttons/ExitButton");
            Texture2D settingsButtonTexture = Content.Load<Texture2D>("GUI/Settings");
            Texture2D restartButtonTexture = Content.Load<Texture2D>("Buttons/RestartButton");
            startScreen = new StartScreen(this, playButtonTexture, exitButtonTexture, settingsButtonTexture);
            endScreen = new EndScreens(this, restartButtonTexture, exitButtonTexture);
            

            Texture2D dialogBackgroundTexture = Content.Load<Texture2D>("Backgrounds/DialogBackground");
            SpriteFont dialogFont = Content.Load<SpriteFont>("Fonts/retro");

            // Initialize the dialog
            _controlsDialog = new CustomDialog(this, dialogBackgroundTexture, dialogFont, "\nW - Up\n\nD - Down\n\nA - Left\n\nD - Right\n\nSpace - Fire\n\n\nPress enter to close menu");


            if (startScreen == null)
                throw new InvalidOperationException("StartScreen is not initialized.");
            startScreen.LoadContent();
            

            _sky = new ParallaxBackground(Content, "Backgrounds/sky", -0.1f, GraphicsDevice.Viewport.Width);
            _clouds1 = new ParallaxBackground(Content, "Backgrounds/clouds_1", -0.2f, GraphicsDevice.Viewport.Width);
            _clouds2 = new ParallaxBackground(Content, "Backgrounds/clouds_2", -0.3f, GraphicsDevice.Viewport.Width);
            _rocks = new ParallaxBackground(Content, "Backgrounds/rocks", -0.4f, GraphicsDevice.Viewport.Width);
            _ground1 = new ParallaxBackground(Content, "Backgrounds/ground_1", -0.5f, GraphicsDevice.Viewport.Width);
            _ground2 = new ParallaxBackground(Content, "Backgrounds/ground_2", -0.6f, GraphicsDevice.Viewport.Width);
            _ground3 = new ParallaxBackground(Content, "Backgrounds/ground_3", -0.6f, GraphicsDevice.Viewport.Width);

            // Load player textures
            textureFullHealth = Content.Load<Texture2D>("Spaceship/FullHealth");
            textureDamaged = Content.Load<Texture2D>("Spaceship/Damaged");
            textureSlightDamage = Content.Load<Texture2D>("Spaceship/SlightDamage");
            textureVeryDamaged = Content.Load<Texture2D>("Spaceship/VeryDamaged");
            if (textureFullHealth == null || textureDamaged == null || textureSlightDamage == null || textureVeryDamaged == null)
            {
                throw new Exception("One or more textures failed to load.");
            }
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            _player.Initialize(textureFullHealth, textureDamaged, textureSlightDamage, textureVeryDamaged, playerPosition);

            // Load enemy texture
            _enemyTexture = Content.Load<Texture2D>("Enemy/Beholder");

            laserTexture = Content.Load<Texture2D>("Enemy/BeholderBullets");
            laserBeams = new List<Laser>();
            const float rateOfFire = 200f;
            laserSpawnTime = TimeSpan.FromSeconds(60f / rateOfFire);
            previousLaserSpawnTime = TimeSpan.Zero;

            explosionTexture = Content.Load<Texture2D>("Enemy/explosion_large");
            explosions = new List<Explosion>();

            enemyLaserTexture = Content.Load<Texture2D>("Enemy/EmmisaryBullets");
            enemyLasers = new List<EnemyLaser>();

            _numberSpritesheet = Content.Load<Texture2D>("GUI/NumbersSs");
            _healthBarSpritesheet = Content.Load<Texture2D>("GUI/HealthBar");
            _healthBarPosition = new Vector2(GraphicsDevice.Viewport.Width - 250, 10); // Adjust as needed
            _scorePosition = new Vector2(10, 10);
            _score = 0;

            shieldEffectTexture = Content.Load<Texture2D>("Effects/Shield");
            shieldPickupTexture = Content.Load<Texture2D>("Pickups/ShieldPickup");
            healthPickupTexture = Content.Load<Texture2D>("Pickups/HealthPickup");

             shieldEffectAnimation.Initialize(shieldEffectTexture, new Vector2(0, 0), shieldEffectTexture.Width / 12, shieldEffectTexture.Height, 12, 100, Color.White, 1f, true);
            shieldPickupAnimation.Initialize(shieldPickupTexture, new Vector2(0, 0), shieldPickupTexture.Width / 15, shieldPickupTexture.Height, 15, 100, Color.White, 1f, true);

            Texture2D bossTexture = Content.Load<Texture2D>("Boss/BossTexture");
            Texture2D bossLaserTexture = Content.Load<Texture2D>("Boss/BossLaser");

            // Initialize FinalBoss
            finalBoss = new FinalBoss(bossTexture, new Vector2(300, 200), bossLaserTexture);

            _player.InitializeShield(shieldEffectTexture);
            pickups = new List<Pickup>();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameTimer += gameTime.ElapsedGameTime;
            //startScreen.Update(gameTime);

            switch (currentState)
            {
                case GameState.StartScreen:
                    startScreen.Update(gameTime);
                    _controlsDialog.Update(gameTime);
                    break;
                case GameState.Playing:
                    // Existing game update logic
                    
                    _sky.Update(gameTime);
                    _clouds1.Update(gameTime);
                    _clouds2.Update(gameTime);
                    _rocks.Update(gameTime);
                    _ground1.Update(gameTime);
                    _ground2.Update(gameTime);
                    _ground3.Update(gameTime);

                  
                    _player.Update(gameTime);

                    if (gameTimer > TimeSpan.FromSeconds(10) && !bossSpawned) //change FromSeconds value to change the time it takes for the boss to spawn
                    {
                        _enemies.Clear(); // Clear existing enemies
                        
                        bossSpawned = true;
                    }

                    if (bossSpawned && finalBoss.Active)
                    {
                        finalBoss.Update(gameTime);
                    }
                    else
                    {
                        UpdateEnemies(gameTime);
                    }
                   

                    if (gameTime.TotalGameTime - _previousPickupSpawnTime > _pickupSpawnTime)
                    {
                        _previousPickupSpawnTime = gameTime.TotalGameTime;
                        SpawnPickup();
                    }

                    foreach (var pickup in pickups)
                    {
                        pickup.Update(gameTime);
                    }
                    pickups.RemoveAll(p => !p.Active);

                    foreach (var laser in laserBeams)
                    {
                        laser.Update(gameTime);
                    }
                    laserBeams.RemoveAll(l => !l.Active);

                    KeyboardState currentKeyboardState = Keyboard.GetState();
                    if (currentKeyboardState.IsKeyDown(Keys.Space))
                    {
                        FireLaser(gameTime);
                    }

                    foreach (var enemylaser in enemyLasers)
                    {
                        enemylaser.Update();
                    }
                    enemyLasers.RemoveAll(l => !l.Active);

                    UpdateCollision(gameTime);

                    foreach (var explosion in explosions)
                    {
                        explosion.Update(gameTime);
                    }
                    explosions.RemoveAll(e => !e.Active);

                    if (_player.Health <= 0)
                    {
                        currentState = GameState.GameOver;
                    }
                    if (shieldEffectAnimation.Active)
                    {
                        shieldEffectAnimation.Position = _player.Position; // Update position to follow player
                        shieldEffectAnimation.Update(gameTime);
                    }
                    if (bossSpawned && finalBoss.Active)
                    {
                        finalBoss.Update(gameTime);
                        // Spawn normal enemy every 5 seconds
                        if (finalBoss.Health <= 0)
                        {
                            finalBoss.Active = false; // Make sure to deactivate the boss
                            currentState = GameState.Win; // Transition to Win state
                        }
                    }
                    

                    break;
                    
                case GameState.Paused:
                    // Handle paused state if necessary
                    break;
                case GameState.GameOver:
                    UpdateGameOver(gameTime);
                    currentState = GameState.Restart;
                    break;
                case GameState.Win:
                    UpdateWin(gameTime);
                    currentState = GameState.Restart;
                    break;
                case GameState.Restart:
                    endScreen.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        TimeSpan enemyLaserSpawnTime = TimeSpan.FromSeconds(2);  // Enemies fire every 2 seconds
        private void UpdateEnemies(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - _previousSpawnTime > _enemySpawnTime)
            {
                _previousSpawnTime = gameTime.TotalGameTime;
                AddEnemy();
            }

            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime);
                if (gameTime.TotalGameTime - enemy.PreviousFireTime > enemyLaserSpawnTime)
                {
                    enemy.PreviousFireTime = gameTime.TotalGameTime;
                    FireEnemyLaser(enemy.Position);
                }

            }
            _enemies.RemoveAll(e => !e.Active);
        }
        private void FireEnemyLaser(Vector2 position)
        {
            EnemyLaser enemylaser = new EnemyLaser();
            enemylaser.Initialize(enemyLaserTexture, position);
            enemyLasers.Add(enemylaser);
        }

        private void AddEnemy()
        {
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + _enemyTexture.Width / 2, _random.Next(100, GraphicsDevice.Viewport.Height - 100));
            Enemy enemy = new Enemy();
            enemy.Initialize(_enemyTexture, position);
            _enemies.Add(enemy);
        }

        protected void AddExplosion(Vector2 position)
        {
            Console.WriteLine("Adding explosion at position: " + position);
            Animation explosionAnimation = new Animation();
            explosionAnimation.Initialize(explosionTexture, position, 512, 512, 64, 10, Color.White, 1f, false);
            Explosion explosion = new Explosion();
            explosion.Initialize(explosionAnimation, position);
            explosions.Add(explosion);

           
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            

            switch (currentState)
            {
                case GameState.StartScreen:
                    startScreen.Draw(_spriteBatch);
                    _controlsDialog.Draw(_spriteBatch);
                    break;
                case GameState.Playing:

                    _sky.Draw(_spriteBatch, GraphicsDevice);
                    _clouds1.Draw(_spriteBatch, GraphicsDevice);
                    _clouds2.Draw(_spriteBatch, GraphicsDevice);
                    _rocks.Draw(_spriteBatch, GraphicsDevice);
                    _ground1.Draw(_spriteBatch, GraphicsDevice);
                    _ground2.Draw(_spriteBatch, GraphicsDevice);
                    _ground3.Draw(_spriteBatch, GraphicsDevice);

                    DrawHealthBar(_spriteBatch, 5.0f); // Adjust scale as needed
                    DrawScore(_spriteBatch, 5.0f);
                   
                    _player.Draw(_spriteBatch);

                    if (bossSpawned && finalBoss.Active)
                    {
                        finalBoss.Draw(_spriteBatch);
                    }
                    else
                    {
                        foreach (var enemy in _enemies)
                        {
                            enemy.Draw(_spriteBatch);
                        }
                    }
                    foreach (var bosslaser in laserBeams)
                    {
                        bosslaser.Draw(_spriteBatch);
                    }

                    //foreach (var enemy in _enemies)
                    //{
                    //    enemy.Draw(_spriteBatch);
                    //}


                    foreach (var laser in laserBeams)
                    {
                        laser.Draw(_spriteBatch);
                    }

                    foreach (var explosion in explosions)
                    {
                        explosion.Draw(_spriteBatch);
                    }

                    foreach (var enemylaser in enemyLasers)
                    {

                        enemylaser.Draw(_spriteBatch);
                    }
                    foreach (var pickup in pickups)
                    {
                        pickup.Draw(_spriteBatch);
                    }
                    if (shieldEffectAnimation.Active)
                    {
                        shieldEffectAnimation.Draw(_spriteBatch);
                    }


                    // More drawing...
                    break;
                case GameState.Paused:
                    // Draw paused screen if necessary
                    break;
                case GameState.GameOver:
                    SpriteFont font = Content.Load<SpriteFont>("Fonts/PixelFont"); // Ensure you have this font
                    string gameOverText = "game over";
                    Vector2 textSize = font.MeasureString(gameOverText);
                    Vector2 textPosition = new Vector2((GraphicsDevice.Viewport.Width - textSize.X) / 2, GraphicsDevice.Viewport.Height / 5 - 100);
                    _spriteBatch.DrawString(font, gameOverText, textPosition, Color.White);

                    // Draw score
                    string scoreText = "score: " + _score;
                    DrawScoreUsingSpritesheet(_spriteBatch, _score, new Vector2(textPosition.X, textPosition.Y + 100));

                    
                    // endScreen.Draw(_spriteBatch);
                    currentState = GameState.Restart;
                    endScreen.Draw(_spriteBatch);

                    break;
                case GameState.Win:

                    SpriteFont fontW = Content.Load<SpriteFont>("Fonts/PixelFont");
                    string winText = "finished";
                    Vector2 textSizeW = fontW.MeasureString(winText);
                    Vector2 textPositionW = new Vector2((GraphicsDevice.Viewport.Width - textSizeW.X) / 2, GraphicsDevice.Viewport.Height / 5 - 100);
                    _spriteBatch.DrawString(fontW, winText, textPositionW, Color.White);

                    // Draw score in a similar way to the game over screen
                    string scoreTextW = "score: " + _score;
                    Vector2 scorePosition = new Vector2(textPositionW.X, textPositionW.Y + 500);
                    DrawScoreUsingSpritesheet(_spriteBatch, _score, scorePosition);
                    //endScreen.Draw(_spriteBatch);

                    currentState = GameState.Restart;
                    break;
                case GameState.Restart:
                    endScreen.Draw(_spriteBatch);
                    break;
            }



            _spriteBatch.End();

            base.Draw(gameTime);

        }

        private void UpdateCollision(GameTime gameTime)
        {
            // Adjust the player rectangle for scaling
            Rectangle playerRectangle = new Rectangle(
                (int)(_player.Position.X - _player.Width * _player.Scale / 2),
                (int)(_player.Position.Y - _player.Height * _player.Scale / 2),
                (int)(_player.Width * _player.Scale),
                (int)(_player.Height * _player.Scale));

            for (int i = pickups.Count - 1; i >= 0; i--)
            {
                Rectangle pickupRectangle = new Rectangle(
                    (int)pickups[i].Position.X,
                    (int)pickups[i].Position.Y,
                    (int)(pickups[i].Texture.Width * pickups[i].Scale),
                    (int)(pickups[i].Texture.Height * pickups[i].Scale));

                if (playerRectangle.Intersects(pickupRectangle))
                {
                    // Handle the pickup effect based on its type
                    HandlePickup(pickups[i]);
                    pickups[i].Active = false;
                }
            }

            pickups.RemoveAll(p => !p.Active);

            // Check collision with each enemy
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                // Adjust the enemy rectangle for scaling
                Rectangle enemyRectangle = new Rectangle(
                    (int)(_enemies[i].Position.X - _enemies[i].Width * 0.5f),
                    (int)(_enemies[i].Position.Y - _enemies[i].Height * 0.5f),
                    _enemies[i].Width,
                    _enemies[i].Height);


                // Check if the player's rectangle intersects with the enemy's rectangle
                if (playerRectangle.Intersects(enemyRectangle) && !_player.shieldActive)
                {
                    // Reduce player health by the damage value of the enemy
                    _player.Health -= _enemies[i].Damage;

                    AddExplosion(_enemies[i].Position);
              
                    // Destroy the enemy
                    _enemies[i].Active = false;

                    // Remove the enemy from the list
                    _enemies.RemoveAt(i);

                    // Check if the player's health is less than or equal to zero
                    if (_player.Health <= 0)
                    {
                        _player.Active = false;
                        
                    }
                }
                
                
            }

            // Check collision for each laser
            foreach (var laser in laserBeams)
            {
                Rectangle laserRectangle = new Rectangle(
                    (int)laser.Position.X,
                    (int)laser.Position.Y,
                    laser.Width,
                    laser.Height);

                for (int i = _enemies.Count - 1; i >= 0; i--)
                {
                    Rectangle enemyRectangle = new Rectangle(
                        (int)(_enemies[i].Position.X - _enemies[i].Width * 0.5f),
                        (int)(_enemies[i].Position.Y - _enemies[i].Height * 0.5f),
                        _enemies[i].Width,
                        _enemies[i].Height);

                    if (laserRectangle.Intersects(enemyRectangle))
                    {
                        
                        _enemies[i].Health = 0; // Enemy is destroyed
                        laser.Active = false; // Laser is destroyed

                        AddExplosion(_enemies[i].Position);
                        _score += 5;
                        _enemies.RemoveAt(i);
                        break;
                    }
                }
               
            }
            // Check collision for each enemy laser with the player
            foreach (var enemylaser in enemyLasers)
            {
                Rectangle laserRectangle = new Rectangle(
                    (int)enemylaser.Position.X,
                    (int)enemylaser.Position.Y,
                    enemylaser.Width,
                    enemylaser.Height);

                if (laserRectangle.Intersects(playerRectangle) && !_player.shieldActive) // Check if shield is active
                {
                    _player.Health -= 10; // Apply damage only if shield is not active
                    enemylaser.Active = false;
                }
            }
            // Ensure that inactive lasers are removed
            enemyLasers.RemoveAll(l => !l.Active);

            if (finalBoss != null && finalBoss.Active)
            {
                foreach (var laser in laserBeams)
                {
                    Rectangle laserRectangle = new Rectangle(
                        (int)laser.Position.X,
                        (int)laser.Position.Y,
                        laser.Width,
                        laser.Height);

                    Rectangle bossRectangle = new Rectangle(
                        (int)finalBoss.Position.X - finalBoss.Width / 2,
                        (int)finalBoss.Position.Y - finalBoss.Height / 2,
                        finalBoss.Width,
                        finalBoss.Height);

                    if (laserRectangle.Intersects(bossRectangle))
                    {
                        finalBoss.Health -= 10; // Adjust damage as needed
                        laser.Active = false; // Deactivate the laser after hitting the boss
                    }
                }
                laserBeams.RemoveAll(l => !l.Active);
            }

            if (finalBoss != null && finalBoss.Active && finalBoss.BossLasers != null)
            {
                foreach (var bosslaser in finalBoss.BossLasers)
                {
                    Rectangle bosslaserRectangle = new Rectangle(
                        (int)bosslaser.Position.X,
                        (int)bosslaser.Position.Y,
                        bosslaser.Width,
                        bosslaser.Height);

                    if (bosslaserRectangle.Intersects(playerRectangle) && !_player.shieldActive)
                    {
                        _player.Health -= finalBoss.Damage; // Apply damage to player
                        bosslaser.Active = false; // Deactivate laser
                    }


                }
                finalBoss.BossLasers.RemoveAll(l => !l.Active);
            }


        }

        protected void FireLaser(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousLaserSpawnTime > laserSpawnTime)
            {
                previousLaserSpawnTime = gameTime.TotalGameTime;
                AddLaser();
            }
        }

        protected void AddLaser()
        {
            Vector2 laserPosition = new Vector2(_player.Position.X + 30, _player.Position.Y);
            Laser laser = new Laser();
            laser.Initialize(laserTexture, laserPosition);
            laserBeams.Add(laser);
        }
        public enum GameState
        {
            StartScreen,
            Playing,
            Paused,
            GameOver,
            Win,
            Restart
            
        }

        public void StartGame()
        {
            currentState = GameState.Playing;
            _score = 0;
            // Reset or initialize game components as needed
            _player.Initialize(textureFullHealth, textureDamaged, textureSlightDamage, textureVeryDamaged, playerPosition);
            
            _enemies.Clear(); // Clear existing enemies
            
            _previousSpawnTime = TimeSpan.Zero; // Reset enemy spawn timing
                                               
        }
        public void restartGame()
        {
            currentState = GameState.Playing;
            _score = 0;
            // Reset or initialize game components as needed
            _player.Initialize(textureFullHealth, textureDamaged, textureSlightDamage, textureVeryDamaged, playerPosition);
           
            _enemies.Clear(); // Clear existing enemies
            _previousSpawnTime = TimeSpan.Zero; // Reset enemy spawn timing

        }
        public void ShowControls()
        {
            _controlsDialog.Show();
        }




        public void DrawHealthBar(SpriteBatch spriteBatch, float scale)
        {
            // Assuming there are 5 sprites in the single row for different health states
            int totalSprites = 5;
            int healthPerSprite = 100 / totalSprites; // Assuming max health is 100
            int healthIndex = _player.Health / healthPerSprite;

            // Clamp the index to be within the range of available sprites
            healthIndex = Math.Clamp(healthIndex, 0, totalSprites - 1);

            // Calculate the source rectangle for the current health state
            int spriteHeight = _healthBarSpritesheet.Height; // Only one row
            int spriteWidth = _healthBarSpritesheet.Width / totalSprites; // Number of sprites per row
            Rectangle sourceRectangle = new Rectangle(spriteWidth * (totalSprites - 1 - healthIndex), 0, spriteWidth, spriteHeight);

            // Draw the health bar at the specified position with scaling
            Vector2 scaleVector = new Vector2(scale, scale);
            spriteBatch.Draw(_healthBarSpritesheet, _healthBarPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, scaleVector, SpriteEffects.None, 0f);
        }

        public void DrawScore(SpriteBatch spriteBatch, float scale)
        {
            string scoreText = _score.ToString();
            int numberWidth = _numberSpritesheet.Width / 10;
            int numberHeight = _numberSpritesheet.Height;
            Vector2 scaleVector = new Vector2(scale, scale);

            for (int i = 0; i < scoreText.Length; i++)
            {
                int number = int.Parse(scoreText[i].ToString());
                Rectangle sourceRectangle = new Rectangle(numberWidth * number, 0, numberWidth, numberHeight);
                Vector2 position = new Vector2(_scorePosition.X + i * numberWidth * scale, _scorePosition.Y);
                spriteBatch.Draw(_numberSpritesheet, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scaleVector, SpriteEffects.None, 0f);
            }
        }

        private void UpdateGameOver(GameTime gameTime)
        {
            // use the logic from endScreen to handle button clicks
            endScreen.Update(gameTime);
        }

        private void UpdateWin(GameTime gameTime)
        {
            endScreen.Update(gameTime);
        }

        private void DrawScoreUsingSpritesheet(SpriteBatch spriteBatch, int score, Vector2 position)
        {
            string scoreText = score.ToString();
            int numberWidth = _numberSpritesheet.Width / 10; // Assuming there are 10 digits (0-9) in the spritesheet
            int numberHeight = _numberSpritesheet.Height;
            float scale = 3.0f; // Adjust scale as needed

            for (int i = 0; i < scoreText.Length; i++)
            {
                int number = int.Parse(scoreText[i].ToString());
                Rectangle sourceRectangle = new Rectangle(numberWidth * number, 0, numberWidth, numberHeight);
                Vector2 digitPosition = new Vector2(position.X + i * numberWidth * scale, position.Y);
                spriteBatch.Draw(_numberSpritesheet, digitPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        private void HandlePickup(Pickup pickup)
        {
            switch (pickup.Type)
            {
                case PickupType.Health:
                    _player.Health += 30; // Adjust value as needed
                    break;
                case PickupType.Shield:
                    _player.ActivateShield(10); // Activate shield for 10 seconds
                    shieldEffectAnimation.Position = _player.Position; // Center on the player
                    shieldEffectAnimation.Active = true; // Activate the animation
                    break;
            }
        }


        private void SpawnPickup()
        {
            int margin = 100; // Margin to ensure pickups spawn within visible range
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + margin, _random.Next(margin, GraphicsDevice.Viewport.Height - margin));
            Vector2 velocity = new Vector2(-100, 0); // Move left at a constant speed
            Pickup.PickupType type = (Pickup.PickupType)_random.Next(0, 2); // 0 for Health, 1 for Shield
            Texture2D texture = type == Pickup.PickupType.Health ? healthPickupTexture : shieldPickupTexture;

            Pickup newPickup = new Pickup(texture, position, type, velocity);
            pickups.Add(newPickup);
        }
        private void DrawWinningScreen(SpriteBatch spriteBatch)
        {
            

            
        }


    }
}
