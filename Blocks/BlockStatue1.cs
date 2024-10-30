using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class BlockStatue1 : IBlock
    {
        private Rectangle sourceRectangle = new Rectangle(80, 0, 16, 16);
        private Rectangle destinationRectangle = new Rectangle(450, 340, 48, 48);

        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        public ObjectType ObjectType { get { return ObjectType.Block; } }
        public BlockType BlockType { get { return BlockType.Statue1; } }

        public BlockStatue1() { }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlockSpriteFactory.Instance.GetBlockSpritesheet(), destinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);
        }
    }
}
