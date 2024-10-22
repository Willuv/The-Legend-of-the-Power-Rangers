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
            spriteBatch.Draw(linkTexture, destinationRectangle, SourceRectangle, color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
        }
        public void Update(GameTime gameTime)
        {
        }
    }
}