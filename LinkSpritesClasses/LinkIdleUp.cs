using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkIdleUp : ILinkSprite
    {
        private Texture2D linkTexture;
        private Rectangle sourceRectangle;


        private float scaleFactor = 3f;
        public LinkIdleUp(Texture2D texture)
        {
            linkTexture = texture;
            sourceRectangle = new Rectangle(60, 0, 14, 16);
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