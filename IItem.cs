using Legend_of_the_Power_Rangers.Collision;
using Legend_of_the_Power_Rangers.ItemSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Legend_of_the_Power_Rangers
{
    public interface IItem : ICollision
    {
        ItemType ItemType { get; }
        void Update(GameTime gameTime);
        void Draw(Texture2D texture, SpriteBatch spriteBatch);
    }
}