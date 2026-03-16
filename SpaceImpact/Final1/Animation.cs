using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation
{
    public Texture2D Texture { get; private set; }
    public Vector2 Position { get; set; }
    public int FrameWidth { get; private set; }
    public int FrameHeight { get; private set; }
    public int FrameCount { get; private set; }
    public int CurrentFrame { get; private set; }
    public float FrameTime { get; private set; }
    public float TotalElapsed { get; private set; }
    public Color Color { get; set; }
    public float Scale { get; set; }
    public bool Active { get; set; }
    public bool Loop { get; set; }

    public Animation()
    {
        Active = true;
        Loop = true;
        Color = Color.White;
        Scale = 1f;
    }

    public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, float frameTime, Color color, float scale, bool loop)
    {
        Texture = texture;
        Position = position;
        FrameWidth = frameWidth;
        FrameHeight = frameHeight;
        FrameCount = frameCount;
        FrameTime = frameTime;
        Color = color;
        Scale = scale;
        Loop = loop;
        CurrentFrame = 0;
        TotalElapsed = 0f;
    }

    public void Update(GameTime gameTime)
    {
        if (!Active) return;

        TotalElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (TotalElapsed >= FrameTime)
        {
            TotalElapsed -= FrameTime;
            CurrentFrame++;

            if (CurrentFrame >= FrameCount)
            {
                CurrentFrame = Loop ? 0 : FrameCount - 1;
                if (!Loop) Active = false; // Stop animation if not looping
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Active) return;

        Rectangle sourceRectangle = new Rectangle(CurrentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
        spriteBatch.Draw(Texture, Position, sourceRectangle, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
    }
}