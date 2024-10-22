using Legend_of_the_Power_Rangers.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class OldMan : IEnemy
    {
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        public ObjectType ObjectType { get { return ObjectType.Enemy; } }
        public EnemyType EnemyType { get { return EnemyType.BlueGorya; } }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}