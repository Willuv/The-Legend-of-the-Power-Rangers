using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.LevelCreation;

internal class diamondDoor : IDoor
{
    private Texture2D spriteSheet;
    private Rectangle sourceRectangle;
    public Rectangle DestinationRectangle { get; set; }
    private int doorNum;
    private int scaleFactor = 4;
    public ObjectType ObjectType { get { return ObjectType.Door; } }
    public DoorType DoorType { get { return DoorType.Diamond; } }
    public bool AlreadyOverlapping { get; set; }
    public diamondDoor(Texture2D spriteSheet, int doorNum)
    {
        this.doorNum = doorNum;
        this.spriteSheet = spriteSheet;
        this.sourceRectangle = new Rectangle(393, (33 * doorNum), 31, 31);
        DetermineDestination();
        AlreadyOverlapping = false;
    }

    public void DetermineDestination()
    {
        switch (doorNum)
        {
            case 0:
                DestinationRectangle = new Rectangle(443, 192, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 1:
                DestinationRectangle = new Rectangle(-5, 479, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 2:
                DestinationRectangle = new Rectangle(895, 479, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 3:
                DestinationRectangle = new Rectangle(443, 765, 33 * scaleFactor, 32 * scaleFactor);
                break;
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteSheet, DestinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
    }
}