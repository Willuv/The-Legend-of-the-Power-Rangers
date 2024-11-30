using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class BlockBlueFloor : IBlock
    {
        private Rectangle sourceRectangle = new Rectangle(160, 16, 16, 16);
        private Rectangle destinationRectangle = new Rectangle(450, 340, 48, 48);
        public bool IsMoving { get; set;}
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        public ObjectType ObjectType { get { return ObjectType.Block; } }
        public BlockType BlockType { get { return BlockType.BlueFloor; } }

        public BlockBlueFloor() { }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlockSpriteFactory.Instance.GetBlockSpritesheet(), destinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0,0), SpriteEffects.None, 0.9f);
        }
    }
}
