using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Laser
{
    public Texture2D Texture { get; private set; }
    public Vector2 Position { get; set; }
    public bool Active { get; set; }
    public float Speed { get; set; }
    public int Width => Texture.Width;
    public int Height => Texture.Height;

    public Laser()
    {
        Active = false;
    }

    public void Initialize(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        Position = position;
        Active = true;
        Speed = 10f; // Adjust speed as needed
    }

    public void Update()
    {
        Position.X += Speed;

        // Deactivate the laser if it goes off-screen
        if (Position.X > 1920) // Assuming 1920 is the screen width
        {
            Active = false;
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