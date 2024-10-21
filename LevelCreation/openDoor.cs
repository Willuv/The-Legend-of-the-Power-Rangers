using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;

internal class openDoor : IDoor
{
    private Texture2D spriteSheet;
    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int doorNum;
    private int scaleFactor = 3;
    public openDoor(Texture2D spriteSheet, int doorNum)
    {
        this.doorNum = doorNum;
        this.spriteSheet = spriteSheet;
        this.sourceRectangle = new Rectangle(327, (33 * doorNum), 31, 31);
        determineDestination();
    }

    public void determineDestination()
    {
        switch (doorNum)
        {
            case 0:
                destinationRectangle = new Rectangle(349, 0, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 1:
                destinationRectangle = new Rectangle(13, 216, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 2:
                destinationRectangle = new Rectangle(685, 216, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 3:
                destinationRectangle = new Rectangle(349, 432, 33 * scaleFactor, 32 * scaleFactor);
                break;
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White);
    }
}