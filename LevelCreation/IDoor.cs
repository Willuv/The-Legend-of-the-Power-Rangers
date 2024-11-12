using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework.Graphics;

public interface IDoor : ICollision
{
    void DetermineDestination();
    void Draw(SpriteBatch spriteBatch);
    bool IsCameraMoving { get; set; } //so we don't constantly call move until transition over
    bool IsOpen { get; set; }
    DoorType DoorType { get; }
}