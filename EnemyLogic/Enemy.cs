using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class Enemy : ISprite
    {
        public Vector2 position;
        //protected Texture2D texture;
        protected Rectangle[] sourceRectangles;
        protected int currentFrameIndex = 0;
        private double millisecondsPerToggle = 200;
        private double timeSinceLastFrame = 0;
        public bool IsAlive { get; set; } = true;
        private bool isSpawning = true;
        private bool hasBeenUpdated = false;

        public Enemy(Texture2D texture)
        {
            //this.texture = texture;
            InitializeFrames();
        }

        private void InitializeFrames()
        {
            sourceRectangles = new Rectangle[]
            {
                new Rectangle(400, 240, 16, 16), // Spawn frame 1
                new Rectangle(400, 256, 16, 16), // Spawn frame 2
                new Rectangle(415, 240, 16, 16), // Death frame 1
                new Rectangle(415, 256, 16, 16)  // Death frame 2
            };
            currentFrameIndex = 0; // Start w/ frame one of spawn
        }

        public void Update(GameTime gameTime)
        {
            if (!hasBeenUpdated)
            {
                // Start spawn animation
                isSpawning = true;
                currentFrameIndex = 0;
                hasBeenUpdated = true;
            }

            if (isSpawning)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeSinceLastFrame >= millisecondsPerToggle)
                {
                    timeSinceLastFrame = 0;
                    currentFrameIndex++;
                    if (currentFrameIndex >= 2) 
                    {
                        isSpawning = false;
                        currentFrameIndex = 0; // Reset
                    }
                }
            }
            else if (!IsAlive)
            {
                HandleDeathAnimation(gameTime);
            }
        }

        private void HandleDeathAnimation(GameTime gameTime)
        {
            // Death animation logic
            if (currentFrameIndex < 2 || currentFrameIndex > 3)
                currentFrameIndex = 2;
            
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastFrame >= millisecondsPerToggle)
            {
                timeSinceLastFrame = 0;
                currentFrameIndex++;
                if (currentFrameIndex > 3)  // End of death animation
                {
                    currentFrameIndex = 3;  // Hold on last frame
                }
            }
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            if (IsAlive)
                spriteBatch.Draw(texture, position, sourceRectangles[currentFrameIndex], Color.White);
        }

        public void TriggerDeath()
        {
            IsAlive = false;
            currentFrameIndex = 2; // Start death animation
        }
        public void OnSelected()
        {
            isSpawning = true; 
            currentFrameIndex = 0;
        }
    }
}