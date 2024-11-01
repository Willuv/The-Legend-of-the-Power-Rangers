using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class BatKeese : Enemy, IEnemy
    {
        private Rectangle[] sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        private int currentFrameIndex;
        private Vector2 direction;
         private float speed = 100f;
        //private float scale = 2.0f;

        private double timeSinceLastToggle;
        private const double millisecondsPerToggle = 100;
        private double directionChangeTimer;
        private int frameIndex1;
        private int frameIndex2;
        private Random random = new Random();

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.BatKeese; } }

        public BatKeese() : base()
        {
            DestinationRectangle = new Rectangle(300, 100, 44, 30); // Default positon
            OnSelected(destinationRectangle.X, destinationRectangle.Y);
            InitializeFrames();
            SetRandomDirection();
            
        }
        private void InitializeFrames()
        {
            sourceRectangle = new Rectangle[50];
            int yOffset = 270;
            sourceRectangle[0] = new Rectangle(230, yOffset, 22, 15); // Frame 1
            sourceRectangle[1] = new Rectangle(255, yOffset, 22, 15); // Frame 2
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
            // Update destinationRectangle based on direction and speed
            destinationRectangle.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            destinationRectangle.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }
        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], Color.White);
            if (IsSpawning || IsDying)
            {
                base.Draw(texture, spriteBatch);
            }
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
    }
}