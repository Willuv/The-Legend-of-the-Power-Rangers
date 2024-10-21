using Legend_of_the_Power_Rangers.LinkSpritesClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class OctoProjectile : IitemSprite
    {
        private Texture2D projectileTexture;
        private Rectangle sourceRectangle;
        private Rectangle offset;
        private Vector2 movement;
        private Rectangle position;
        private int currentFrame;
        private int totalFrames;
        private bool finished;
        private float scaleFactor = 3f;

        public Rectangle DestinationRectangle { get; set; }

        public OctoProjectile(Texture2D texture, Rectangle position, Vector2 direction)
        {
            projectileTexture = texture;
            this.position = position;
            totalFrames = 40; // How long the projectile lasts
            currentFrame = 0;
            finished = false;

            SetDirection(direction);
        }

        // Aligning the projectile based on RedOcto's direction
        public void SetDirection(Vector2 direction)
        {
            if (direction.X < 0) // Left
            {
                offset = new Rectangle(-50, 15, 0, 0);
                movement.X = -4;
                sourceRectangle = new Rectangle(150, 8, 15, 5);
            }
            else if (direction.X > 0) // Right
            {
                offset = new Rectangle(50, 15, 0, 0);
                movement.X = 4;
                sourceRectangle = new Rectangle(210, 8, 15, 5);
            }
            else if (direction.Y < 0) // Up
            {
                offset = new Rectangle(15, -50, 0, 0);
                movement.Y = -4;
                sourceRectangle = new Rectangle(185, 3, 5, 15);
            }
            else if (direction.Y > 0) // Down
            {
                offset = new Rectangle(15, 50, 0, 0);
                movement.Y = 4;
                sourceRectangle = new Rectangle(125, 3, 5, 15);
            }
        }

        public void Update(GameTime gameTime)
        {
            currentFrame++;
            position.X += (int)movement.X;
            position.Y += (int)movement.Y;

            if (currentFrame > totalFrames)
            {
                finished = true;
            }

            DestinationRectangle = new Rectangle(
                position.X + offset.X,
                position.Y + offset.Y,
                sourceRectangle.Width * (int)scaleFactor,
                sourceRectangle.Height * (int)scaleFactor);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(projectileTexture, DestinationRectangle, sourceRectangle, Color.White);
        }

        public bool GetState()
        {
            return finished;
        }
    }
}
