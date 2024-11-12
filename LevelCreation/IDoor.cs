using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IDoor //add icollision
{
    void determineDestination();
    void Update(GameTime gametime, int enemiesCount);
    void Draw(SpriteBatch spriteBatch);
}