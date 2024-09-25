using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class Link
    {
        private Texture2D linkSpriteSheet;
        private LinkStateMachine stateMachine;
        private Vector2 position;

        public Link(Texture2D spriteSheet, LinkStateMachine stateMachine)
        {
            linkSpriteSheet = spriteSheet;
            this.stateMachine = stateMachine;
            position = new Vector2(200, 200);
        }

        public void UpdatePosition(Vector2 movement)
        {
            position += movement;
        }
        public Vector2 GetPosition()
        {
            return position;
        }

        public Texture2D GetLinkSpriteSheet()
        {
            return linkSpriteSheet;
        }

        public LinkStateMachine GetStateMachine()
        {
            return stateMachine;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (stateMachine.GetCurrentState() != LinkStateMachine.LinkState.Idle)
            {
                ILinkSprite currentSprite = stateMachine.GetCurrentSprite();
                currentSprite.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            ILinkSprite currentSprite = stateMachine.GetCurrentSprite();
            currentSprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
