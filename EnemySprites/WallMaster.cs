using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.LevelCreation;

namespace Legend_of_the_Power_Rangers
{
    
    public class WallMaster : Enemy, IEnemy
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
        private float speed = 100f;
        private float scale = 2.0f;
        private double timeSinceLastToggle;
        private int currentFrameIndex;
        private const double millisecondsPerToggle = 400;
        private double directionChangeTimer;
        private Random random = new Random();

       public bool isDead { get; set; }
        private bool shouldSpawn = true;
        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;

        private bool isGrabbing = false;
        private Point targetDoor;
        private bool reachedDoor = false;
        
        private const float grabSpeed = 150f;
        private const float proximityThreshold = 100f;
        private const float doorReachThreshold = 300f;

        private readonly Point startPosition = new Point(2530, 4192);
        private readonly Point topDoor = new Point(510, 0);
        private readonly Point bottomDoor = new Point(510, 842);
        private readonly Point leftDoor = new Point(0, 446);
        private readonly Point rightDoor = new Point(970, 446);
        
        private const double fadeDuration = 2000; // 2 seconds for fade-to-black
        private const double maxGrabDuration = 3000; // 3 seconds max grabbing
        private double fadeTimer = 0;
        private float fadeOpacity = 0;
        //private Texture2D screenOverlayTexture;
        

        private double grabTimer = 0;
        private bool isFading = false;

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.WallMaster; } }

        public WallMaster() : base()
        {
            InitializeFrames();
            SetRandomDirection();
            CollisionHitbox = new Rectangle(300, 100, 60, 50); // Default positon
            DestinationRectangle = new Rectangle(300, 100, 60, 50);
            // screenOverlayTexture = new Texture2D(graphicsDevice, 1, 1);
            // screenOverlayTexture.SetData(new[] { Color.White });
            isDead = false;
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
            //float angle = (float)(random.NextDouble() * Math.PI * 2); // Random angle in radians
            //direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            Vector2[] directions = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
            direction = directions[random.Next(directions.Length)];
            //direction.Normalize();
            SetDirection(direction);
        }

        public void SetDirection(Vector2 newDirection)
        {
            direction = newDirection;
            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                currentFrameIndex = (direction.X < 0) ? 0 : 2;
            }
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (shouldSpawn)
            {
                shouldSpawn = false;
                OnSelected(destinationRectangle.X, destinationRectangle.Y);
            }
            
            if (isHurt)
            {
                HandleHurtState(gameTime);
                return;
            }

            if (isGrabbing)
            {
                HandleGrabbingState(gameTime);
            }
            else
            {
                HandleNormalState(gameTime, deltaTime);
            }

            // Update sprite dimensions
            int width = (int)(sourceRectangle[currentFrameIndex].Width * scale);
            int height = (int)(sourceRectangle[currentFrameIndex].Height * scale);
            destinationRectangle.Width = width;
            destinationRectangle.Height = height;
            base.Update(gameTime);
        }

        private void HandleHurtState(GameTime gameTime)
        {
            hurtTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (hurtTimer >= hurtDuration)
            {
                isHurt = false;
                hurtTimer = 0;
            }
        }

        private void HandleGrabbingState(GameTime gameTime)
        {
            grabTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (isFading)
            {
                // Continue fading to black
                fadeTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                fadeOpacity = MathHelper.Clamp((float)(fadeTimer / fadeDuration), 0f, 1f);
                if (fadeTimer >= fadeDuration)
                {
                    CompleteTransition();
                }
                return;
            }

            if (!reachedDoor)
            {
                Vector2 doorPos = targetDoor.ToVector2();
                Vector2 currentPos = new Vector2(destinationRectangle.X, destinationRectangle.Y);
                Vector2 toTarget = doorPos - currentPos;
                float distance = toTarget.Length();

                if (distance > doorReachThreshold && grabTimer < maxGrabDuration) // or if 3 seconds, also add fade to black
                {
                    // Move towards the door
                toTarget.Normalize();
                Vector2 movement = toTarget * grabSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                destinationRectangle.X += (int)movement.X;
                destinationRectangle.Y += (int)movement.Y;
                
                Link link = LinkManager.GetLink();
                    link.destinationRectangle = new Rectangle(
                        destinationRectangle.X, 
                        destinationRectangle.Y,
                        link.destinationRectangle.Width,
                        link.destinationRectangle.Height
                    );
            }
            else
            {
                // Start fade-to-black and prepare transition
                reachedDoor = true;
                StartFade();
            }
            }
        }
        private void StartFade()
        {
            isFading = true;
            fadeTimer = 0; // Reset fade timer
            fadeOpacity = 0f;
        }

        private void CompleteTransition()
        {
            DelegateManager.RaiseChangeToSpecificRoom(1); // Change to the first room
            ResetLinkToStart();
            isGrabbing = false;
            isFading = false;
            grabTimer = 0; // Reset grab timer
            fadeOpacity = 0f;
        }

        private void HandleNormalState(GameTime gameTime, float deltaTime)
        {
            directionChangeTimer += deltaTime;
            if (directionChangeTimer >= 3)
            {
                SetRandomDirection();
                directionChangeTimer = 0;
            }

            // Update animation
            timeSinceLastToggle += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastToggle >= millisecondsPerToggle)
            {
                if (Math.Abs(direction.X) > Math.Abs(direction.Y))
                {
                    currentFrameIndex = (direction.X < 0) ?
                        (currentFrameIndex == 0 ? 1 : 0) :
                        (currentFrameIndex == 2 ? 3 : 2);
                }
                timeSinceLastToggle = 0;
            }

            // Move WallMaster
            destinationRectangle.X += (int)(direction.X * speed * deltaTime);
            destinationRectangle.Y += (int)(direction.Y * speed * deltaTime);

            // Check for Link proximity
            Link link = LinkManager.GetLink();
            float distanceToLink = Vector2.Distance(
                new Vector2(destinationRectangle.X, destinationRectangle.Y),
                new Vector2(link.destinationRectangle.X, link.destinationRectangle.Y)
            );

            if (distanceToLink < proximityThreshold && !isGrabbing)
            {
                StartGrab();
            }
        }

        private void ResetLinkToStart()
        {
            Link link = LinkManager.GetLink();
            DelegateManager.RaiseChangeToSpecificRoom(1);
                    link.destinationRectangle.X = startPosition.X;
                    link.destinationRectangle.Y = startPosition.Y;
            // link.destinationRectangle = new Rectangle(
            //     startPosition.X,
            //     startPosition.Y,
            //     link.destinationRectangle.Width,
            //     link.destinationRectangle.Height
            // );
        }

        public void StartGrab()
        {
            if (!isGrabbing)
            {
                FaceLink();
                
                isGrabbing = true;
                targetDoor = GetNearestDoor();
                reachedDoor = false;
            }
        }

        private void FaceLink()
        {
            Link link = LinkManager.GetLink();

            // Calculate direction vector from WallMaster to Link
            Vector2 linkPosition = new Vector2(link.destinationRectangle.X, link.destinationRectangle.Y);
            Vector2 wallMasterPosition = new Vector2(destinationRectangle.X, destinationRectangle.Y);
            Vector2 directionToLink = linkPosition - wallMasterPosition;

            if (directionToLink.LengthSquared() > 0)
            {
                directionToLink.Normalize();
                SetDirection(directionToLink); // Use method to set direction and animation
            }
        }

        private Point GetNearestDoor()
        {
            Vector2 currentPos = new Vector2(destinationRectangle.X, destinationRectangle.Y);
            Point[] doors = { topDoor, bottomDoor, leftDoor, rightDoor };
            
            float minDistance = float.MaxValue;
            Point nearestDoor = doors[0];

            foreach (Point door in doors)
            {
                float distance = Vector2.Distance(currentPos, door.ToVector2());
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestDoor = door;
                }
            }

            return nearestDoor;
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

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            Color tint = isHurt ? Color.Red : Color.White;

            if (!isGrabbing)
            {
                // Draw normally
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], tint);
            }
            else
            {
                // toDo: When grabbing, draw WallMaster first (behind Link)
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], tint);
            }
            if (IsSpawning || IsDying)
            {
                base.Draw(texture, spriteBatch);
            }
            // if (isFading) // Removed because of glitch
            // {
            //     // Overlay a black rectangle with adjustable opacity
            //     spriteBatch.Draw(
            //         texture: null, // Or use a single-pixel texture
            //         destinationRectangle: new Rectangle(0, 0, 1020, 892),
            //         color: new Color(0, 0, 0, fadeOpacity)
            //     );
            // }

        }
    }
}