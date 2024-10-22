using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkAttackLeftSprite : IAttackSprite
    {
        private Texture2D linkTexture;
        private int currentFrame;
        private int totalFrames;
        private int spriteWidth;
        private int spriteHeight;
        private Rectangle[] frameRectangles;
        private bool isAnimationPlaying;

        public Rectangle SourceRectangle
        {
            get
            {
                if (currentFrame < 10)
                    return frameRectangles[0];
                else
                    return frameRectangles[1];
            }
        }

        public LinkAttackLeftSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 20;
            spriteWidth = 17;
            spriteHeight = 16;
            isAnimationPlaying = true;

            frameRectangles = new Rectangle[2];
            frameRectangles[0] = new Rectangle(28, 59, spriteWidth, spriteHeight);
            frameRectangles[1] = new Rectangle(23, 88, spriteWidth + 11, spriteHeight);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            Rectangle sourceRectangle;

            if (currentFrame < 10)
            {
                sourceRectangle = frameRectangles[0];
            }
            else
            {
                sourceRectangle = frameRectangles[1];
            }

            spriteBatch.Draw(linkTexture, destinationRectangle, sourceRectangle, color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
        }

        public void Update(GameTime gameTime)
        {
            currentFrame++;

            if (currentFrame > totalFrames)
            {
                currentFrame = 0;
                isAnimationPlaying = false;
            }
        }

        public bool IsAnimationPlaying()
        {
            return isAnimationPlaying;
        }
    }
}
