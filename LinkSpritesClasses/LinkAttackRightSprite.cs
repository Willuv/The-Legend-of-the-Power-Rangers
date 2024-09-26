using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Legend_of_the_Power_Rangers
{
    public class LinkAttackRightSprite : ILinkSprite
    {
        private Texture2D linkTexture;
        private int currentFrame;
        private int totalFrames;
        private int nextSpriteDistance;
        private int spriteWidth;
        private int spriteHeight;
        private int currentLinkLocation;
        private int spriteStart;
        private float scaleFactor = 3f;
        private Vector2 swordOffset;
        public LinkAttackRightSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 20;
            spriteWidth = 15;
            spriteHeight = 16;
            spriteStart = 87;
            currentLinkLocation = 59;
            nextSpriteDistance = 28;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(spriteStart, currentLinkLocation, spriteWidth, spriteHeight);
            spriteBatch.Draw(linkTexture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);

        }
        public void Update(GameTime gameTime)
        {
            currentFrame++;
            if (currentFrame == 10)
            {
                currentLinkLocation = currentLinkLocation + nextSpriteDistance;
                spriteStart -= 6;
                spriteWidth += 14;
            }
            if (currentFrame > totalFrames)
            {
                currentLinkLocation = currentLinkLocation - nextSpriteDistance;
                spriteStart += 6;
                spriteWidth -= 14;
                currentFrame = 0;
            }
        }
    }


}
