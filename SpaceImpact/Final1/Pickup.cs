using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Pickup
{
    public enum PickupType
    {
        Health,
        Shield
    }

    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public bool Active { get; set; }
    public PickupType Type { get; set; }
    public float Scale { get; set; }

    public Pickup(Texture2D texture, Vector2 position, PickupType type, Vector2 velocity)
    {
        Texture = texture;
        Position = position;
        Type = type;
        Velocity = velocity;
        Active = true;
        Scale = 1.0f; // Default scale
    }

    public void Update(GameTime gameTime)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Deactivate if it goes off-screen
        if (Position.X < -Texture.Width || Position.Y < -Texture.Height || Position.Y > GraphicsDevice.Viewport.Height)
        {
            Active = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Active)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
        }
    }
}