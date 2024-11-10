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

        public GreenDot(GraphicsDevice graphicsDevice, Texture2D greenDotTexture, int currentRoom)
        {
            this.greenDotTexture = greenDotTexture;
            this.greenDotSpriteBatch = new SpriteBatch(graphicsDevice);
            //this.destinationRectangle = destinationRectangle;
            this.currentRoom = currentRoom;
        }

        public void Update()
        {
            switch (currentRoom)
            {
                case 1:
                    destinationRectangle = new Rectangle(613, 574, 16, 16);
                    break;
                case 2:
                    destinationRectangle = new Rectangle(613, 544, 16, 16);
                    break;
                case 3:
                    destinationRectangle = new Rectangle(613, 514, 16, 16);
                    break;
            }
            
        }

        public void Draw()
        {
            
            greenDotSpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            greenDotSpriteBatch.Draw(greenDotTexture, destinationRectangle, sourceRectangle, Color.White);
            greenDotSpriteBatch.End();
        }
    }
}
