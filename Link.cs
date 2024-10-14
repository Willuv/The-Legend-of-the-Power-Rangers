using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Link
{
    private LinkStateMachine stateMachine;
    private ILinkSprite currentSprite;
    public Rectangle destinationRectangle;

    private const float ScaleFactor = 3.0f;

    public Link()
    {
        stateMachine = new LinkStateMachine();
        currentSprite = stateMachine.GetCurrentSprite();
        UpdateDestinationRectangle();
    }

    private void UpdateDestinationRectangle()
    {
        Rectangle sourceRectangle = currentSprite.SourceRectangle;

        destinationRectangle = new Rectangle(
            destinationRectangle.X,
            destinationRectangle.Y,
            (int)(sourceRectangle.Width * ScaleFactor),  
            (int)(sourceRectangle.Height * ScaleFactor)
        );
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
        }

        currentSprite = stateMachine.GetCurrentSprite();
        currentSprite.Update(gameTime);

        UpdateDestinationRectangle();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        currentSprite = stateMachine.GetCurrentSprite();
        currentSprite.Draw(spriteBatch, destinationRectangle, Color.White);
    }
}
