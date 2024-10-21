using System;
using System.Diagnostics;
using Legend_of_the_Power_Rangers.BlockSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class BlockBlueFloor : IBlock
    {
        private Rectangle sourceRectangle = new Rectangle(160, 16, 16, 16);
        private Rectangle destinationRectangle = new Rectangle(450, 340, 48, 48);
        
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        public ObjectType ObjectType { get { return ObjectType.Block; } }
        public BlockType BlockType { get { return BlockType.BlueFloor; } }

        public void Update(GameTime gameTime)
        {
        }
        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            Debug.WriteLine($"Block position: {destinationRectangle.X}, {destinationRectangle.Y}");
        }
    }
}