using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Legend_of_the_Power_Rangers
{
    public class LinkAttackDownSprite : ILinkSprite
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
        private float scaleFactor = 3f;
        public LinkAttackDownSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 20;
            spriteWidth = 17;
            spriteHeight = 16;
            spriteStart = 0;
            currentLinkLocation = 59;
            nextSpriteDistance = 23;
            swordOffset = new Vector2(0, 0);
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
                swordOffset.Y = 10;
                spriteHeight += 12;
            }
            if (currentFrame > totalFrames)
            {
                currentLinkLocation = currentLinkLocation - nextSpriteDistance;
                swordOffset.Y = 0;
                spriteHeight -= 12;
                currentFrame = 0;
            }
        }
    }


}
