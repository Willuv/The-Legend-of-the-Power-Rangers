using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class InvisibleTeleportBlock : IBlock
    {
        private Rectangle sourceRectangle = new Rectangle();
        private Rectangle destinationRectangle = new Rectangle(450, 340, 48, 48);
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        public ObjectType ObjectType { get { return ObjectType.Block; } }
        public BlockType BlockType { get { return BlockType.TeleportBlock; } }
        public int DesiredRoom { get; set; }
        public Vector2 DesiredPosition { get; set; }

        public InvisibleTeleportBlock() {
            DesiredRoom = 1; //default
            DesiredPosition = Vector2.Zero; //default
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlockSpriteFactory.Instance.GetBlockSpritesheet(), destinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0,0), SpriteEffects.None, 0.9f);
        }
    }
}
