using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class HUD
    {
        private Texture2D hudTexture;
        private Rectangle hudSourceRectangle = new Rectangle(0, 183, 255, 48);
        private Rectangle hudDestinationRectangle; 
        private SpriteBatch hudSpriteBatch;

        public HUD(GraphicsDevice graphicsDevice, Texture2D hudTexture, Rectangle destinationRectangle)
        {
            this.hudTexture = hudTexture;
            this.hudSpriteBatch = new SpriteBatch(graphicsDevice);
            this.hudDestinationRectangle = destinationRectangle;
        }

        public void Draw()
        {
            hudSpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            hudSpriteBatch.Draw(hudTexture, hudDestinationRectangle, hudSourceRectangle, Color.White);
            hudSpriteBatch.End();
        }
    }
}
