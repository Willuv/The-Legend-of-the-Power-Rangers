using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkAttackLeftSprite : IAttackSprite
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

        public LinkAttackLeftSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 20;
            spriteWidth = 15;
            spriteHeight = 16;
            spriteStart = 28;
            currentLinkLocation = 59;
            nextSpriteDistance = 28;
            swordOffset = new Vector2(0, 0);
            isAnimationPlaying = true;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(spriteStart, currentLinkLocation, spriteWidth, spriteHeight);

            int expandedWidth = destinationRectangle.Width;
            int expandedX = destinationRectangle.X;

            if (currentFrame >= 10 && currentFrame <= totalFrames)
            {
                expandedWidth += 22;
                expandedX -= 22;
            }

            Rectangle adjustedDestinationRectangle = new Rectangle(
                expandedX + (int)swordOffset.X,
                destinationRectangle.Y,
                expandedWidth,
                destinationRectangle.Height
            );

            spriteBatch.Draw(linkTexture, adjustedDestinationRectangle, sourceRectangle, color);
        }

        public void Update(GameTime gameTime)
        {
            currentFrame++;

            if (currentFrame == 10)
            {
                currentLinkLocation += nextSpriteDistance;
                swordOffset.X = -22;
            }

            if (currentFrame > totalFrames)
            {
                currentLinkLocation -= nextSpriteDistance;
                swordOffset.X = 0;
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
