
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Final1;
using System;
public class Enemy
{
    // Texture representing the enemy
    public Texture2D Texture;

    // The position of the enemy ship relative to the top left corner of the screen
    public Vector2 Position;

    // The state of the Enemy Ship
    public bool Active;

    // The hit points of the enemy, if this goes to zero the enemy dies
    public int Health;

    // The amount of damage the enemy inflicts on the player ship
    public int Damage;

    // The amount of score the enemy will give to the player
    public int Value;

    // Get the width of the enemy ship
    public int Width { get { return (int)(Texture.Width * Scale); } }

    // Get the height of the enemy ship
    public int Height { get { return (int)(Texture.Height * Scale); } }

    private float Scale = 2.0f;

    // The speed at which the enemy moves
    private float enemyMoveSpeed;

    public TimeSpan PreviousFireTime { get; set; }



    public void Initialize(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        Position = position;
        Active = true;
        Health = 10;
        Damage = 20;
        enemyMoveSpeed = 6f;
        Value = 100;
        
    }

    public void Update(GameTime gameTime)
    {
        // The enemy always moves to the left so decrement its x position
        Position.X -= enemyMoveSpeed;

        // If the enemy is past the screen or its health reaches 0 then deactivate it
        if (Position.X < -Width || Health <= 0)
        {
            Active = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {

        if (Active)
        {
            // Calculate the center of the enemy for proper scaling and rotation
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            // Draw the texture with scaling
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, origin, Scale, SpriteEffects.None, 0f);
        }
    }

}
