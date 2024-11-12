using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.LevelCreation;

internal class diamondDoor : IDoor
{
    private Texture2D spriteSheet;
    private Rectangle sourceRectangleClosed;
    private Rectangle sourceRectangleOpen;
    private Rectangle destinationRectangle;
    public Rectangle CollisionHitbox
    {
        get { return destinationRectangle; }
        set { destinationRectangle = value; }
    }
    private int doorNum;
    private int xPos;
    private int yPos;
    private int scaleFactor = 4;
    public ObjectType ObjectType { get { return ObjectType.Door; } }
    public DoorType DoorType { get { return DoorType.Diamond; } }
    public bool IsCameraMoving { get; set; }
    public bool IsOpen { get; set; }
    public diamondDoor(Texture2D spriteSheet, int doorNum, int RoomRow, int RoomColumn)
    {
        this.doorNum = doorNum;
        this.xPos = RoomRow;
        this.yPos = RoomColumn;
        this.spriteSheet = spriteSheet;
        this.sourceRectangleClosed = new Rectangle(393, (33 * doorNum), 31, 31);
        this.sourceRectangleOpen = new Rectangle(327, (33 * doorNum), 31, 31);
        DetermineDestination();
        IsCameraMoving = false;
        IsOpen = false;
    }

    public void DetermineDestination()
    {
        int roomTopLeftX = xPos * 1020;
        int roomTopLeftY = yPos * 698;
        switch (doorNum)
        {
            case 0:
                destinationRectangle = new Rectangle(443 + roomTopLeftX, 192 + roomTopLeftY, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 1:
                destinationRectangle = new Rectangle(-5 + roomTopLeftX, 479 + roomTopLeftY, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 2:
                destinationRectangle = new Rectangle(898 + roomTopLeftX, 479 + roomTopLeftY, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 3:
                destinationRectangle = new Rectangle(445 + roomTopLeftX, 765 + roomTopLeftY, 33 * scaleFactor, 32 * scaleFactor);
                break;
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsOpen)
        {
            spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangleOpen, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
        } else
        {
            spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangleClosed, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
        }
    }
}