using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class RedOcto : IEnemy
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
        private int scale = 2;
        
        private double timeSinceLastToggle;
        private const double millisecondsPerToggle = 200;
        private double directionChangeTimer;
        private int frameIndex1;
        private int frameIndex2;
        private int currentFrameIndex;
        private Random random = new Random();
        
        private List<OctoProjectile> projectiles;
        private double projectileTimer;
        private const double projectileInterval = 2.0; // Shoot every 2 seconds
        private Texture2D projectileTexture;

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.RedOcto; } }

        public RedOcto(Texture2D projectileTexture, Rectangle? spawnRectangle = null)
        {
            this.projectileTexture = projectileTexture;
            InitializeFrames();
            SetRandomDirection();
            projectiles = new List<OctoProjectile>();
            if (spawnRectangle.HasValue)
            {
                DestinationRectangle = spawnRectangle.Value;
            }
            else
            {
                DestinationRectangle = new Rectangle(300, 100, 15 * scale, 15 * scale); // Default position
            }
        }

        private void InitializeFrames()
        {
            sourceRectangle = new Rectangle[64];
            int xOffset = 0;
            int yOffset = 0;
            int spriteWidth = 15;
            int spriteHeight = 15;
            for (int direction = 0; direction < 4; direction++)
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

        public void SetDirection(Vector2 newDirection)
        {
            direction = newDirection;
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
                if (currentFrameIndex == frameIndex1)
                    currentFrameIndex = frameIndex2;
                else
                    currentFrameIndex = frameIndex1;
                timeSinceLastToggle = 0;
            }

            // Update destinationRectangle based on direction and speed
            destinationRectangle.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            destinationRectangle.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);
            // Update projectiles
            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
            projectiles.RemoveAll(p => p.GetState());

            // Fire projectile
            projectileTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (projectileTimer >= projectileInterval)
            {
                FireProjectile();
                projectileTimer = 0;
            }
        }
        private void FireProjectile()
        {
            var projectileRectangle = new Rectangle(destinationRectangle.X, destinationRectangle.Y, 15, 7);
            OctoProjectile projectile = new OctoProjectile(projectileTexture, projectileRectangle, direction);
            projectiles.Add(projectile);
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], Color.White);

            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
    }
}