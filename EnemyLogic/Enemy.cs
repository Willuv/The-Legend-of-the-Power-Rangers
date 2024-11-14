using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class Enemy
    {
        public Rectangle DestinationRectangle { get; set; }
        private Rectangle[] sourceRectangles;
        public bool IsAlive { get; set; } = true;
        protected bool IsSpawning { get; set; } = true;
        protected bool IsDying { get; set; }
        public bool IsDragonBoss { get; set; } = false;

        private const int DefaultScale = 1;
        private const int DragonBossScale = 4;
        private const int DragonBossXOffset = -65;
        private const int DragonBossYOffset = -65;

        private double frameDisplayTime = 500; // Time between frames
        private double totalFrameTime = 0;
        private int currentFrameIndex = 0;

        public Enemy()
        {
            InitializeFrames();
            DestinationRectangle = new Rectangle(DestinationRectangle.X, DestinationRectangle.Y, sourceRectangles[0].Width * 3 * DefaultScale, sourceRectangles[0].Height * 3 * DefaultScale);  // X/Y are reset later
        }
        private void InitializeFrames()
        {
            sourceRectangles = new Rectangle[]
            {
                new Rectangle(390, 235, 25, 25), // Spawn frame 1
                new Rectangle(387, 265, 25, 25), // Spawn frame 2
                new Rectangle(415, 235, 25, 25), // Death frame 1
                new Rectangle(415, 265, 22, 25)  // Death frame 2
            };
        }

        public void OnSelected(int X, int Y)
        {
            IsSpawning = true;
            IsAlive = true;
            currentFrameIndex = 0;  // Start at spawn frame 1

            int scale = IsDragonBoss ? DragonBossScale : DefaultScale;
            int adjustedX = IsDragonBoss ? X + DragonBossXOffset : X;
            int adjustedY = IsDragonBoss ? Y + DragonBossYOffset : Y;
            DestinationRectangle = new Rectangle(adjustedX, adjustedY - 25, sourceRectangles[0].Width * 3 * scale, sourceRectangles[0].Height * 3 * scale);  // X-8,Y-25 center the animation (removed - 8, looked like not needed)
        }

        public void TriggerDeath(int X, int Y)
        {
            IsAlive = false;
            IsDying = true;
            currentFrameIndex = 2;  // Start at death frame 1

            int scale = IsDragonBoss ? DragonBossScale : DefaultScale;
            int adjustedX = IsDragonBoss ? X + DragonBossXOffset : X;
            int adjustedY = IsDragonBoss ? Y + DragonBossYOffset : Y;
            DestinationRectangle = new Rectangle(adjustedX - 8, adjustedY - 25, sourceRectangles[2].Width * 3 * scale, sourceRectangles[2].Height * 3 * scale);  // // X-8/Y-25 center the animation
            if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Enemy_Die");
        }

        //testing purposes from Jake
        // public void TriggerDeath()
        // {
        //     IsAlive = false;
        //     IsDying = true;
        //     currentFrameIndex = 2;  // Start at death frame 1
        //     int X = DestinationRectangle.X;
        //     int Y = DestinationRectangle.Y;
        //     DestinationRectangle = new Rectangle(X - 8, Y - 25, sourceRectangles[2].Width * 3, sourceRectangles[2].Height * 3);  // // X-8/Y-25 center the animation
        // }

        public void Update(GameTime gameTime)
        {
            totalFrameTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (IsSpawning)
            {
                if (totalFrameTime >= frameDisplayTime)
                {
                    totalFrameTime = 0;
                    currentFrameIndex++;

                    // Stop after frame 0 and 1
                    if (currentFrameIndex >= 2)
                    {
                        IsSpawning = false; // Spawning done
                        currentFrameIndex = 0;  // Reset frame (maybe needed)
                    }
                }
            }
            else if (IsDying)
            {
                if (totalFrameTime >= frameDisplayTime)
                {
                    totalFrameTime = 0;
                    currentFrameIndex++;

                    // Stop after frame 2 and 3
                    if (currentFrameIndex > 3)
                    {
                        currentFrameIndex = 3;
                        IsDying = false;  // Dying done
                        IsDragonBoss = false;
                    }
                }
            }
        }
        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DestinationRectangle, sourceRectangles[currentFrameIndex], Color.White);
        }
    }
}
