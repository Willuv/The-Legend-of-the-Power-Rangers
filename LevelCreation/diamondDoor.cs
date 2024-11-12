using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using System;
using Legend_of_the_Power_Rangers;

internal class diamondDoor : IDoor
{
    private Texture2D spriteSheet;
    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int doorNum;
    private int xPos;
    private int yPos;
    private int scaleFactor = 4;
    private bool canWalkThrough;
    public bool CanWalkThrough
    {
        get { return canWalkThrough; }
        set { canWalkThrough = value; }
    }
    private bool isOpen;
    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }
    public diamondDoor(Texture2D spriteSheet, int doorNum, int RoomRow, int RoomColumn)
    {
        this.doorNum = doorNum;
        this.xPos = RoomRow;
        this.yPos = RoomColumn;
        this.spriteSheet = spriteSheet;
        this.sourceRectangle = new Rectangle(393, (33 * doorNum), 31, 31);
        determineDestination();
        isOpen = false;
        canWalkThrough = false;
    }

    public void determineDestination()
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
    public void Update(GameTime gametime, int enemiesCount)
    {
        if (enemiesCount == 0)
        {
            canWalkThrough = true;
            sourceRectangle = new Rectangle(327, (33 * doorNum), 31, 31);
        } else
        {
            canWalkThrough = false;
            sourceRectangle = new Rectangle(393, (33 * doorNum), 31, 31);
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
    }
}