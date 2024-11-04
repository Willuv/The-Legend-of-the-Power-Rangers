using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class WallMaster : Enemy, IEnemy
    {
        private Rectangle[] sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        private Vector2 direction;
        private float speed = 100f;
        private float scale = 2.0f;
        private double timeSinceLastToggle;
        private int currentFrameIndex;
        private const double millisecondsPerToggle = 400;
        private double directionChangeTimer;
        private Random random = new Random();
        
        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.WallMaster; } }

        public WallMaster()
        {
            InitializeFrames();
            SetRandomDirection();
            DestinationRectangle = new Rectangle(300, 100, 60, 50); // Default positon
        }

        private void InitializeFrames()
        {
            sourceRectangle = new Rectangle[4];
            int xOffset = 235;
            int xIncrease = 30;

            // Left direction
            sourceRectangle[0] = new Rectangle(xOffset, 0, 30, 25);
            sourceRectangle[1] = new Rectangle(xOffset, xIncrease, 30, 25);

            // Right direction
            sourceRectangle[2] = new Rectangle(xOffset + xIncrease, 0, 30, 18);
            sourceRectangle[3] = new Rectangle(xOffset + xIncrease, xIncrease, 30, 18);
        }

        private void SetRandomDirection()
        {
            Vector2[] directions = { new Vector2(1, 0), new Vector2(-1, 0) };
            direction = directions[random.Next(directions.Length)];
            SetDirection(direction);
        }

        public void SetDirection(Vector2 newDirection)
        {
            direction = newDirection;
            if (direction.X < 0)
            {
                // Left
                currentFrameIndex = 0;
            }
            else if (direction.X > 0)
            {
                // Right 
                currentFrameIndex = 2;
            }
        }

        public void Update(GameTime gameTime)
        {
            directionChangeTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (directionChangeTimer >= 3) // ChangeDirrection every 3sec
            {
                SetRandomDirection();
                directionChangeTimer = 0;
            }
            timeSinceLastToggle += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastToggle >= millisecondsPerToggle)
            {
                if (direction.X < 0)
                {
                    currentFrameIndex = currentFrameIndex == 0 ? 1 : 0;  // Toggle left frames
                }
                else if (direction.X > 0)
                {
                    currentFrameIndex = currentFrameIndex == 2 ? 3 : 2;  // Toggle right frames
                }
                timeSinceLastToggle = 0;
            }
            int width = (int)(sourceRectangle[currentFrameIndex].Width * scale);
            int height = (int)(sourceRectangle[currentFrameIndex].Height * scale);
            destinationRectangle = new Rectangle((int)destinationRectangle.X, (int)destinationRectangle.Y, width, height);
            // Update destinationRectangle based on direction and speed
            destinationRectangle.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            destinationRectangle.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);
        }
        int Health = 1;
        public void TakeDamage(int damage = 1)
        {
            Health -= damage;
            if (Health <= 0)
            {
                TriggerDeath(destinationRectangle.X, destinationRectangle.Y);
            }
        }
        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], Color.White);
        }
    }
}