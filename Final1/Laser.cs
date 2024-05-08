using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Laser
{
    // Texture for the laser
    public Texture2D Texture;
   
    public Vector2 Position;
    public float LaserMoveSpeed = 30f;
    public int Damage = 10;
    public bool Active;
    public int Range;

    // Properties to get the width and height of the laser texture
    public int Width => Texture.Width;
    public int Height => Texture.Height;

    public void Initialize(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        Position = position;
        Active = true;
    }

    public void Update(GameTime gameTime)
    {
        // Update the position of the laser
        Position.X += LaserMoveSpeed;

        // Deactivate the laser if it goes off screen
        if (Position.X > 1920) // Assuming 1920 is the width of the screen
            Active = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Active)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
