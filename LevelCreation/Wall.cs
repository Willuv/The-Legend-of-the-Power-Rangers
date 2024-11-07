using Legend_of_the_Power_Rangers;
using Microsoft.Testing.Platform.Extensions.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class Wall : IWall
{
	int wallNum;
	Rectangle sourceRectangle;
	Rectangle destinationRectangle;
    public Rectangle CollisionHitbox
    {
        get { return destinationRectangle; }
        set { destinationRectangle = value; }
    }
    int scaleFactor = 4;
    public ObjectType ObjectType { get { return ObjectType.Wall; } }
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
		switch (wallNum)
		{
			case 0:
                sourceRectangle = new Rectangle(0, 0, 31, 71);
                destinationRectangle = new Rectangle(1, 194, 31 * scaleFactor, 71 * scaleFactor);
                break;
			case 1:
                sourceRectangle = new Rectangle(0, 0, 111, 31);
                destinationRectangle = new Rectangle(1, 194, 111 * scaleFactor, 31 * scaleFactor);
                break;
			case 2:
                sourceRectangle = new Rectangle(144, 0, 111, 31);
                destinationRectangle = new Rectangle(575, 194, 111 * scaleFactor, 31 * scaleFactor);
                break;
            case 3:
                sourceRectangle = new Rectangle(224, 0, 31, 71);
                destinationRectangle = new Rectangle(895, 194, 31 * scaleFactor, 71 * scaleFactor);
                break;
            case 4:
                sourceRectangle = new Rectangle(224, 104, 31, 71);
                destinationRectangle = new Rectangle(895, 605, 31 * scaleFactor, 71 * scaleFactor);
                break;
            case 5:
                sourceRectangle = new Rectangle(144, 144, 111, 31);
                destinationRectangle = new Rectangle(575, 767, 111 * scaleFactor, 31 * scaleFactor);
                break;
            case 6:
                sourceRectangle = new Rectangle(0, 144, 111, 31);
                destinationRectangle = new Rectangle(0, 767, 111 * scaleFactor, 31 * scaleFactor);
                break;
            case 7:
                sourceRectangle = new Rectangle(0, 104, 31, 71);
                destinationRectangle = new Rectangle(0, 605, 31 * scaleFactor, 71 * scaleFactor);
                break;
		}
	}
}
