using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemRupee : IItem
    {
        private Microsoft.Xna.Framework.Vector2 position = new Microsoft.Xna.Framework.Vector2(370, 300);

        private Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(160, 120, 16, 16);

        private int change = 1;

        private bool reverse = false;
        public void Update(GameTime gameTime)
        {
            int time = 8;
            if (change <= time/2)
            {
                rectangle = new Microsoft.Xna.Framework.Rectangle(160, 120, 16, 16);
                if (change == 1)
                {
                    reverse = false;
                }
            }
            else if (change >= time/2 && change <= time)
            {
                rectangle = new Microsoft.Xna.Framework.Rectangle(200, 120, 16, 16);
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
            spriteBatch.Draw(texture, position, rectangle, Microsoft.Xna.Framework.Color.White);
        }
    }
}