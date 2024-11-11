using Legend_of_the_Power_Rangers.LevelCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;


namespace Legend_of_the_Power_Rangers
{
    public class GreenDot
    {
        public Rectangle destinationRectangle = new Rectangle(613, 574, 16, 16);
        public Rectangle sourceRectangle = new Rectangle(15, 232, 3, 3);
        private Texture2D greenDotTexture;
        private SpriteBatch greenDotSpriteBatch;
        private int currentRoom;
        private LinkInventory linkInventory;

        public GreenDot(GraphicsDevice graphicsDevice, Texture2D greenDotTexture, int currentRoom, LinkInventory linkInventory)
        {
            this.greenDotTexture = greenDotTexture;
            this.greenDotSpriteBatch = new SpriteBatch(graphicsDevice);
            //this.destinationRectangle = destinationRectangle;
            this.currentRoom = currentRoom;
            this.linkInventory = linkInventory;
        }

        public void Update()
        {
            switch (currentRoom)
            {
                case 1:
                    destinationRectangle = new Rectangle(613, 575, 16, 16);
                    break;
                case 2:
                    destinationRectangle = new Rectangle(581, 575, 16, 16);
                    break;
                case 3:
                    destinationRectangle = new Rectangle(645, 575, 16, 16);
                    break;
                case 4:
                    destinationRectangle = new Rectangle(613, 544, 16, 16);
                    break;
                case 5:
                    destinationRectangle = new Rectangle(613, 513, 16, 16);
                    break;
                case 6:
                    destinationRectangle = new Rectangle(581, 513, 16, 16);
                    break;
                case 7:
                    destinationRectangle = new Rectangle(645, 513, 16, 16);
                    break;
                case 8:
                    destinationRectangle = new Rectangle(613, 482, 16, 16);
                    break;
                case 9:
                    destinationRectangle = new Rectangle(581, 482, 16, 16);
                    break;
                case 10:
                    destinationRectangle = new Rectangle(549, 482, 16, 16);
                    break;
                case 11:
                    destinationRectangle = new Rectangle(645, 482, 16, 16);
                    break;
                case 12:
                    destinationRectangle = new Rectangle(677, 482, 16, 16);
                    break;
                case 13:
                    destinationRectangle = new Rectangle(613, 450, 16, 16);
                    break;
                case 14:
                    destinationRectangle = new Rectangle(677, 450, 16, 16);
                    break;
                case 15:
                    destinationRectangle = new Rectangle(709, 450, 16, 16);
                    break;
                case 16:
                    destinationRectangle = new Rectangle(613, 420, 16, 16);
                    break;
                case 17:
                    destinationRectangle = new Rectangle(581, 420, 16, 16);
                    break;
                case 18:
                    destinationRectangle = new Rectangle(549, 420, 16, 16);
                    break;
            }
            
        }

        public void Draw()
        {
            
            greenDotSpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (linkInventory.HasItem(ItemType.Compass))
            {
                greenDotSpriteBatch.Draw(greenDotTexture, destinationRectangle, sourceRectangle, Color.White);
            }  
            greenDotSpriteBatch.End();
        }
    }
}
