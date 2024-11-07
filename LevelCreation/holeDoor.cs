using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.LevelCreation;

internal class holeDoor : IDoor
{
    private Texture2D spriteSheet;
    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    public Rectangle CollisionHitbox { get; set; }
    private int doorNum;
    private int scaleFactor = 4;
    private bool blownUp;
    public ObjectType ObjectType { get { return ObjectType.Door; } }
    public DoorType DoorType { get { return DoorType.Hole; } }
    public bool AlreadyOverlapping { get; set; }
    public bool IsOpen { get; set; }
    public holeDoor(Texture2D spriteSheet, int doorNum)
    {
        this.doorNum = doorNum;
        this.spriteSheet = spriteSheet;
        this.sourceRectangle = new Rectangle(294, (33 * doorNum), 31, 31);
        DetermineDestination();
        blownUp = false;
        AlreadyOverlapping = false;
        IsOpen = false;
    }
    public void DetermineDestination()
    {
        switch (doorNum)
        {
            case 0:
                destinationRectangle = new Rectangle(443, 192, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 1:
                destinationRectangle = new Rectangle(-5, 479, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 2:
                destinationRectangle = new Rectangle(895, 479, 33 * scaleFactor, 32 * scaleFactor);
                break;
            case 3:
                destinationRectangle = new Rectangle(443, 765, 33 * scaleFactor, 32 * scaleFactor);
                break;
        }
    }
    public void Update(GameTime gameTime)
    {
        if (blownUp)
        {
            sourceRectangle = new Rectangle(426, (33 * doorNum), 31, 31);
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White, 0f, new Vector2(), SpriteEffects.None, 0.1f);
    }
}