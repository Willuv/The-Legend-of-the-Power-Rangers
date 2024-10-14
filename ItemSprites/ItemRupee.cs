using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemRupee : IItem
    {
        public Rectangle position = new Rectangle(370, 300, 16, 16);

        public Rectangle rectangle = new Rectangle(160, 120, 16, 16);

        private int change = 1;

        private bool reverse = false;
        public void Update(GameTime gameTime)
        {
            int time = 8;
            if (change <= time/2)
            {
                rectangle = new Rectangle(160, 120, 16, 16);
                if (change == 1)
                {
                    reverse = false;
                }
            }
            else if (change >= time/2 && change <= time)
            {
                rectangle = new Rectangle(200, 120, 16, 16);
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
            spriteBatch.Draw(texture, position, rectangle, Color.White);
        }
    }
}