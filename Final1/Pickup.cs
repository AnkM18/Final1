using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Final1;
public class Pickup
{
    public enum PickupType
    {
        Health,
        Shield
    }

    public Texture2D Texture { get; private set; }
    public Vector2 Position { get; private set; }
    public Vector2 Velocity { get; private set; }
    public bool Active { get; set; }

    public PickupType Type { get; private set; }
    public float Scale { get; set; } = 2.0f;
    public Rectangle BoundingBox => new Rectangle(
        (int)Position.X,
        (int)Position.Y,
        (int)(Texture.Width * Scale),
        (int)(Texture.Height * Scale));


    public Pickup(Texture2D texture, Vector2 position, PickupType type, Vector2 velocity)
    {
        Texture = texture;
        Position = position;
        Type = type;
        Velocity = velocity;
        Active = true;
    }

    public void Update(GameTime gameTime)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        // Deactivate the pickup if it moves off the left side of the screen
        if (Position.X < -Texture.Width)
        {
            Active = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Active)
        {

            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
