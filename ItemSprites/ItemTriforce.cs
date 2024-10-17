using System;
using Legend_of_the_Power_Rangers.ItemSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemTriforce : IItem
    {
        public Rectangle position = new Rectangle(370, 300, 16, 16);

        public Rectangle rectangle = new Rectangle(320, 120, 16, 16);
        public Rectangle DestinationRectangle
        {
            get {return rectangle;}
            set {rectangle = value;}
        }

        public ObjectType ObjectType { get { return ObjectType.Item; } }
        public ItemType ItemType { get { return ItemType.Triforce; } }

        bool pickedUp = false;
        public bool PickedUp
        {
            get { return pickedUp; }
            set { pickedUp = value; }
        }

        private int change = 1;

        private bool reverse = false;

        
        public void Update(GameTime gameTime)
        {
            int time = 8;
            if (change <= time/2)
            {
                rectangle = new Rectangle(320, 120, 16, 16);
                if (change == 1)
                {
                    reverse = false;
                }
            }
            else if (change >= time/2 && change <= time)
            {
                rectangle = new Rectangle(340, 120, 16, 16);
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