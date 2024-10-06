using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkAttackUpSprite : IAttackSprite
    {
        private Texture2D linkTexture;
        private Vector2 swordOffset;
        private int currentFrame;
        private int totalFrames;
        private int nextSpriteDistance;
        private int spriteWidth;
        private int spriteHeight;
        private int currentLinkLocation;
        private int spriteStart;
        private bool isAnimationPlaying;

        public LinkAttackUpSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 20;
            spriteWidth = 17;
            spriteHeight = 16;
            spriteStart = 58;
            currentLinkLocation = 59;
            nextSpriteDistance = 23;
            swordOffset = new Vector2(0, 0);
            isAnimationPlaying = true;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(spriteStart, currentLinkLocation, spriteWidth, spriteHeight);

            int expandedHeight = destinationRectangle.Height;
            int expandedY = destinationRectangle.Y;

            if (currentFrame >= 10 && currentFrame <= totalFrames)
            {
                expandedHeight += 24;
                expandedY -= 24;
            }

            Rectangle adjustedDestinationRectangle = new Rectangle(
                destinationRectangle.X,
                expandedY + (int)swordOffset.Y,
                destinationRectangle.Width,
                expandedHeight
            );

            spriteBatch.Draw(linkTexture, adjustedDestinationRectangle, sourceRectangle, color);
        }

        public void Update(GameTime gameTime)
        {
            currentFrame++;

            if (currentFrame == 10)
            {
                currentLinkLocation += nextSpriteDistance;
                swordOffset.Y = -24;
            }

            if (currentFrame > totalFrames)
            {
                currentLinkLocation -= nextSpriteDistance;
                swordOffset.Y = 0;
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
