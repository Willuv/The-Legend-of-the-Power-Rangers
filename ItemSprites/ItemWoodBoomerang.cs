using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemWoodBoomerang : IItem
    {
        public Rectangle position = new Rectangle(370, 300, 16, 16);

        public Rectangle rectangle = new Rectangle(280, 0, 16, 16);
        public Rectangle DestinationRectangle
        {
            get {return rectangle;}
            set {rectangle = value;}
        }
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