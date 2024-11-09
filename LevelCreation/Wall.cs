using Microsoft.Testing.Platform.Extensions.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class Wall : IWall
{
	int wallNum;
	Rectangle sourceRectangle;
	Rectangle destinationRectangle;
    int scaleFactor = 4;
	public Wall(int wallNum, int xPos, int yPos)
	{
		this.wallNum = wallNum;
		DetermineRectangles(xPos,  yPos);
	}
	public void Draw(SpriteBatch spriteBatch, Texture2D levelSpriteSheet)
	{
		spriteBatch.Draw(levelSpriteSheet, destinationRectangle, sourceRectangle, Color.White);
	}
    public void DetermineRectangles(int xPos, int yPos)
    {
        // Calculate the room's top-left corner based on xPos and yPos
        int roomTopLeftX = xPos * 1020;
        int roomTopLeftY = yPos * 698;
        switch (wallNum)
        {
            case 0: // Left wall, top
                sourceRectangle = new Rectangle(0, 0, 31, 71);
                destinationRectangle = new Rectangle(roomTopLeftX, roomTopLeftY + 194, 31 * scaleFactor, 71 * scaleFactor);
                break;
            case 1: // Top wall, middle
                sourceRectangle = new Rectangle(0, 0, 111, 31);
                destinationRectangle = new Rectangle(roomTopLeftX, roomTopLeftY + 194, 111 * scaleFactor, 31 * scaleFactor);
                break;
            case 2: // Top wall, right
                sourceRectangle = new Rectangle(144, 0, 111, 31);
                destinationRectangle = new Rectangle(roomTopLeftX + 575, roomTopLeftY + 194, 111 * scaleFactor, 31 * scaleFactor);
                break;
            case 3: // Right wall, top
                sourceRectangle = new Rectangle(224, 0, 31, 71);
                destinationRectangle = new Rectangle(roomTopLeftX + 899 , roomTopLeftY + 194, 31 * scaleFactor, 71 * scaleFactor);
                break;
            case 4: // Right wall, bottom
                sourceRectangle = new Rectangle(224, 104, 31, 71);
                destinationRectangle = new Rectangle(roomTopLeftX + 899, roomTopLeftY + 605, 31 * scaleFactor, 71 * scaleFactor);
                break;
            case 5: // Bottom wall, right
                sourceRectangle = new Rectangle(144, 145, 111, 31);
                destinationRectangle = new Rectangle(roomTopLeftX + 578, roomTopLeftY + 769, 111 * scaleFactor, 31 * scaleFactor);
                break;
            case 6: // Bottom wall, left
                sourceRectangle = new Rectangle(0, 145, 111, 31);
                destinationRectangle = new Rectangle(roomTopLeftX, roomTopLeftY + 769, 111 * scaleFactor, 31 * scaleFactor);
                break;
            case 7: // Left wall, bottom
                sourceRectangle = new Rectangle(0, 104, 31, 71);
                destinationRectangle = new Rectangle(roomTopLeftX, roomTopLeftY + 605, 31 * scaleFactor, 71 * scaleFactor);
                break;
        }
    }


}
