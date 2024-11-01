using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

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
        Debug.WriteLine("[LinkDecorator] Damage taken! Triggering visual feedback.");
        baseLink.LoseHealth();
        timeDamaged = 0f;

        Debug.WriteLine($"[LinkDecorator] Visual feedback active. Link's health after damage: {baseLink.GetCurrentHealth()}/{baseLink.GetMaxHealth()}");

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
