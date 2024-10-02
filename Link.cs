using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

public class Link
{
    private LinkStateMachine stateMachine;
    private ILinkSprite currentSprite;
    private Vector2 position;

    public Link()
    {
        stateMachine = new LinkStateMachine(); // Pass the spriteSheet as necessary
        position = new Vector2(200, 200);
        currentSprite = stateMachine.GetCurrentSprite();
    }

    public void UpdatePosition(Vector2 movement)
    {
        position += movement;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public LinkStateMachine GetStateMachine()
    {
        return stateMachine;
    }

    public LinkDirection GetDirection()
    {
        return stateMachine.GetCurrentDirection(); // Get current direction without checking for idle
    }

    public virtual void Update(GameTime gameTime)
    {
        Vector2 movement = stateMachine.UpdateMovement();
        UpdatePosition(movement);

        // Instead of checking for idle direction, just check if it's not idle action
        if (stateMachine.GetCurrentAction() != LinkStateMachine.LinkAction.Idle)
        {
            currentSprite = stateMachine.GetCurrentSprite();
            currentSprite.Update(gameTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        currentSprite = stateMachine.GetCurrentSprite();

        // Draw only if Link is performing an action
        if (stateMachine.GetCurrentAction() != LinkStateMachine.LinkAction.Idle)
        {
            currentSprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
