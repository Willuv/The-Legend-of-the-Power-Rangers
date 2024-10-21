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
        bool PickedUp { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}