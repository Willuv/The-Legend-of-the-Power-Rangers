using System;
using System.Net.Http.Headers;
using Legend_of_the_Power_Rangers.LinkSpritesClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class BombSprite : IitemSprite
	{
		private Texture2D bombTexture;
        int totalFrames;
        int currentFrame;
        int width;
        int height;
        bool blowing;
        bool finished;
        Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        Rectangle usedRectangle;
        Rectangle sourceRectangle1;
        Rectangle offset;
        Rectangle offset2;
        Rectangle position;
        int scaleFactor = 3;

        public ObjectType ObjectType { get { return ObjectType.LinkItem; } }
        public LinkItemType LinkItemType { get { return LinkItemType.Bomb; } }

        public BombSprite(Texture2D texture, Rectangle position, LinkDirection direction)
		{
            bombTexture = texture;
            finished = false;
            currentFrame = 0;
            totalFrames = 70;
            width = 27;
            height = 45;
            this.position = position;
            position.Width = 27;
            position.Height = 45;
            this.usedRectangle = new Rectangle(203, 0, 9, 15);
            this.sourceRectangle1 = new Rectangle(84, 119, 7, 6);
            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Rectangle(-30, 0, 0, 0);
                    offset2 = new Rectangle(-60, -20, 0, 0);
                    break;
                case LinkDirection.Right:
                    offset = new Rectangle(50, 0, 0, 0);
                    offset2 = new Rectangle(20, -20, 0, 0);
                    break;
                case LinkDirection.Up:
                    offset = new Rectangle(5, -50, 0, 0);
                    offset2 = new Rectangle(-25, -70, 0, 0);
                    break;
                case LinkDirection.Down:
                    offset = new Rectangle(5, 50, 0, 0);
                    offset2 = new Rectangle(-25, 25, 0, 0);
                    break;
            }
		}
        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle(position.X + offset.X, position.Y + offset.Y, usedRectangle.Width * scaleFactor, usedRectangle.Height * scaleFactor);
            spriteBatch.Draw(bombTexture, destinationRectangle, usedRectangle, Color.White);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame == 50) {
                usedRectangle = sourceRectangle1;
                offset = offset2;
                scaleFactor = 12;
            }
            else if (currentFrame == totalFrames)
            {
                finished = true;
            }
        }
        public bool GetState() {
            return finished;
        }

    }

}
