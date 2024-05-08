using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Final1;

public class EnemyLaser
{
    public Texture2D Texture { get; private set; }
    public Vector2 Position { get; set; }
    public bool Active { get; set; }
    public int Width => Texture.Width;
    public int Height => Texture.Height;

    public void Initialize(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        Position = position;
        Active = true;
    }

    public void Update()
    {
        // Move the laser downwards
        Position = new Vector2(Position.X - 10, Position.Y);
        if (Position.Y > 1920) // Assuming 1080p resolution
            Active = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }
}
