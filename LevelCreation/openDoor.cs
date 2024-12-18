﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.LevelCreation;

internal class openDoor : IDoor
{
    private Texture2D spriteSheet;
    private Rectangle sourceRectangle;
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
    public DoorType DoorType { get { return DoorType.Open; } }
    public bool IsCameraMoving { get; set; }
    public bool IsOpen { get; set; }
    public openDoor(Texture2D spriteSheet, int doorNum, int RoomRow, int RoomColumn)
    {
        this.doorNum = doorNum;
        this.xPos = RoomRow;
        this.yPos = RoomColumn;
        this.spriteSheet = spriteSheet;
        this.sourceRectangle = new Rectangle(327, (33 * doorNum), 31, 31);
        DetermineDestination();
        IsCameraMoving = false;
        IsOpen = true;
    }

    public void DetermineDestination()
    {
        int roomTopLeftX = xPos * 1020;
        int roomTopLeftY = yPos * 698;
        switch (doorNum)
        {
            case 0:
                destinationRectangle = new Rectangle(443 + roomTopLeftX, 190 + roomTopLeftY, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 1:
                destinationRectangle = new Rectangle(-9 + roomTopLeftX, 479 + roomTopLeftY, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 2:
                destinationRectangle = new Rectangle(902 + roomTopLeftX, 479 + roomTopLeftY, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 3:
                destinationRectangle = new Rectangle(445 + roomTopLeftX, 773 + roomTopLeftY, 33 * scaleFactor, 32 * scaleFactor);
                break;
        }
    }

    public void Update(GameTime gameTime)
    {

    }


    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
    }
}