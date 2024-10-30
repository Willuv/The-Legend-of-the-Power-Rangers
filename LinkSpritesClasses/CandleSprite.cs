using System;
using System.IO;
using Legend_of_the_Power_Rangers.LinkSpritesClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class CandleSprite : ILinkItemSprite, IDamaging
    {
        private Texture2D candleTexture;
        int totalFrames;
        int currentFrame;
        bool finished;
        Rectangle offset;
        Rectangle position;
        Rectangle movement;
        Rectangle currentRectangle;
        Rectangle sourceRectangle1;
        Rectangle sourceRectangle2;
        Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        float scaleFactor = 3f;

        public ObjectType ObjectType { get { return ObjectType.Projectile; } }
        public ProjectileType ProjectileType { get { return ProjectileType.Candle; } }
        private bool hasHitWall = false;
        public bool HasHitWall
        {
            get { return hasHitWall; }
            set { hasHitWall = value; }
        }

        public CandleSprite(Texture2D texture, Rectangle position, LinkDirection direction)
        {
            candleTexture = texture;
            finished = false;
            currentFrame = 0;
            totalFrames = 150;
            sourceRectangle1 = new Rectangle(160, 32, 15, 15);
            sourceRectangle2 = new Rectangle(176, 32, 15, 15);
            currentRectangle = sourceRectangle1;
            this.position = position;
            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Rectangle(-50, 5, 0 ,0);
                    offset.X = -50;
                    movement.X = -2;
                    break;
                case LinkDirection.Right:
                    offset = new Rectangle(50, 5, 0, 0);
                    offset.X = 50;
                    movement.X = 2;
                    break;
                case LinkDirection.Up:
                    offset = new Rectangle(5, -50, 0, 0);
                    offset.Y = -50;
                    movement.Y = -2;
                    break;
                case LinkDirection.Down:
                    offset = new Rectangle(5, 50, 0, 0);
                    movement.Y = 2;
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle(position.X + offset.X, position.Y + offset.Y, 45, 45);
            spriteBatch.Draw(candleTexture, destinationRectangle, currentRectangle, Color.White);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame % 7 == 0)
            {
                if (currentRectangle == sourceRectangle1)
                {
                    currentRectangle = sourceRectangle2;
                }
                else
                {
                    currentRectangle = sourceRectangle1;
                }
            }
            if (currentFrame < 40)
            {
                if (movement.X == 0)
                {
                    offset.Y += movement.Y;
                }
                else
                {
                    offset.X += movement.X;
                }
            }
            else if (currentFrame > totalFrames)
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
