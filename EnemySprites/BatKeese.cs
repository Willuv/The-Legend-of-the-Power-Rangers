using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class BatKeese : Enemy, IEnemy
    {
        private Rectangle[] sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        public bool HasBeenCounted { get; set; } = false;
        private int currentFrameIndex;
        private Vector2 direction;
        private float speed = 100f;
        private float scale = 1.5f;

        private double timeSinceLastToggle;
        private const double millisecondsPerToggle = 100;
        private double directionChangeTimer;
        private int frameIndex1;
        private int frameIndex2;
        private Random random = new Random();
        
        public bool isDead { get; set; }
        private bool shouldSpawn = true;
        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.BatKeese; } }


        public BatKeese() : base()
        {
            CollisionHitbox = new Rectangle(300, 100, 88, 60); // Default positon
            InitializeFrames();
            SetRandomDirection();
            isDead = false;
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
            if (shouldSpawn)
            {
                shouldSpawn = false;
                OnSelected(destinationRectangle.X, destinationRectangle.Y);
            }
            
            if (isHurt)
            {
                hurtTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (hurtTimer >= hurtDuration)
                {
                    isHurt = false;
                    hurtTimer = 0;
                }
            }

            // Update direction change timer
            directionChangeTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (directionChangeTimer >= 3)
            {
                SetRandomDirection();
                directionChangeTimer = 0;
            }

            // Update animation frame
            timeSinceLastToggle += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastToggle >= millisecondsPerToggle)
            {
                currentFrameIndex = (currentFrameIndex + 1) % 2;
                timeSinceLastToggle = 0;
            }

            // Update position based on direction and speed
            destinationRectangle.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            destinationRectangle.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            Color tint = isHurt ? Color.Red : Color.White;
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], tint);
            if (IsSpawning || IsDying)
            {
                base.Draw(texture, spriteBatch);
            }
        }

        int Health = 2;
        public void TakeDamage(int damage = 1)
        {
            isHurt = true;
            Health -= damage;
            if (Health <= 0)
            {
                isDead = true;
                TriggerDeath(destinationRectangle.X, destinationRectangle.Y);
                this.destinationRectangle.Width = 0;
                this.destinationRectangle.Height = 0;
                
            }
            else
            {
                hurtTimer = 0;
            }
        }

        public bool IsHurt()
        {
            return isHurt;
        }
    }
}
