using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class ItemTriforce : IItem
    {
        public Rectangle destinationRectangle = new Rectangle(370, 300, 32, 32);
        public Rectangle sourceRectangle = new Rectangle(320, 120, 16, 16);

        public Rectangle CollisionHitbox
        {
            get {return destinationRectangle; }
            set { destinationRectangle = value;}
        }

        private readonly Link link;
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
                sourceRectangle = new Rectangle(320, 120, 16, 16);
                if (change == 1)
                {
                    reverse = false;
                }
            }
            else if (change >= time/2 && change <= time)
            {
                sourceRectangle = new Rectangle(340, 120, 16, 16);
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
            if (PickedUp)
            {
                Link link = LinkManager.GetLink();
                // Draw above link
                destinationRectangle = new Rectangle(
                    link.destinationRectangle.X + link.destinationRectangle.Width / 2 - CollisionHitbox.Width / 2,
                    link.destinationRectangle.Y - CollisionHitbox.Height,
                    CollisionHitbox.Width,
                    CollisionHitbox.Height
                );
                spriteBatch.Draw(ItemSpriteFactory.Instance.GetItemSpritesheet(), destinationRectangle, sourceRectangle, Color.White);

            } else {
                spriteBatch.Draw(ItemSpriteFactory.Instance.GetItemSpritesheet(), destinationRectangle, sourceRectangle, Color.White);
            }
        }
    }
}