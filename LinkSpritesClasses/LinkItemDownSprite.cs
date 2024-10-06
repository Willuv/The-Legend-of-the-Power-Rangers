using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkItemDownSprite : ILinkSprite
    {
        private Texture2D linkTexture;
        private int currentFrame;
        private int totalFrames;
        private int currentLinkLocation;
        private Rectangle sourceRectangle;

        public LinkItemDownSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 10;
            currentLinkLocation = 58;

            sourceRectangle = new Rectangle(0, currentLinkLocation, 17, 17);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            spriteBatch.Draw(linkTexture, destinationRectangle, sourceRectangle, color);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
