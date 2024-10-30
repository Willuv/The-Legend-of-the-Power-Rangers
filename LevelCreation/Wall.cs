using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class Wall : IWall
{
	int wallNum;
	Rectangle sourceRectangle;
	Rectangle destinationRectangle;
    int scaleFactor = 5;
	public Wall(int wallNum)
	{
		this.wallNum = wallNum;
		DetermineRectangles();
	}
	public void Draw(SpriteBatch spriteBatch, Texture2D levelSpriteSheet)
	{
		spriteBatch.Draw(levelSpriteSheet, destinationRectangle, sourceRectangle, Color.White);
	}
	public void DetermineRectangles()
	{
		switch (wallNum)
		{
			case 0:
                sourceRectangle = new Rectangle(0, 0, 31, 71);
                destinationRectangle = new Rectangle(3, 1, 31 * scaleFactor, 71 * scaleFactor);
				break;
			case 1:
                sourceRectangle = new Rectangle(0, 0, 111, 31);
                destinationRectangle = new Rectangle();
                break;
			case 2:
                sourceRectangle = new Rectangle(144, 0, 111, 31);
                destinationRectangle = new Rectangle();
                break;
            case 3:
                sourceRectangle = new Rectangle(224, 0, 31, 71);
                destinationRectangle = new Rectangle();
                break;
            case 4:
                sourceRectangle = new Rectangle(224, 104, 31, 71);
                destinationRectangle = new Rectangle();
                break;
            case 5:
                sourceRectangle = new Rectangle(144, 144, 111, 31);
                destinationRectangle = new Rectangle();
                break;
            case 6:
                sourceRectangle = new Rectangle(0, 144, 111, 31);
                destinationRectangle = new Rectangle();
                break;
            case 7:
                sourceRectangle = new Rectangle(0, 104, 31, 71);
                destinationRectangle = new Rectangle();
                break;
		}
	}
}
