using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework.Graphics;

public interface IWall : ICollision
{
    void Draw(SpriteBatch spriteBatch, Texture2D levelSpriteSheet);
    void DetermineRectangles(int xPos, int yPos);
    bool IsVisible { get; set; }
}
