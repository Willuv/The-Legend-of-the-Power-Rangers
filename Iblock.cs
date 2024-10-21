using Legend_of_the_Power_Rangers.BlockSprites;
using Legend_of_the_Power_Rangers.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Legend_of_the_Power_Rangers
{
    public interface IBlock : ICollision
    {
        BlockType BlockType { get; }
        void Update(GameTime gameTime);
        void Draw(Texture2D texture, SpriteBatch spriteBatch);
    }
}