using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

public class Link
{
    private LinkStateMachine stateMachine;
    private ILinkSprite currentSprite;
    public Rectangle destinationRectangle;
    private const int LinkWidth = 48;  // Assuming each sprite has a width of 32 pixels
    private const int LinkHeight = 48; // Assuming each sprite has a height of 32 pixels

    public Link()
    {
        stateMachine = new LinkStateMachine();
        // Initialize the destination rectangle with a size of LinkWidth x LinkHeight
        destinationRectangle = new Rectangle(200, 200, LinkWidth, LinkHeight);
        currentSprite = stateMachine.GetCurrentSprite();
    }

    public void UpdatePosition(Vector2 movement)
    {
        destinationRectangle.X += (int)movement.X;
        destinationRectangle.Y += (int)movement.Y;
    }

    public Rectangle GetDestinationRectangle()
    {
        return destinationRectangle;
    }

    public LinkStateMachine GetStateMachine()
    {
        return stateMachine;
    }

    public LinkStateMachine.LinkDirection GetDirection()
    {
        return stateMachine.GetCurrentDirection();
    }

    public virtual void Update(GameTime gameTime)
    {
        stateMachine.UpdateActionTimer(gameTime);

        Vector2 movement = stateMachine.UpdateMovement();
        bool isMoving = (movement != Vector2.Zero);

        if (isMoving)
        {
            UpdatePosition(movement);
        }
        else if (!stateMachine.IsActionLocked())
        {
            stateMachine.ChangeAction(LinkStateMachine.LinkAction.Idle);
            Debug.WriteLine("IDLE");
        }

        currentSprite = stateMachine.GetCurrentSprite();
        currentSprite.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        currentSprite = stateMachine.GetCurrentSprite();
        // Draw the sprite using the destination rectangle
        currentSprite.Draw(spriteBatch, destinationRectangle, Color.White);
    }
}
