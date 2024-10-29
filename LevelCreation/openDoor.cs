using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;

internal class openDoor : IDoor
{
    private Texture2D spriteSheet;
    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int doorNum;
    private int scaleFactor = 5;
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
                destinationRectangle = new Rectangle(561, 120, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 1:
                destinationRectangle = new Rectangle(-5, 487, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 2:
                destinationRectangle = new Rectangle(1120, 486, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 3:
                destinationRectangle = new Rectangle(562, 845, 33 * scaleFactor, 32 * scaleFactor);
                break;
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
    }
}