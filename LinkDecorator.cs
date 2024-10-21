using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LinkDecorator : Link
{
    private Color damagedColor;
    private float damageDuration;
    private float timeDamaged;
    private Link baseLink;

    public LinkDecorator(Link baseLink) : base()
    {
        this.baseLink = baseLink;
        damagedColor = Color.Red;
        damageDuration = 1f;
        timeDamaged = damageDuration;
    }

    public void TakeDamage()
    {
        timeDamaged = 0f;
    }

    public bool IsDamaged()
    {
        return timeDamaged < damageDuration;
    }

    public override void Update(GameTime gameTime)
    {
        baseLink.Update(gameTime);

        if (IsDamaged())
        {
            timeDamaged += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        ILinkSprite currentSprite = baseLink.GetStateMachine().GetCurrentSprite();
        Color drawColor = IsDamaged() ? damagedColor : Color.White;
        currentSprite.Draw(spriteBatch, baseLink.DestinationRectangle, drawColor);
    }
}
