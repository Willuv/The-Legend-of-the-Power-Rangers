using System;
using Legend_of_the_Power_Rangers.ItemSprites;
using Legend_of_the_Power_Rangers.LinkSpritesClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class ArrowSprite : IitemSprite
    {
        private Texture2D arrowTexture;
        int totalFrames;
        int currentFrame;
        bool finished;
        Rectangle offset;
        Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        Rectangle position;
        Rectangle movement;
        Rectangle sourceRectangle;
        float scaleFactor = 3f;

        public ObjectType ObjectType { get { return ObjectType.LinkItem; } }
        public LinkItemType LinkItemType { get { return LinkItemType.Arrow; } }


        public ArrowSprite(Texture2D texture, Rectangle position, LinkDirection direction)
        {
            arrowTexture = texture;
            finished = false;
            currentFrame = 0;
            totalFrames = 40;
            this.position = position;
            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Rectangle(-50, 15, 0, 0);
                    movement.X = -4;
                    sourceRectangle = new Rectangle(150, 8, 15, 5);
                    break;
                case LinkDirection.Right:
                    offset = new Rectangle(50, 15, 0, 0);
                    movement.X = 4;
                    sourceRectangle = new Rectangle(210, 8, 15, 5);
                    break;
                case LinkDirection.Up:
                    offset = new Rectangle(15, -50, 0, 0);
                    movement.Y = -4;
                    sourceRectangle = new Rectangle(185, 3, 5, 15);
                    break;
                case LinkDirection.Down:
                    offset = new Rectangle(15, 50, 0, 0);
                    movement.Y = 4;
                    sourceRectangle = new Rectangle(125, 3, 5, 15);
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle(position.X + offset.X, position.Y + offset.Y, sourceRectangle.Width * 3, sourceRectangle.Height * 3);
            spriteBatch.Draw(arrowTexture, destinationRectangle, sourceRectangle, Color.White);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (movement.X == 0)
            {
                position.Y += movement.Y;
            } else
            {
                position.X += movement.X;
            }
            if (currentFrame > totalFrames)
            {
                finished = true;
            }
        }
        public bool GetState()
        {
            return finished;
        }

    }

}
