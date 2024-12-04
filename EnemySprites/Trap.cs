////LinkDecorator.baseLink.GetPosition();
//using Legend_of_the_Power_Rangers;

//LinkManager.getLink().DestinationRectangle.X
//x 265
//y 316
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
        private float speed = 150f;
        private int frameIndex1;
        private int frameIndex2;
        private int currentFrameIndex;
        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;

        public bool isDead { get; set; } = false;
        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.Trap; } }

        private Vector2 direction = Vector2.Zero;

        public TrapEnemy() : base()
        {
            InitializeFrames();
            CollisionHitbox = new Rectangle(200, 200, 64, 64); // Initial position
        }

        private void InitializeFrames()
        {
            sourceRectangle = new Rectangle[]
            {
                new Rectangle(270, 330, 16, 16),  // Example frame 1
            };
        }

        public void SetDirection(Vector2 newDirection)
        {
            direction = newDirection;

            // Update sprite animation frames based on direction
            if (direction.X > 0) currentFrameIndex = 1; // Moving right
            else if (direction.X < 0) currentFrameIndex = 3; // Moving left
            else if (direction.Y > 0) currentFrameIndex = 5; // Moving down
            else if (direction.Y < 0) currentFrameIndex = 7; // Moving up
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

            // Get Link's position
            Rectangle linkPosition;
            Link link = LinkManager.GetLink();
            linkPosition.X = link.destinationRectangle.X;
            linkPosition.Y = link.destinationRectangle.Y;

            // Check if TrapEnemy is aligned with Link in the X or Y direction
            if (linkPosition.X == destinationRectangle.X)
            {
                // Move up or down towards Link
                if (linkPosition.Y > destinationRectangle.Y) SetDirection(new Vector2(0, 1)); // Down
                else SetDirection(new Vector2(0, -1)); // Up
            }
            else if (linkPosition.Y == destinationRectangle.Y)
            {
                // Move left or right towards Link
                if (linkPosition.X > destinationRectangle.X) SetDirection(new Vector2(1, 0)); // Right
                else SetDirection(new Vector2(-1, 0)); // Left
            }
            else
            {
                // Not aligned, stop moving
                SetDirection(Vector2.Zero);
            }

            // Update position
            destinationRectangle.X += (int)(direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds);
            destinationRectangle.Y += (int)(direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            Color tint = isHurt ? Color.Red : Color.White;
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrameIndex], tint);
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
