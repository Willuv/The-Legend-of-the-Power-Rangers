using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class Link
    {
        private Texture2D linkSpriteSheet;
        private Texture2D itemSpriteSheet;
        private Texture2D projectileSpriteSheet;
        private LinkStateMachine stateMachine;
        private ILinkSprite currentSprite;
        private Vector2 position;

        public Link(Texture2D spriteSheet)
        {
            linkSpriteSheet = spriteSheet;
            stateMachine = new LinkStateMachine(linkSpriteSheet, itemSpriteSheet, projectileSpriteSheet);
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

        public LinkDirection GetDirection()
        {
            return stateMachine.GetLastDirection();
        }

        public virtual void Update(GameTime gameTime)
        {
            Vector2 movement = stateMachine.UpdateMovement();
            UpdatePosition(movement);
            stateMachine.UpdateAnimation(gameTime);

            if (stateMachine.GetCurrentDirection() != LinkStateMachine.LinkDirection.Idle || stateMachine.GetCurrentAction() != LinkStateMachine.LinkAction.Idle)
            {
                var currentSprite = stateMachine.GetCurrentSprite();
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
