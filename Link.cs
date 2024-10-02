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
        stateMachine = new LinkStateMachine();
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
        return stateMachine.GetCurrentDirection();
    }

    public virtual void Update(GameTime gameTime)
    {
        Vector2 movement = stateMachine.UpdateMovement();
        UpdatePosition(movement);

        currentSprite = stateMachine.GetCurrentSprite();
        currentSprite.Update(gameTime);

    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        currentSprite = stateMachine.GetCurrentSprite();
        currentSprite.Draw(spriteBatch, position, Color.White);
    }
}
