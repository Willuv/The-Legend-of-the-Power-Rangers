using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Legend_of_the_Power_Rangers.Portals;

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

        public bool HasBeenCounted { get; set; } = false;
        private Vector2 direction;
        private bool isFacingLeft = true;
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

        public bool isDead { get; set; }
        private bool shouldSpawn = true;
        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;
        private int health = 6;

        private BluePortal currentLocationPortal;
        private OrangePortal newLocationPortal;
        private bool isTeleporting = false;
        private double teleportTimer = 0.0;
        private const double portalDuration = 1.5;

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.DragonBoss; } }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public DragonBoss(Texture2D spritesheet, Texture2D projectileTexture) : base()
        {
            bossSpritesheet = spritesheet;
            this.projectileTexture = projectileTexture;
            
            CollisionHitbox = new Rectangle(300, 100, bossSpriteWidth * scale, bossSpriteHeight * scale); // Default positon
            projectileSourceRectangle = new Rectangle(330, 0, spriteWidth, spriteHeight); // Coordinates and size for projectile
            
            SetRandomDirection();
            InitializeFrames();
            IsDragonBoss = true;
            isDead = false;
            projectiles = new List<Tuple<DragonProjectile, Vector2>>();
            currentLocationPortal = new BluePortal();
            newLocationPortal = new OrangePortal();
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
        private bool oneTime4HPDirection = false;
        private bool oneTime2HPDirection = false;
        private void SetRandomDirection()
        {
            if (Health == 4 && !oneTime4HPDirection)
            {
                direction = new Vector2(1, 0); // right
                oneTime4HPDirection = true;
            }
            else if (Health == 2 && !oneTime2HPDirection)
            {
                direction = new Vector2(-1, 0); // Left
                oneTime2HPDirection = true;
            }
            else
            {
            Vector2[] directions = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
            direction = directions[random.Next(directions.Length)];
            }
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
            if (isTeleporting)
            {
                teleportTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (teleportTimer >= portalDuration)
                {
                    isTeleporting = false;
                }
            }

            directionChangeTimer += gameTime.ElapsedGameTime.TotalSeconds;
            ScreenShakeManager.Update(gameTime); // ScreenShaker not Working
            if (directionChangeTimer >= 3) // ChangeDirrection every 3sec
            {
                SetRandomDirection();
                directionChangeTimer = 0;
            }

            timeSinceLastToggle += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastToggle >= millisecondsPerToggle)
            {
                currentFrameIndex = (currentFrameIndex + 1) % sourceRectangle.Length;
                timeSinceLastToggle = 0;
            }

            // Update destinationRectangle based on direction and speed
            destinationRectangle.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            destinationRectangle.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);
            UpdateProjectiles(gameTime);
            base.Update(gameTime);
        }
        private void UpdateProjectiles(GameTime gameTime)
        {
            if (Health <= 0) return;

            projectileFireTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (projectileFireTimer >= projectileFireInterval)
            {
                ShootProjectiles();
                projectileFireTimer = 0; // Reset timer
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                var projectile = projectiles[i];

                if (projectile.Item1.HasHitWall)
                {
                    projectiles[i].Item1.CollisionHitbox = Rectangle.Empty;
                    projectiles.RemoveAt(i);
                }
                else
                {
                    projectile.Item1.Update(gameTime);
                }
            }
        }

        private bool alternation2HPShootDirection = false;
        private void ShootProjectiles()
        {   
        Vector2[] directions = { new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1) };

        if (Health <= 4) directions = new Vector2[] { new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(1, 1), new Vector2(1, 0) }; // Added 4 corners and right
        if (Health <= 2) {
            if (!alternation2HPShootDirection){
                directions = new Vector2[] { new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(-1, 1.5f),new Vector2(-1, -1.5f), new Vector2(-1, 0.5f), new Vector2(-1, -0.5f), new Vector2(0,1), new Vector2(0,-1)}; // Added up and down, removed left and right and right 2 corners
                alternation2HPShootDirection = true;
            } else {
                directions = new Vector2[] { new Vector2(-0.7f, -1), new Vector2(-1, 0.5f), new Vector2(0, 1), new Vector2(1, 0.3f), new Vector2(1, -0.5f)};
                alternation2HPShootDirection = false;
            }
        }

        foreach (var direction in directions)
        {
            direction.Normalize(); // Normalize for consistent speed in all directions
            DragonProjectile projectile = new DragonProjectile(projectileTexture, projectileSourceRectangle);
            DelegateManager.RaiseObjectCreated(projectile);
            
            if (isFacingLeft){ // Ensure projectiles spawn from Dragonbosses horn shooter
            projectile.Position = new Vector2(destinationRectangle.X + xOffset - 13, destinationRectangle.Y + yOffset - 13); // Start at boss's position w/ Offset
            } else {
            projectile.Position = new Vector2(destinationRectangle.X + xOffset + 83, destinationRectangle.Y + yOffset - 13); // Start at boss's position w/ Offset
            }

            projectile.Direction = direction; // Set the projectile movement direction
            projectiles.Add(new Tuple<DragonProjectile, Vector2>(projectile, projectile.Position));
        }
    }
        private void DrawHealthBar(SpriteBatch spriteBatch)
        {
            int healthBarWidth = destinationRectangle.Width; // Width of the health bar
            int healthBarHeight = 10; // Height of the health bar
            int healthBarYOffset = 10; // Offset above the boss
            int currentHealthBarWidth = (int)((float)Health / 6 * healthBarWidth); // Scaled bar on health
            // Gray Background
            Rectangle backgroundBar = new Rectangle( destinationRectangle.X, destinationRectangle.Y - healthBarYOffset - healthBarHeight, healthBarWidth, healthBarHeight);
            spriteBatch.Draw(CreateSolidColorTexture(Color.Gray), backgroundBar, Color.Gray);

            // Black Health Bar
            Rectangle foregroundBar = new Rectangle(destinationRectangle.X, destinationRectangle.Y - healthBarYOffset - healthBarHeight, currentHealthBarWidth, healthBarHeight);
            spriteBatch.Draw(CreateSolidColorTexture(Color.Black), foregroundBar, Color.Black);
        }
        private Texture2D CreateSolidColorTexture(Color color)
        {
            Texture2D texture = new Texture2D(bossSpritesheet.GraphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }
        private Color GetBossColor()
        {
            if (Health > 4) // More than 2/3 health
                return Color.White;
            else if (Health > 2) // Between 1/3 and 2/3 health
                return Color.Orange;
            else // 1/3 health or less
                return Color.Red;
        }

        private void PhaseChange()
        {
            //SoundManager.PlaySound(SoundType.BossYell); // Not Added
            if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Boss_Scream1");
            
            if (Health == 4){
                TeleportTo(new Vector2(4228, 1209));
                if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Candle");
            } 
            if (Health == 2){
                TeleportTo(new Vector2(4720, 1209));
                if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Candle");
            } 

            // Trigger screen shake
            ScreenShakeManager.TriggerShake(intensity: 15, duration: 5); // Does not work

            speed += 20f; // Slight speed increase on PhaseChange()
        }
        public void TeleportTo(Vector2 newPosition)
        {
            // Set up Blue portal
            currentLocationPortal.CollisionHitbox = new Rectangle(destinationRectangle.X, destinationRectangle.Y, destinationRectangle.Width, destinationRectangle.Height);

            // Set up Orange portal
            newLocationPortal.CollisionHitbox = new Rectangle((int)newPosition.X, (int)newPosition.Y, destinationRectangle.Width, destinationRectangle.Height);

            // Trigger isTeleporting
            isTeleporting = true;
            teleportTimer = 0.0;

            destinationRectangle.X = (int)newPosition.X;
            destinationRectangle.Y = (int)newPosition.Y;

            // Flip
            isFacingLeft = !isFacingLeft;
            SetRandomDirection();
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            if (isTeleporting)
            {
                currentLocationPortal.Draw(spriteBatch);
                newLocationPortal.Draw(spriteBatch);
            }

            Color tint = GetBossColor();
            if (isHurt) tint = Color.Lerp(tint, Color.Red, 0.5f); // Blend with red when hurt
            SpriteEffects spriteEffect = isFacingLeft ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(bossSpritesheet, destinationRectangle, sourceRectangle[currentFrameIndex], tint, 0f, Vector2.Zero, spriteEffect, 0f); // Draw the boss
            DrawHealthBar(spriteBatch);
            if (IsSpawning || IsDying)
            {
                base.Draw(texture, spriteBatch);
            }
            
            foreach (var projectile in projectiles) // Draw projectiles
            {
                projectile.Item1.Draw(texture, spriteBatch);
            }
        }

        public void TakeDamage(int damage = 1)
        {
            isHurt = true;
            Health -= damage;
            if (Health <= 0)
            {
                for (int i = projectiles.Count - 1; i >= 0; i--)
                {   // Clear the projectiles that are left
                    projectiles[i].Item1.CollisionHitbox = Rectangle.Empty;
                    projectiles.RemoveAt(i);
                }
                isDead = true;
                projectiles.Clear();
                TriggerDeath(destinationRectangle.X, destinationRectangle.Y);
                this.destinationRectangle.Width = 0;
                this.destinationRectangle.Height = 0;
            }
            else
            {
                hurtTimer = 0;
                if (Health == 4 || Health == 2)
                {
                    PhaseChange();
                }
            }
        }

        public bool IsHurt()
        {
            return isHurt;
        }
    }
}
