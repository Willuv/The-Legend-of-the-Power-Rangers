using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class OldMan : Enemy, IEnemy
    {
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        private bool isHurt = false;
        private double hurtTimer = 0;
        private const double hurtDuration = 1000;

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.BlueGorya; } }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            // if (isHurt)
            // {
            //     hurtTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            //     if (hurtTimer >= hurtDuration)
            //     {
            //         isHurt = false;
            //         hurtTimer = 0;
            //     }
            // }
            throw new System.NotImplementedException();
        }
        int Health = 1;
        public void TakeDamage(int damage = 1)
        {
            Health -= damage;
            if (Health <= 0)
            {
                TriggerDeath(destinationRectangle.X, destinationRectangle.Y);
            }
            else
            {
                isHurt = true;
                hurtTimer = 0;
            }
        }
    }
}