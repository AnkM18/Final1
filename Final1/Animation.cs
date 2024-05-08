using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Final1
{
    public class Animation
    {
        // The image representing the collection of images used for animation
        Texture2D spriteStrip;
        // The scale used to display the sprite strip
        float scale;
        // The time since we last updated the frame
        int elapsedTime;
        // The time we display a frame until the next one
        int frameTime;
        // The number of frames that the animation contains
        int frameCount;
        // The index of the current frame we are displaying
        int currentFrame;
        // The color of the frame we will be displaying
        Color color;
        // The area of the image strip we want to display
        Rectangle sourceRect = new Rectangle();
        // The area where we want to display the image strip in the game
        Rectangle destinationRect = new Rectangle();
        // Width of a given frame
        public int FrameWidth;
        // Height of a given frame
        public int FrameHeight;
        // The state of the Animation
        public bool Active;
        // Determines if the animation will keep playing or deactivate after one run
        public bool Looping;
        // Position of the frame
        public Vector2 Position;

        


        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;
            Looping = looping;
            Position = position;
            spriteStrip = texture;

            elapsedTime = 0;
            currentFrame = 0;

            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!Active)
                return;

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            

            if (elapsedTime > frameTime)
            {
                currentFrame++;
                elapsedTime = 0;

                if (currentFrame >= frameCount)
                {
                    if (Looping)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        Active = false; // Stop the animation if not looping
                    }
                }
            }

            int framesPerRow = spriteStrip.Width / FrameWidth;
            int frameRow = currentFrame / framesPerRow;
            int frameColumn = currentFrame % framesPerRow;

            sourceRect = new Rectangle(frameColumn * FrameWidth, frameRow * FrameHeight, FrameWidth, FrameHeight);
            


            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2,
                                            (int)Position.Y - (int)(FrameHeight * scale) / 2,
                                            (int)(FrameWidth * scale),
                                            (int)(FrameHeight * scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Vector2 origin = new Vector2(FrameWidth / 2, FrameHeight / 2);
                spriteBatch.Draw(spriteStrip, Position, sourceRect, color, 0f, origin, scale, SpriteEffects.None, 0f);
            }
        }
        public void SetScale(float newScale)
        {
            scale = newScale;
        }
        public Rectangle GetCurrentFrame()
        {
            return new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, spriteStrip.Height);
        }

    }
}
