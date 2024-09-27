using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemArrow : IItem
    {
        private Vector2 position = new Vector2(370, 300);

        private Rectangle rectangle = new Rectangle(0, 0, 16, 16);
        public void Update(GameTime gameTime)
        {
            //no code
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White);
        }
    }
}