using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkIdleRight : ILinkSprite
    {
        private Texture2D linkTexture;
        private Rectangle sourceRectangle;

        public LinkIdleRight(Texture2D texture)
        {
            linkTexture = texture;
            sourceRectangle = new Rectangle(88, 0, 14, 16);
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
