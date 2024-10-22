using System;
using Legend_of_the_Power_Rangers.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class WallMaster : IEnemy
    {
        //private Texture2D texture;
        private Rectangle[] sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        private int currentFrameIndex;
        private Vector2 direction;
        private float scale = 2.0f;
        private double timeSinceLastToggle;
        private const double millisecondsPerToggle = 400;
        private float speed = 33f;
        private double directionChangeTimer;
        private Random random = new Random();
        private Vector2 position;
        Vector2 initialPosition = new Vector2(300, 200);

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.WallMaster; } }

        public WallMaster()
        {
            //this.texture = spritesheet;
            this.position = initialPosition;
            InitializeFrames();
            SetRandomDirection();
            UpdateDestinationRectangle();
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
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateDestinationRectangle();
        }
        private void UpdateDestinationRectangle()
        {
            int width = (int)(sourceRectangle[currentFrameIndex].Width * scale);
            int height = (int)(sourceRectangle[currentFrameIndex].Height * scale);
            destinationRectangle = new Rectangle(destinationRectangle.X, (int)destinationRectangle.Y, width, height);
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], Color.White);
        }
    }
}