using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkWinSprite : ILinkSprite
    {
        private Texture2D linkTexture;
        public Rectangle SourceRectangle { get; private set; }

        public LinkWinSprite(Texture2D texture)
        {
            linkTexture = texture;
            // Set the source rectangle to the specified starting position and size for the win sprite
            SourceRectangle = new Rectangle(30, 147, 14, 16);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            spriteBatch.Draw(
                linkTexture, 
                destinationRectangle, 
                SourceRectangle, 
                color, 
                0.0f, // No rotation
                Vector2.Zero, // No origin offset
                SpriteEffects.None, // No sprite effects
                0.1f // Layer depth
            );
        }

        public void Update(GameTime gameTime)
        {
            // No update logic for this static sprite
        }
    }
}
