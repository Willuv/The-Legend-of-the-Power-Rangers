using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace Legend_of_the_Power_Rangers
{
    public class LinkDecorator : Link
    {
        private Color damagedColor;
        private float damageDuration;
        private float timeDamaged;
        private Link baseLink;

        public LinkDecorator(Link baseLink) : base(baseLink.GetLinkSpriteSheet())
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
            if (IsDamaged())
            {
                timeDamaged += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                baseLink.Update(gameTime);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ILinkSprite currentSprite = baseLink.GetStateMachine().GetCurrentSprite();
            Color drawColor = Color.White;
            if (IsDamaged())
            {
                drawColor = damagedColor;
            }
            currentSprite.Draw(spriteBatch, baseLink.GetPosition(), drawColor);
        }
    }
}
