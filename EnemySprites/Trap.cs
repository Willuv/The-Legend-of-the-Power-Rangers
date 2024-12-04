using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class TrapEnemy : Enemy, IEnemy
    {
        private Rectangle[] sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        public bool HasBeenCounted { get; set; } = false;
        private int originalX;
        private int originalY;
        private float speed = 300f;
        private Vector2 direction = Vector2.Zero;
        
        private bool isReturning = false;
        private bool isTriggered = false;
        private double triggeredTimer = 0; 
        private const double returnDelay = 2000;

        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;

        public bool isDead { get; set; } = false;
        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.Trap; } }


        public TrapEnemy() : base()
        {
            InitializeFrames();
            CollisionHitbox = new Rectangle(200, 200, 64, 64); // Initial position
        }

        private void InitializeFrames()
        {
            sourceRectangle = new Rectangle[] { new Rectangle(270, 330, 16, 16)};
        }

        public void SetDirection(Vector2 newDirection)
        {
            direction = newDirection;
            // Usually houses change frame logic, but traps have no frames.
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

            if (isReturning)
            {
                // Return to starting position
                // Vector2 currentPosition = new Vector2(destinationRectangle.X, destinationRectangle.Y);
                // Vector2 returnDirection = startPosition - currentPosition;
                Vector2 currentPosition = new Vector2(destinationRectangle.X, destinationRectangle.Y);
                Vector2 returnDirection = new Vector2(originalX, originalY) - currentPosition;

                // Stop when close enough to start position
                if (returnDirection.Length() < 1.5) // Used to use 1, but there is a bug with the top two traps getting stuck at the bottom of the room
                {
                    // Set location of where trap currently is
                    destinationRectangle.X = originalX;
                    destinationRectangle.Y = originalY;
                    
                    direction = Vector2.Zero;
                    isReturning = false; // Stop returning
                    isTriggered = false; // Ready for next trigger
                }
                else
                {
                    returnDirection.Normalize();
                    SetDirection(returnDirection);
                }
            }
            else if (!isTriggered)
            {
                Link link = LinkManager.GetLink();
                Rectangle linkPosition;
                linkPosition.X = link.destinationRectangle.X;
                linkPosition.Y = link.destinationRectangle.Y;
                
                const int tolerance = 5; // Trap Sensativity for trigger
                bool alignedX = Math.Abs(linkPosition.X - destinationRectangle.X) <= tolerance;
                bool alignedY = Math.Abs(linkPosition.Y - destinationRectangle.Y) <= tolerance;

                if (alignedX || alignedY)
                {
                    originalX = destinationRectangle.X;
                    originalY = destinationRectangle.Y;
                    isTriggered = true;

                    if (alignedX)
                    {
                        // Up or Down
                        SetDirection(new Vector2(0, linkPosition.Y > destinationRectangle.Y ? 1 : -1));
                    }
                    else if (alignedY)
                    {
                        // Left or Right
                        SetDirection(new Vector2(linkPosition.X > destinationRectangle.X ? 1 : -1, 0));
                    }
                }
            }
            else
            {
                triggeredTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (triggeredTimer >= returnDelay) // Return set to two second cycle
                {
                    triggeredTimer = 0;
                    isReturning = true;
                }
            }

            // Update position
            destinationRectangle.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            destinationRectangle.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            Color tint = isHurt ? Color.Red : Color.White;
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[0], tint);
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