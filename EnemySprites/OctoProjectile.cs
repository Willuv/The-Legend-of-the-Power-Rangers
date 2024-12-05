using Legend_of_the_Power_Rangers.LinkSpritesClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class OctoProjectile : IEnemyProjectile
    {
        private Texture2D projectileTexture;
        private Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        private Rectangle offset;
        private Vector2 movement;
        private Rectangle position;
        public Vector2 Direction { get; set; }
        private int currentFrame;
        private int totalFrames;
        private bool finished;
        private int scale = 3;

        public ObjectType ObjectType { get { return ObjectType.EnemyProjectile; } }
        public EnemyProjectileType EnemyProjectileType { get { return EnemyProjectileType.Octo; } }
        private bool hasHitWall = false;
        public bool HasHitWall
        {
            get { return hasHitWall; }
            set { hasHitWall = value; }
        }


        public OctoProjectile(Texture2D texture, Rectangle position, Vector2 direction)
        {
            projectileTexture = texture;
            this.position = position;
            totalFrames = 40; // Time the projectile lasts
            currentFrame = 0;
            finished = false;

            SetDirection(direction);
            this.Direction = direction;
        }

        public void SetDirection(Vector2 direction)
        {
            int centerX = position.Width / 2;
            int centerY = position.Height / 2;
            // offset uses magic numbers to align with sprite
            if (direction.X < 0) // Left
            {
                offset = new Rectangle(centerX-50, centerY+5, 0, 0);
                movement.X = -4;
                sourceRectangle = new Rectangle(150, 8, 15, 5);
            }
            else if (direction.X > 0) // Right
            {
                offset = new Rectangle(centerX+20, centerY+5, 0, 0);
                movement.X = 4;
                sourceRectangle = new Rectangle(210, 8, 15, 5);
            }
            else if (direction.Y < 0) // Up
            {
                offset = new Rectangle(centerX+1, centerY-45, 0, 0);
                movement.Y = -4;
                sourceRectangle = new Rectangle(185, 3, 5, 15);
            }
            else if (direction.Y > 0) // Down
            {
                offset = new Rectangle(centerX+1, centerY+20, 0, 0);
                movement.Y = 4;
                sourceRectangle = new Rectangle(125, 3, 5, 15);
            }
        }

        public void Update(GameTime gameTime)
        {
            currentFrame++;
            position.X += (int)movement.X;
            position.Y += (int)movement.Y;

            if (currentFrame > totalFrames)
            {
                finished = true;
            }

            destinationRectangle = new Rectangle(
                position.X + offset.X,
                position.Y + offset.Y,
                sourceRectangle.Width * scale,
                sourceRectangle.Height * scale);
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(projectileTexture, destinationRectangle, sourceRectangle, Color.White);
        }

        public bool GetState()
        {
            return finished;
        }
    }
}
