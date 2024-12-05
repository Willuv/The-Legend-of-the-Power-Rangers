using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class GoryaProjectile : IEnemyProjectile
    {
        private Texture2D projectileTexture;
        private Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        private Rectangle offset;
        private Vector2 movement;
        private Rectangle position;
        private Vector2 center;
        private int currentFrame;
        private int totalFrames;
        private bool finished;
        private float rotation;

        public Rectangle CollisionHitbox
        {
            get 
            {
            return finished ? Rectangle.Empty : new Rectangle(destinationRectangle.X, destinationRectangle.Y, 4, 7);
            }
            set
            {
            if (!finished) // Only update hitbox if the projectile is active
            destinationRectangle = value;
            }
        }

        public ObjectType ObjectType { get { return ObjectType.EnemyProjectile; } }
        public EnemyProjectileType EnemyProjectileType { get { return EnemyProjectileType.GoryaBoomerang; } }

        public Vector2 Direction { get; set; }
        private bool hasHitWall = false;
        public bool HasHitWall
        {
            get { return hasHitWall; }
            set { hasHitWall = value; }
        }

        public GoryaProjectile(Texture2D texture, Rectangle position, Vector2 direction)
        {
            projectileTexture = texture;
            this.position = position;
            totalFrames = 100;
            currentFrame = 0;
            finished = false;
            center = new Vector2(2f, 3.5f);
            this.sourceRectangle = new Rectangle(285, 4, 4, 7);
            SetDirection(direction);
            this.Direction = direction;


            if (direction.X < 0) // Left
            {
                offset = new Rectangle(0, 20, 0, 0);
                movement = new Vector2(-3, 0);
            }
            else if (direction.X > 0) // Right
            {
                offset = new Rectangle(50, 20, 0, 0);
                movement = new Vector2(3, 0);
            }
            else if (direction.Y < 0) // Up
            {
                offset = new Rectangle(20, -15, 0, 0);
                movement = new Vector2(0, -3);
            }
            else if (direction.Y > 0) // Down
            {
                offset = new Rectangle(25, 60, 0, 0);
                movement = new Vector2(0, 3);
            }
        }

        public void SetDirection(Vector2 direction)
        {
            int centerX = position.Width / 2;
            int centerY = position.Height / 2;

            if (direction.X < 0) // Left
            {
                offset = new Rectangle(centerX - 50, centerY + 5, 0, 0);
                movement.X = -3;
            }
            else if (direction.X > 0) // Right
            {
                offset = new Rectangle(centerX + 20, centerY + 5, 0, 0);
                movement.X = 3;
            }
            else if (direction.Y < 0) // Up
            {
                offset = new Rectangle(centerX + 1, centerY - 45, 0, 0);
                movement.Y = -3;
            }
            else if (direction.Y > 0) // Down
            {
                offset = new Rectangle(centerX + 1, centerY + 20, 0, 0);
                movement.Y = 3;
            }
        }

        public void Update(GameTime gameTime)
        {
            currentFrame++;

            if (currentFrame % 10 == 0)
            {
                rotation += MathHelper.PiOver4;
                rotation %= MathHelper.TwoPi;
            }

            if (currentFrame < totalFrames / 2)
            {
                offset.X += (int)movement.X;
                offset.Y += (int)movement.Y;
            }
            else if (currentFrame < totalFrames * 0.85f)
            {
                offset.X -= (int)movement.X;
                offset.Y -= (int)movement.Y;
            }

            if (currentFrame >= totalFrames * 0.85f)
            {
                finished = true;
            }
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle(position.X + offset.X, position.Y + offset.Y, 16, 28);
            spriteBatch.Draw(projectileTexture, destinationRectangle, sourceRectangle, Color.White, rotation, center, SpriteEffects.None, 0f);
        }

        public bool GetState()
        {
            return finished;
        }
    }
}