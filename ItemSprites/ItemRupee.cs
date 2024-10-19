using System;
using Legend_of_the_Power_Rangers.ItemSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemRupee : IItem
    {
        public Rectangle destinationRectangle = new Rectangle(370, 300, 32, 32);
        public Rectangle sourceRectangle = new Rectangle(160, 120, 16, 16);
        public Rectangle DestinationRectangle
        {
            get {return destinationRectangle; }
            set { destinationRectangle = value;}
        }

        public ObjectType ObjectType { get { return ObjectType.Item; } }
        public ItemType ItemType { get { return ItemType.Rupee; } }


        private int change = 1;

        private bool reverse = false;
        public void Update(GameTime gameTime)
        {
            int time = 8;
            if (change <= time/2)
            {
                sourceRectangle = new Rectangle(160, 120, 16, 16);
                if (change == 1)
                {
                    reverse = false;
                }
            }
            else if (change >= time/2 && change <= time)
            {
                sourceRectangle = new Rectangle(200, 120, 16, 16);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ItemSpriteFactory.Instance.GetItemSpritesheet(), destinationRectangle, sourceRectangle, Color.White);
        }
    }
}