using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class EnemySprite : ISprite
    {
        private Texture2D texture;
        private Rectangle[] sourceRectangle;
        private int currentFrameIndex;
        private int frameIndex1;
        private int frameIndex2;
        private float scale = 2.0f;
        private double timeSinceLastToggle;
        private double millisecondsPerToggle = 200;

        public EnemySprite(Texture2D Enemytexture, int framesCount, int spriteWidth, int spriteHeight, int xOffset = 0, int yOffset = 0)
        {
            texture = Enemytexture;
            sourceRectangle = new Rectangle[framesCount * 8];

            for (int direction = 0; direction < 4; direction++)
            {
                for (int i = 0; i < framesCount; i++)
                {
                    int baseIndex = direction * framesCount + i;
                    int alternateIndex = baseIndex + framesCount * 4;

                    sourceRectangle[baseIndex] = new Rectangle(
                        xOffset + i * spriteWidth,
                        yOffset + direction * spriteHeight,
                        spriteWidth,
                        spriteHeight);

                    sourceRectangle[alternateIndex] = new Rectangle(
                        xOffset + i * spriteWidth,
                        yOffset + (direction + 4) * spriteHeight,
                        spriteWidth,
                        spriteHeight);
                }
            }
        }


        public void SetDirection(Vector2 direction)
        {
            int directionIndex = 0;
            if (direction.X < 0) directionIndex = 1;
            else if (direction.Y < 0) directionIndex = 2;
            else if (direction.X > 0) directionIndex = 3;

            frameIndex1 = directionIndex * 2;
            frameIndex2 = frameIndex1 + 16;
            currentFrameIndex = frameIndex1;
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastToggle += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastToggle >= millisecondsPerToggle)
            {
                if (currentFrameIndex == frameIndex1)
                    currentFrameIndex = frameIndex2;
                else
                    currentFrameIndex = frameIndex1;

                timeSinceLastToggle = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(texture, location, sourceRectangle[currentFrameIndex], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
