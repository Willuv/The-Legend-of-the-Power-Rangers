using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Legend_of_the_Power_Rangers
{
    public interface IEnemy : ICollision
    {
        EnemyType EnemyType { get; }
        void Update(GameTime gameTime);
        void Draw(Texture2D texture, SpriteBatch spriteBatch);
        public void TriggerDeath(int X, int Y);
        public void TakeDamage(int X);
    }
}