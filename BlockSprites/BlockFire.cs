using System;
using Legend_of_the_Power_Rangers.BlockSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class BlockFire : IBlock
    {
        private Rectangle sourceRectangle = new Rectangle(160, 32, 16, 16);
        private Rectangle destinationRectangle = new Rectangle(450, 340, 48, 48);
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        public ObjectType ObjectType { get { return ObjectType.Block; } }
        public BlockType BlockType { get { return BlockType.Fire; } }

        private int change = 1;

        private bool reverse = false;
        public void Update(GameTime gameTime)
        {
            int time = 8;
            if (change <= time/2)
            {
                sourceRectangle = new Rectangle(160, 32, 16, 16);
                if (change == 1)
                {
                    reverse = false;
                }
            }
            else if (change >= time/2 && change <= time)
            {
                sourceRectangle = new Rectangle(176, 32, 16, 16);
                if (change == time)
                {
                    reverse = true;
                }
            }
            if (!reverse)
            {
                change += 1;
            }
            else
            {
                change -= 1;
            }
        }
        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}