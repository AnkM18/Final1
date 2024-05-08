using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Final1
{
    public class ParallaxBackground
    {
    public Texture2D Texture;
    public float ScrollSpeed;

    private Vector2 _position1, _position2;
    private int _screenWidth;

    public ParallaxBackground(ContentManager content, string texturePath, float scrollSpeed, int screenWidth)
    {
        Texture = content.Load<Texture2D>(texturePath);
        ScrollSpeed = scrollSpeed;
        _screenWidth = screenWidth;
        _position1 = Vector2.Zero;
        _position2 = new Vector2(screenWidth, 0);
    }

    public void Update(GameTime gameTime)
    {
        // Move the backgrounds
        _position1.X += ScrollSpeed;
        _position2.X += ScrollSpeed;

        // If a background has moved entirely off-screen, reset its position
        if (_position1.X <= -_screenWidth)
            _position1.X = _position2.X + _screenWidth;
        if (_position2.X <= -_screenWidth)
            _position2.X = _position1.X + _screenWidth;
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {

    float scale = GetScale(graphicsDevice);

    spriteBatch.Draw(Texture, _position1, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    spriteBatch.Draw(Texture, _position2, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    
    }
    
    public float GetScale(GraphicsDevice graphicsDevice)
    {
    return (float)graphicsDevice.Viewport.Width / Texture.Width;
    }
    }
}
