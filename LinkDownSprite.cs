using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkDownSprite : ISprite
    {
        private Texture2D linkTexture;
        private int currentFrame;
        private int totalFrames;
        private int nextSpriteDistance;
        private int spriteWidth;
        private int spriteHeight;
        private int currentLinkLocation;
        public LinkDownSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 10;
            spriteWidth = 14;
            spriteHeight = 16;
            currentLinkLocation = 0;
            nextSpriteDistance = 28;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle sourceRectangle = new Rectangle(0, currentLinkLocation, 14, 16);
            spriteBatch.Draw(linkTexture, position, sourceRectangle, Color.White);

        }
        public void Update(GameTime gameTime)
        {
            currentFrame++;
            if (currentFrame > totalFrames)
            {
                currentLinkLocation = currentLinkLocation + nextSpriteDistance;
                nextSpriteDistance = nextSpriteDistance * -1;
                currentFrame = 0;
            }
        }
    }
}