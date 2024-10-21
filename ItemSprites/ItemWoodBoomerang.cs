using System;
using Legend_of_the_Power_Rangers.ItemSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemWoodBoomerang : IItem
    {
        public Rectangle destinationRectangle = new Rectangle(370, 300, 32, 32);

        public Rectangle sourceRectangle = new Rectangle(280, 0, 16, 16);
        public Rectangle DestinationRectangle
        {
            get {return destinationRectangle; }
            set { destinationRectangle = value;}
        }

        public ObjectType ObjectType { get { return ObjectType.Item; } }
        public ItemType ItemType { get { return ItemType.WoodBoomerang; } }

        public void Update(GameTime gameTime)
        {
            //no code
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}