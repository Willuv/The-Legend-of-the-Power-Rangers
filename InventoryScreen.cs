using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class InventoryScreen
    {
        private Texture2D InventoryTexture;
        private Rectangle InventorySourceRectangle = new Rectangle(0, 0, 255, 265);
        private Rectangle InventoryDestinationRectangle;
        private SpriteBatch InventorySpriteBatch;

        public InventoryScreen(GraphicsDevice graphicsDevice, Texture2D hudTexture, Rectangle destinationRectangle)
        {
            this.InventoryTexture = hudTexture;
            this.InventorySpriteBatch = new SpriteBatch(graphicsDevice);
            this.InventoryDestinationRectangle = destinationRectangle;
        }

        public void Draw()
        {
            InventorySpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            InventorySpriteBatch.Draw(InventoryTexture, InventoryDestinationRectangle, InventorySourceRectangle, Color.White);
            InventorySpriteBatch.End();
        }
    }
}