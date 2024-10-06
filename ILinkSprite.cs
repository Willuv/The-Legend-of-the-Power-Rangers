using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface ILinkSprite
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color);
}
