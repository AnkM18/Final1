using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ParallaxBackground
{
    private Texture2D texture;
    private Vector2 position;
    private float speed;
    private float viewportWidth;

    public ParallaxBackground(Texture2D texture, float speed, float viewportWidth)
    {
        this.texture = texture;
        this.speed = speed;
        this.viewportWidth = viewportWidth;
        this.position = Vector2.Zero;
    }

    public void Update(GameTime gameTime)
    {
        position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (position.X <= -viewportWidth)
        {
            position.X = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
        spriteBatch.Draw(texture, new Vector2(position.X + viewportWidth, position.Y), Color.White);
    }
}