using Microsoft.Xna.Framework.Graphics;

public interface IDoor
{
    void determineDestination();
    void Draw(SpriteBatch spriteBatch);
}