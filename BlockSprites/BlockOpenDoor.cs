using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class BlockOpenDoor : IBlock
    {
        public Rectangle position = new Rectangle(400, 340, 16, 16);
        public Rectangle rectangle = new Rectangle(128, 16, 16, 16);
        public void Update(GameTime gameTime)
        {

        }
        public voisd Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White);
        }
    }
}