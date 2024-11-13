using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Legend_of_the_Power_Rangers;

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

        private Vector2 direction;
        private float speed = 100f;
        private float scale = 2.0f;
        private double timeSinceLastToggle;
        private int currentFrameIndex;
        private const double millisecondsPerToggle = 400;
        private double directionChangeTimer;
        private Random random = new Random();

        private bool shouldSpawn = true;
        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;

        private bool isGrabbing = false;
        private Point targetDoor;
        private bool reachedDoor = false;
        
        private const float grabSpeed = 150f;
        private const float proximityThreshold = 50f;
        private const float doorReachThreshold = 10f;

        private readonly Point startPosition = new Point(100, 100);
        private readonly Point topDoor = new Point(510, 0);
        private readonly Point bottomDoor = new Point(510, 842);
        private readonly Point leftDoor = new Point(0, 446);
        private readonly Point rightDoor = new Point(970, 446);
        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.WallMaster; } }

        public WallMaster()
        {
            InitializeFrames();
            SetRandomDirection();
            CollisionHitbox = new Rectangle(300, 100, 60, 50); // Default positon
            DestinationRectangle = new Rectangle(300, 100, 60, 50);
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
            float angle = (float)(random.NextDouble() * Math.PI * 2); // Random angle in radians
            direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            direction.Normalize();
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
                HandleGrabbingState(deltaTime);
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

        private void HandleGrabbingState(float deltaTime)
        {
            if (!reachedDoor)
            {
                Vector2 doorPos = targetDoor.ToVector2();
                Vector2 currentPos = new Vector2(destinationRectangle.X, destinationRectangle.Y);
                Vector2 toTarget = doorPos - currentPos;
                float distance = toTarget.Length();

                if (distance > doorReachThreshold)
                {
                    toTarget.Normalize();
                    Vector2 movement = toTarget * grabSpeed * deltaTime;
                    destinationRectangle.X += (int)movement.X;
                    destinationRectangle.Y += (int)movement.Y;

                    // Update Link's position to match WallMaster
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
                    reachedDoor = true;
                    ResetLinkToStart();
                    isGrabbing = false;
                }
            }
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

            if (distanceToLink < proximityThreshold)
            {
                StartGrab();
            }
        }

        private void ResetLinkToStart()
        {
            Link link = LinkManager.GetLink();
            link.destinationRectangle = new Rectangle(
                startPosition.X,
                startPosition.Y,
                link.destinationRectangle.Width,
                link.destinationRectangle.Height
            );
        }

        public void StartGrab()
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
                targetDoor = GetNearestDoor();
                reachedDoor = false;
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
        int Health = 1;
        public void TakeDamage(int damage = 1)
        {
            Health -= damage;
            if (Health <= 0)
            {
                isHurt = true;
                TriggerDeath(destinationRectangle.X, destinationRectangle.Y);
                this.destinationRectangle.Width = 0;
                this.destinationRectangle.Height = 0;
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
        }
    }
}