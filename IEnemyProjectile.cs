using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Legend_of_the_Power_Rangers
{
    public interface IEnemyProjectile : ICollision, IDamaging
    {
        EnemyProjectileType EnemyProjectileType { get; }
        Vector2 Direction { get; set; }
        void Update(GameTime gameTime);
        void Draw(Texture2D texture, SpriteBatch spriteBatch);
    }
}