using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkIdleUp : ILinkSprite
    {
        private Texture2D linkTexture;
        public Rectangle SourceRectangle { get; private set; }

        public LinkIdleUp(Texture2D texture)
        {
            linkTexture = texture;
            SourceRectangle = new Rectangle(60, 0, 14, 16);
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