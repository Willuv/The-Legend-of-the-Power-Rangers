using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkIdleRight : ILinkSprite
    {
        private Texture2D linkTexture;
        public Rectangle SourceRectangle { get; private set; }

        public LinkIdleRight(Texture2D texture)
        {
            linkTexture = texture;
            SourceRectangle = new Rectangle(88, 0, 14, 16);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            spriteBatch.Draw(linkTexture, destinationRectangle, SourceRectangle, color);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
