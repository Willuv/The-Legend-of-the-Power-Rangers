using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class LinkDecorator : Link
{
    private Color damagedColor;
    private float damageDuration;
    private float timeDamaged;
    private Link baseLink;

    private bool isHurt;              
    private float hurtCooldown = 0.5f; 
    private float hurtCooldownTimer;

    public LinkDecorator(Link baseLink) : base()
    {
        this.baseLink = baseLink;
        damagedColor = Color.Red;
        damageDuration = 0.5f;
        timeDamaged = damageDuration;
        isHurt = false;
        hurtCooldownTimer = hurtCooldown;
    }

    public void TakeDamage()
    {
        if (!isHurt)
        {
            baseLink.LoseHealth();
            timeDamaged = 0f;
            isHurt = true;
            hurtCooldownTimer = 0f;

            if (!AudioManager.Instance.IsMuted())
                AudioManager.Instance.PlaySound("Link_Hurt");
        }
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

        if (isHurt)
        {
            hurtCooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (hurtCooldownTimer >= hurtCooldown)
            {
                isHurt = false;
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        ILinkSprite currentSprite = baseLink.GetStateMachine().GetCurrentSprite();
        Color drawColor = IsDamaged() ? damagedColor : Color.White;
        currentSprite.Draw(spriteBatch, baseLink.destinationRectangle, drawColor);
    }
}
