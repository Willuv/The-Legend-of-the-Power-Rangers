using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class DoorMaker
{
    Texture2D levelSpriteSheet;
    public DoorMaker(Texture2D levelSpriteSheet) 
    { 
        this.levelSpriteSheet = levelSpriteSheet;
    }

    public IDoor CreateDoor(char doorChar, int doorNum, int RoomRow, int RoomColumn)
    {
        IDoor door = null;
        switch (doorChar)
		{
			case '1':
				door = new wallDoor(levelSpriteSheet, doorNum, RoomRow, RoomColumn);
				break;
			case '2':
                door = new openDoor(levelSpriteSheet, doorNum, RoomRow, RoomColumn);
                break;
			case '3':
                door = new keyDoor(levelSpriteSheet, doorNum, RoomRow, RoomColumn);
                break;
			case '4':
                door = new diamondDoor(levelSpriteSheet, doorNum, RoomRow, RoomColumn);
                break;
			case '5':
				door = new holeDoor(levelSpriteSheet, doorNum, RoomRow, RoomColumn);
                break;
		}
        if (door == null)
        {
            Debug.WriteLine("Door error");
        }
        return door;
    }
}
