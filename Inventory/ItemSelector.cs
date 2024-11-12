using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class ItemSelector
    {
        private int change = 1;
        private bool reverse = false;

        public Rectangle destinationRectangle = new Rectangle(505, 180, 65, 65);
        public Rectangle sourceRectangle = new Rectangle(257, 44, 17, 17);
        private Rectangle activeSource;
        private Rectangle activeDestination1 = new Rectangle(260, 180, 50, 65);
        private Rectangle activeDestination2 = new Rectangle(508, 770, 40, 58);
        private Texture2D itemSelectTexture;
        private SpriteBatch itemSelectSpriteBatch;
        private LinkInventory linkInventory;
        int destX;

        public ItemSelector(GraphicsDevice graphicsDevice, Texture2D itemSelectTexture, int destX, LinkInventory linkInventory) 
        {
            this.itemSelectTexture = itemSelectTexture;
            this.itemSelectSpriteBatch = new SpriteBatch(graphicsDevice);
            //this.destinationRectangle = destinationRectangle;
            this.destX = destX;
            this.linkInventory = linkInventory;
        }

        public void moveSelector(int direction)
        {
            int count = 1;
            destX += direction;

            if(destX > 705)
            {
                destX = 505;
            }
            if (destX < 505)
            {
                destX = 705;
            }
            destinationRectangle = new Rectangle(destX, 180, 65, 65);
        }
        public void chooseActiveItem()
        {
            switch(destX)
            {
                case 505:
                    if (linkInventory.HasItem(ItemType.WoodBoomerang))
                    {
                        linkInventory.ActiveItem = ItemType.WoodBoomerang;
                        activeSource = new Rectangle(127, 232, 9, 17);
                    }
                    break;
                case 605:
                    if (linkInventory.HasItem(ItemType.Bomb))
                    {
                        linkInventory.ActiveItem = ItemType.Bomb;
                        activeSource = new Rectangle(127, 249, 9, 17);
                    }
                    break;
                case 705:
                    if (linkInventory.HasItem(ItemType.Bow))
                    {
                        linkInventory.ActiveItem = ItemType.Bow;
                        activeSource = new Rectangle(127, 266, 9, 17);
                    }
                    break;
                default:
                    activeSource = new Rectangle(1, 1, 1, 1);
                    break;
            }
        }
        public void Update(GameTime gameTime)
        {
            int time = 8;
            if (change <= time / 2)
            {
                sourceRectangle = new Rectangle(257, 44, 17, 17);
                if (change == 1)
                {
                    reverse = false;
                }
            }
            else if (change >= time / 2 && change <= time)
            {
                sourceRectangle = new Rectangle(274, 44, 17, 17);
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
        public void Draw()
        {
            itemSelectSpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            itemSelectSpriteBatch.Draw(itemSelectTexture, destinationRectangle, sourceRectangle, Color.White);
            itemSelectSpriteBatch.Draw(itemSelectTexture, activeDestination1, activeSource, Color.White);
            itemSelectSpriteBatch.Draw(itemSelectTexture, activeDestination2, activeSource, Color.White);
            itemSelectSpriteBatch.End();
        }
    }
}
