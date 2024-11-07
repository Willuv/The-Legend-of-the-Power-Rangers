using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework.Graphics;

public interface IDoor : ICollision
{
    void DetermineDestination();
    void Draw(SpriteBatch spriteBatch);
    bool AlreadyOverlapping { get; set; }
    bool IsOpen { get; set; }
    DoorType DoorType { get; }
}