using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class GelSmallBlack : ISprite
    {
        //private Texture2D texture;
        private Rectangle[] sourceRectangle;
        private Rectangle destinationRectangle;
        private int currentFrameIndex;
        private Vector2 direction;
        private float scale = 2.0f;
        private double timeSinceLastToggle;
        private const double millisecondsPerToggle = 200;
        private float speed = 33f;
        private double directionChangeTimer;
        private int frameIndex1;
        private int frameIndex2;
        private Random random = new Random();
        private Vector2 position;
        Vector2 initialPosition  = new Vector2(100, 100);

        public GelSmallBlack()
        {
            //this.texture = spritesheet;
            this.position = initialPosition;
            InitializeFrames();
            SetRandomDirection();
            UpdateDestinationRectangle();
        }
        private void InitializeFrames()
        {
            sourceRectangle = new Rectangle[50];
            int xOffset = 415;
            sourceRectangle[0] = new Rectangle(xOffset, 175, 22, 18); // Frame 1
            sourceRectangle[1] = new Rectangle(xOffset, 205, 22, 18); // Frame 2
        }
        private void SetRandomDirection()
        {
            Vector2[] directions = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
            direction = directions[random.Next(directions.Length)];
            SetDirection(direction);
        }
        public void SetDirection(Vector2 direction)
        {
            int directionIndex = 0;
            if (direction.X < 0) directionIndex = 2;
            else if (direction.Y < 0) directionIndex = 4;
            else if (direction.X > 0) directionIndex = 6;

            frameIndex1 = directionIndex;
            frameIndex2 = frameIndex1 + 16; // 16 is distance between sprites
            currentFrameIndex = frameIndex1;
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
                currentFrameIndex = (currentFrameIndex + 1) % 2; // % sourceRectangle.Length
                timeSinceLastToggle = 0;
            }
            position += direction * (float)gameTime.ElapsedGameTime.TotalSeconds * speed;
            UpdateDestinationRectangle();
        }
        private void UpdateDestinationRectangle()
        {
            int width = (int)(sourceRectangle[currentFrameIndex].Width * scale);
            int height = (int)(sourceRectangle[currentFrameIndex].Height * scale);
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], Color.White);
        }
    }
}
