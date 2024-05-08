using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class FinalBoss
{
    public Texture2D Texture;
    public Vector2 Position;
    public bool Active;
    public int Health;
    public int Damage;
    private float verticalMoveSpeed;
    private bool movingDown = true;
    private TimeSpan laserFireRate;
    private TimeSpan lastLaserTime;
    private Texture2D laserTexture;
    public List<EnemyLaser> BossLasers;
    private int screenWidth;
    private int screenHeight;

    public FinalBoss(Texture2D texture, Vector2 position, Texture2D laserTexture)
    {
        Texture = texture;
        Position = new Vector2(1920 - texture.Width, position.Y);
        Active = true;
        Health = 20; // More health than a regular enemy
        Damage = 10;  // Same damage as regular enemy
        verticalMoveSpeed = 2.0f;
        laserFireRate = TimeSpan.FromSeconds(1);
        lastLaserTime = TimeSpan.Zero;
        this.laserTexture = laserTexture;
        BossLasers = new List<EnemyLaser>();
        this.screenWidth = 1920;
        this.screenHeight = 1080;
        
    }

    public void Update(GameTime gameTime)
    {
        // Move vertically
        if (movingDown)
        {
            Position.Y += verticalMoveSpeed;
            if (Position.Y > screenHeight - Height)
                movingDown = false;
        }
        else
        {
            Position.Y -= verticalMoveSpeed;
            if (Position.Y < 0)
                movingDown = true;
        }

        // Fire lasers
        if (gameTime.TotalGameTime - lastLaserTime > laserFireRate)
        {
            FireLaser();
            lastLaserTime = gameTime.TotalGameTime;
        }

        // Update lasers
        foreach (var laser in BossLasers)
        {
            laser.Update();
        }
        BossLasers.RemoveAll(laser => !laser.Active);

        // Check for deactivation
        if (Health <= 0)
        {
            Active = false;
        }
    }

    private void FireLaser()
    {
        Vector2 laserPosition = new Vector2(Position.X, Position.Y + Texture.Height / 2);
        EnemyLaser laser = new EnemyLaser();
        laser.Initialize(laserTexture, laserPosition); // Move left
        BossLasers.Add(laser);

       
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Active)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            foreach (var laser in BossLasers)
            {
                laser.Draw(spriteBatch);
            }
        }
    }

    public int Width
    {
        get { return Texture.Width; }
    }

    public int Height
    {
        get { return Texture.Height; }
    }
}
