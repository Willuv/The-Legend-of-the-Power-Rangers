using System;
using System.Net.Http.Headers;
using Legend_of_the_Power_Rangers.LinkSpritesClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class BluePortalProjectileSprite : ILinkItemSprite
	{
		private Texture2D bluePortalProjectileTexture;
        int width;
        int height;
        Rectangle destinationRectangle;
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        Rectangle sourceRectangle;
        Rectangle offset;
        Rectangle position;
        Rectangle movement;
        int scaleFactor = 3;
        int currentFrame = 0;

        public ObjectType ObjectType { get { return ObjectType.LinkProjectile; } }
        public LinkProjectileType LinkProjectileType { get { return LinkProjectileType.BluePortal; } }
        private bool hasHitWall = false;
        public bool HasHitWall
        {
            get { return hasHitWall; }
            set { hasHitWall = value; }
        }

        public BluePortalProjectileSprite(Texture2D texture, Rectangle position, LinkDirection direction)
		{
            width = 8;
            height = 8;
            this.position = position;
            position.Width = width;
            position.Height = height;
            this.bluePortalProjectileTexture = texture;
            this.sourceRectangle = new Rectangle(164, 0, 8, 8);

            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Rectangle(-30, 0, 0, 0);
                    movement.X = -4;
                    break;
                case LinkDirection.Right:
                    offset = new Rectangle(50, 0, 0, 0);
                    movement.X = 4;
                    break;
                case LinkDirection.Up:
                    offset = new Rectangle(5, -50, 0, 0);
                    movement.Y = -4;
                    break;
                case LinkDirection.Down:
                    offset = new Rectangle(5, 50, 0, 0);
                    movement.Y = 4;
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle(position.X + offset.X, position.Y + offset.Y, sourceRectangle.Width * scaleFactor, sourceRectangle.Height * scaleFactor);

            //if (currentFrame < 10)
            //{
            //    spriteBatch.Draw(bluePortalProjectileTexture, destinationRectangle, sourceRectangle, Color.White);
            //}
            //else
            //{
            //    spriteBatch.Draw(bluePortalProjectileTexture, destinationRectangle, sourceRectangle,
            //        Color.White, MathHelper.ToRadians(45), new Vector2(sourceRectangle.Width / 2,
            //        sourceRectangle.Height / 2), SpriteEffects.None, 0);
            //}
            //only if I want the projectile to look nice. not a priority

            spriteBatch.Draw(bluePortalProjectileTexture, destinationRectangle, sourceRectangle, Color.White);

        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (movement.X == 0)
            {
                position.Y += movement.Y;
            }
            else
            {
                position.X += movement.X;
            }

            currentFrame %= 20;
        }
        public bool GetState() {
            return false; //will look at later
        }

    }

}
