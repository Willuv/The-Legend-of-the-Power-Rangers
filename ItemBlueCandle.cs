using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemBlueCandle : IItem
    {
        private Vector2 position = new Vector2(370, 300);

        private Rectangle rectangle = new Rectangle(120, 0, 16, 16);
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Microsoft.Xna.Framework.Color.White);
        }
    }
}