using System;
using Legend_of_the_Power_Rangers.LinkSpritesClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class SwordSprite : ILinkItemSprite
    {
        private Texture2D swordTexture;
        int totalFrames;
        int currentFrame;
        bool secondStage;
        bool finished;
        Rectangle offset;
        Rectangle movement1;
        Rectangle movement2;
        Rectangle movement3;
        Rectangle movement4;
        Rectangle position;
        Rectangle position1;
        Rectangle position2;
        Rectangle position3;
        Rectangle position4;
        Rectangle movement;
        Rectangle sourceRectangle;
        Rectangle sourceRectangle1;
        Rectangle sourceRectangle2;
        Rectangle sourceRectangle3;
        Rectangle sourceRectangle4;
        Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        Rectangle destinationRectangle1;
        public Rectangle DestinationRectangle1
        {
            get { return destinationRectangle1; }
            set { destinationRectangle1 = value; }
        }
        Rectangle destinationRectangle2;
        public Rectangle DestinationRectangle2
        {
            get { return destinationRectangle2; }
            set { destinationRectangle2 = value; }
        }
        Rectangle destinationRectangle3;
        public Rectangle DestinationRectangle3
        {
            get { return destinationRectangle3; }
            set { destinationRectangle3 = value; }
        }
        Rectangle destinationRectangle4;
        public Rectangle DestinationRectangle4
        {
            get { return destinationRectangle4; }
            set { destinationRectangle4 = value; }
        }
        int scaleFactor = 3;

        public ObjectType ObjectType { get { return ObjectType.LinkProjectile; } }
        public LinkProjectileType LinkProjectileType { get { return LinkProjectileType.ThrownSword; } }
        private bool hasHitWall = false;
        public bool HasHitWall
        {
            get { return hasHitWall; }
            set { hasHitWall = value; }
        }

        public SwordSprite(Texture2D texture, Rectangle position, LinkDirection direction)
        {
            swordTexture = texture;
            secondStage = false;
            finished = false;
            currentFrame = 0;
            totalFrames = 100;
            movement1 = new Rectangle(-1, -1, 0, 0);
            movement2 = new Rectangle(1, -1, 0, 0);
            movement3 = new Rectangle(-1, 1, 0, 0);
            movement4 = new Rectangle(1, 1, 0, 0);
            sourceRectangle1 = new Rectangle(179, 90, 7, 9);
            sourceRectangle2 = new Rectangle(188, 90, 7, 9);
            sourceRectangle3 = new Rectangle(179, 101, 7, 9);
            sourceRectangle4 = new Rectangle(188, 101, 7, 9);
            this.position = position;
            switch (direction)
            {
                case LinkDirection.Left:
                    sourceRectangle = new Rectangle(30 , 7, 16, 7);
                    offset = new Rectangle(-50, 15, 0, 0);
                    movement.X = -4;
                    break;
                case LinkDirection.Right:
                    sourceRectangle = new Rectangle(90, 7, 16, 7);
                    offset = new Rectangle(50, 15, 0, 0);
                    movement.X = 4;
                    break;
                case LinkDirection.Up:
                    sourceRectangle = new Rectangle(64, 3, 7, 16);
                    offset = new Rectangle(15, -50, 0, 0);
                    movement.Y = -4;
                    break;
                case LinkDirection.Down:
                    sourceRectangle = new Rectangle(4, 3, 7, 16);
                    offset = new Rectangle(15, 50, 0, 0);
                    movement.Y = 4;
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!secondStage)
            {
                destinationRectangle = new Rectangle(position.X + offset.X, position.Y + offset.Y, sourceRectangle.Width * scaleFactor, sourceRectangle.Height * scaleFactor);
                spriteBatch.Draw(swordTexture, destinationRectangle, sourceRectangle, Color.White);
            }
            else
            {
                destinationRectangle1 = new Rectangle(position1.X + offset.X, position1.Y + offset.Y, sourceRectangle1.Width * scaleFactor, sourceRectangle1.Height * scaleFactor);
                destinationRectangle2 = new Rectangle(position2.X + offset.X, position2.Y + offset.Y, sourceRectangle2.Width * scaleFactor, sourceRectangle2.Height * scaleFactor);
                destinationRectangle3 = new Rectangle(position3.X + offset.X, position3.Y + offset.Y, sourceRectangle3.Width * scaleFactor, sourceRectangle3.Height * scaleFactor);
                destinationRectangle4 = new Rectangle(position4.X + offset.X, position4.Y + offset.Y, sourceRectangle4.Width * scaleFactor, sourceRectangle4.Height * scaleFactor);
                spriteBatch.Draw(swordTexture, destinationRectangle1, sourceRectangle1, Color.White);
                spriteBatch.Draw(swordTexture, destinationRectangle2, sourceRectangle2, Color.White);
                spriteBatch.Draw(swordTexture, destinationRectangle3, sourceRectangle3, Color.White);
                spriteBatch.Draw(swordTexture, destinationRectangle4, sourceRectangle4, Color.White);
            }
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame < 30)
            {
                position.X += movement.X;
                position.Y += movement.Y;
            }
            else if (currentFrame == 30)
            {
                secondStage = true;
                position1 = position;
                position2 = position;
                position3 = position;
                position4 = position;
            } else if (currentFrame <= totalFrames)
            {
                position1.X += movement1.X;
                position1.Y += movement1.Y;
                position2.X += movement2.X;
                position2.Y += movement2.Y;
                position3.X += movement3.X;
                position3.Y += movement3.Y;
                position4.X += movement4.X;
                position4.Y += movement4.Y;
            }
            else
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
