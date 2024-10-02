using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkIdleLeft : ILinkSprite
    {
        private Texture2D linkTexture;

        private float scaleFactor = 3f;
        public LinkIdleLeft(Texture2D texture)
        {
            linkTexture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(30, 0, 14, 16);
            spriteBatch.Draw(linkTexture, position, sourceRectangle, color, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);

        }
        public void Update(GameTime gameTime)
        {
        }
    }
}