using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class BlockWall : IBlock
    {
        private Rectangle sourceRectangle = new Rectangle(0, 32, 16, 16);
        private Rectangle destinationRectangle = new Rectangle(400, 340, 16, 16);
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        public ObjectType ObjectType { get { return ObjectType.Block; } }
        public BlockType BlockType { get { return BlockType.Wall; } }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}