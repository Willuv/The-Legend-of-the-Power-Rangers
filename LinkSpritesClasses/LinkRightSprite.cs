using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkRightSprite : ILinkSprite
    {
        private Texture2D linkTexture;
        private int currentFrame;
        private int totalFrames;
        private int nextSpriteDistance;
        private int currentLinkLocation;
        public Rectangle SourceRectangle { get; private set; }
        public LinkRightSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 10;
            currentLinkLocation = 0;
            nextSpriteDistance = 28;

            SourceRectangle = new Rectangle(88, currentLinkLocation, 14, 16);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            spriteBatch.Draw(linkTexture, destinationRectangle, SourceRectangle, color);
        }

        public void Update(GameTime gameTime)
        {
            currentFrame++;
            if (currentFrame > totalFrames)
            {
                currentLinkLocation = currentLinkLocation + nextSpriteDistance;
                nextSpriteDistance = nextSpriteDistance * -1;
                currentFrame = 0;

                SourceRectangle = new Rectangle(88, currentLinkLocation, 14, 16);
            }
        }
    }
}