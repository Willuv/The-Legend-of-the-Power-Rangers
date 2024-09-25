using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public interface ILinkSprite
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Vector2 position, Color color);

    }
}
