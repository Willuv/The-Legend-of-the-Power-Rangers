using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class BlueGorya : Enemy, IEnemy
    {
        private Rectangle[] sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        public bool HasBeenCounted { get; set; } = false;
        private Vector2 direction;
        private float speed = 125f;
        //private float scale = 2.0f;

        private double timeSinceLastToggle;
        private const double millisecondsPerToggle = 200;
        private double directionChangeTimer;
        private int frameIndex1;
        private int frameIndex2;
        private int currentFrameIndex;
        private Random random = new Random();

        public bool isDead { get; set; }
        private bool shouldSpawn = true;
        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.BlueGorya; } }

        public BlueGorya() : base()
        {
            CollisionHitbox = new Rectangle(300, 100, 64, 64); // Default positon
            InitializeFrames();
            SetRandomDirection();
            isDead = false;
        }
        private void InitializeFrames()
        {
            sourceRectangle = new Rectangle[64];
            int xOffset = 120;
            int yOffset = 60;
            int spriteWidth = 15;
            int spriteHeight = 15;
            for (int direction = 0; direction < 4; direction++) // 4 directions
            {
                for (int i = 0; i < 16; i++)
                {
                    int baseIndex = direction * 8 + i;
                    sourceRectangle[baseIndex] = new Rectangle(
                        xOffset + i * spriteWidth,
                        yOffset + direction * spriteHeight,
                        spriteWidth,
                        spriteHeight);
                }
            }
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
            
            directionChangeTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (directionChangeTimer >= 3) // ChangeDirrection every 3sec
            {
                SetRandomDirection();
                directionChangeTimer = 0;
            }
            
            timeSinceLastToggle += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastToggle >= millisecondsPerToggle)
            {
                if (currentFrameIndex == frameIndex1)
                    currentFrameIndex = frameIndex2;
                else
                    currentFrameIndex = frameIndex1;

                timeSinceLastToggle = 0;
            }

            // Update destinationRectangle based on direction and speed
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