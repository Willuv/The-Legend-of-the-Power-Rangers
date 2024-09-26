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
        private ILinkSprite currentSprite;
        private Vector2 position;


        public Link(Texture2D spriteSheet)
        {
            linkSpriteSheet = spriteSheet;
            stateMachine = new LinkStateMachine(linkSpriteSheet);
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
            Vector2 movement = stateMachine.UpdateMovement();
            UpdatePosition(movement);
            currentSprite = stateMachine.GetCurrentSprite();
            if (stateMachine.GetCurrentAction() != LinkStateMachine.LinkAction.Idle ||
                stateMachine.GetCurrentDirection() != LinkStateMachine.LinkDirection.Idle)
            {
                currentSprite.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            currentSprite = stateMachine.GetCurrentSprite();

            if (stateMachine.GetCurrentAction() != LinkStateMachine.LinkAction.Idle)
            {
                currentSprite.Draw(spriteBatch, position, Color.White);
            }
        }
    }
}
