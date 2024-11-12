using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Legend_of_the_Power_Rangers
{
    public class DragonBoss : Enemy, IEnemy
    {
        private Rectangle[] sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set
            {
                destinationRectangle = value;
            }
        }

        private Vector2 direction;
        int scale = 3;
        private float speed = 100f;

        public Texture2D bossSpritesheet;
        private Texture2D projectileTexture;
        private Rectangle projectileSourceRectangle;
        private List<Tuple<DragonProjectile, Vector2>> projectiles;
        private double projectileFireTimer = 0;
        private double projectileFireInterval = 1;
       
        private double timeSinceLastToggle;
        private const double millisecondsPerToggle = 200;
        private double directionChangeTimer;
        private int frameIndex1;
        private int frameIndex2;
        private int currentFrameIndex;
        private Random random = new Random();

        int xOffset = 0;
        int yOffset = 0;
        // Width and Height for Projectile
        int spriteWidth = 15;
        int spriteHeight = 15;
        // Width and Height for DragonBoss
        int bossSpriteWidth = 40;
        int bossSpriteHeight = 40;

        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.DragonBoss; } }

        public DragonBoss(Texture2D spritesheet, Texture2D projectileTexture)
        {
            bossSpritesheet = spritesheet;
            this.projectileTexture = projectileTexture;
            
            CollisionHitbox = new Rectangle(300, 100, bossSpriteWidth * scale, bossSpriteHeight * scale); // Default positon
            projectileSourceRectangle = new Rectangle(330, 0, spriteWidth, spriteHeight); // Specific coordinates and size for projectile
            
            SetRandomDirection();
            InitializeFrames();
            projectiles = new List<Tuple<DragonProjectile, Vector2>>();
            OnSelected(destinationRectangle.X, destinationRectangle.Y);
        }

        private void InitializeFrames()
        {
            sourceRectangle = new Rectangle[4]; // 4 frames
            int[] xOffsets = {0, 41, 81, 130}; // Specific offsets for each frame
            int yOffset = 0;

            for (int i = 0; i < sourceRectangle.Length; i++)
            {
                sourceRectangle[i] = new Rectangle(xOffsets[i], yOffset, bossSpriteWidth, bossSpriteHeight);
            }
        }
        private void SetRandomDirection()
        {
            Vector2[] directions = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
            direction = directions[random.Next(directions.Length)];
            SetDragonDirection(direction);
        }
        public void SetDragonDirection(Vector2 direction)
        {
            int directionIndex;
            if (direction.X < 0) directionIndex = 1;
            else if (direction.Y < 0) directionIndex = 0;
            else if (direction.X > 0) directionIndex = 2;
            else directionIndex = 3;

            frameIndex1 = directionIndex;
            frameIndex2 = frameIndex1; // Duplicate Frame1
            currentFrameIndex = frameIndex1; //% sourceRectangle.Length;
        }
        public void Update(GameTime gameTime)
        {
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
                // if (currentFrameIndex == frameIndex1)
                //     currentFrameIndex = frameIndex2;
                // else
                //     currentFrameIndex = frameIndex1;
                currentFrameIndex = (currentFrameIndex + 1) % sourceRectangle.Length;
                timeSinceLastToggle = 0;
            }

            // Update destinationRectangle based on direction and speed
            destinationRectangle.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            destinationRectangle.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);
            UpdateProjectiles(gameTime);
        }
        private void UpdateProjectiles(GameTime gameTime)
        {
            projectileFireTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (projectileFireTimer >= projectileFireInterval)
            {
                ShootProjectiles();
                projectileFireTimer = 0; // Reset timer
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                var projectile = projectiles[i];
                projectile.Item1.Update(gameTime); // Update the projectile
            }
        }

        private void ShootProjectiles()
    {
        Vector2[] directions = { new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1) };
        foreach (var direction in directions)
        {
            direction.Normalize(); // Normalize for consistent speed in all directions
            DragonProjectile projectile = new DragonProjectile(projectileTexture, projectileSourceRectangle);
            DelegateManager.RaiseObjectCreated(projectile);
            //alex please add a way for projectiles to disappear after time - Jake
            projectile.Position = new Vector2(destinationRectangle.X + xOffset - 13, destinationRectangle.Y + yOffset - 13); // Start at boss's position w/ Offset
            projectile.Direction = direction; // Set the movement direction
            projectiles.Add(new Tuple<DragonProjectile, Vector2>(projectile, projectile.Position));
        }
    }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bossSpritesheet, destinationRectangle, sourceRectangle[currentFrameIndex], Color.White); // Draw boss
            foreach (var projectile in projectiles) // Draw projectiles
            {
                projectile.Item1.Draw(texture, spriteBatch);
            }
        }

        int Health = 1;
        public void TakeDamage(int damage = 1)
        {
            Health -= damage;
            if (Health <= 0)
            {
                isHurt = true;
                TriggerDeath(destinationRectangle.X, destinationRectangle.Y);
            }
            else
            {
                isHurt = true;
                hurtTimer = 0;
            }
        }

        public bool IsHurt()
        {
            return isHurt;
        }
    }
}
