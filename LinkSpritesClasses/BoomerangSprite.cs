using System;
using System.IO;
using Legend_of_the_Power_Rangers.LinkSpritesClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class BoomerangSprite : ILinkItemSprite
    {
        private Texture2D boomerangTexture;
        int totalFrames;
        int currentFrame;
        bool finished;
        Rectangle sourceRectangle;
        Vector2 center;
        Rectangle movement;
        Rectangle offset;
        Rectangle position;
        Rectangle destinationRectangle;

        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        float rotation;

        public ObjectType ObjectType { get { return ObjectType.LinkProjectile; } }
        public LinkProjectileType LinkProjectileType { get { return LinkProjectileType.Boomerang; } }
        private bool hasHitWall = false;
        public bool HasHitWall
        {
            get { return hasHitWall; }
            set { hasHitWall = value; }
        }

        public BoomerangSprite(Texture2D texture, Rectangle position, LinkDirection direction)
        {
            boomerangTexture = texture;
            finished = false;
            currentFrame = 0;
            totalFrames = 100;
            this.position = position;
            this.sourceRectangle = new Rectangle(285, 4, 4, 7);
            this.center = new Vector2(2f, 3.5f);
            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Rectangle(0, 20, 0, 0);
                    movement.X = -3;
                    break;
                case LinkDirection.Right:
                    offset = new Rectangle(50, 20, 0, 0);
                    movement.X = 3;
                    break;
                case LinkDirection.Up:
                    offset = new Rectangle(20, -15, 0, 0);
                    movement.Y = -3;
                    break;
                case LinkDirection.Down:
                    offset = new Rectangle(25, 60, 0, 0);
                    movement.Y = 3;
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle(position.X + offset.X, position.Y + offset.Y, 16, 28);
            spriteBatch.Draw(boomerangTexture, destinationRectangle, sourceRectangle, Color.White, rotation, center, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame % 10 == 0) {
                rotation += currentFrame;
                rotation %= MathHelper.Pi * 2;
            }
            if (currentFrame < 50)
            {
                offset.X += movement.X;
                offset.Y += movement.Y;
            }
            else
            {
                 offset.X -= movement.X;
                 offset.Y -= movement.Y;
            }
            if (currentFrame == totalFrames)
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
