using Microsoft.Xna.Framework.Graphics;

public interface IWall
{
    void Draw(SpriteBatch spriteBatch, Texture2D levelSpriteSheet);
    void DetermineRectangles(int xPos, int yPos);
}
