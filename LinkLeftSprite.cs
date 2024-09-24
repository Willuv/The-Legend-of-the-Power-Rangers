using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkLeftSprite : ISprite
    {
        private Texture2D linkTexture;
        private int currentFrame;
        private int totalFrames;
        private int nextSpriteDistance;
        private int spriteWidth;
        private int spriteHeight;
        private int currentLinkLocation;
        private float scaleFactor = 3f;
        public LinkLeftSprite(Texture2D texture)
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
            Rectangle sourceRectangle = new Rectangle(28, currentLinkLocation, 14, 16);
            spriteBatch.Draw(linkTexture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);

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