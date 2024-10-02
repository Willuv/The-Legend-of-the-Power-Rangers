using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Legend_of_the_Power_Rangers.LinkStateMachine;
using static Legend_of_the_Power_Rangers.Item;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkStateMachine
    {
        public enum LinkDirection
        {
            Left, Right, Up, Down
        }

        public enum LinkAction
        {
            Idle, Attack, Item, Moving
        }

        private LinkDirection currentDirection;
        private LinkAction currentAction;
        private ILinkSprite currentSprite;
        private const float MovementSpeed = 2f;

        public LinkStateMachine()
        {
            currentAction = LinkAction.Idle;
            currentDirection = LinkDirection.Right;
            currentSprite = LinkSpriteFactory.Instance.CreateLinkSprite(currentAction, currentDirection);
        }

        public void ChangeDirection(LinkDirection newDirection)
        {
            if (currentDirection != newDirection)
            {
                currentDirection = newDirection;
                ChangeDirectionState();

                if (currentAction != LinkAction.Attack && currentAction != LinkAction.Item)
                {
                    ChangeAction(LinkAction.Moving);
                }
            }
        }

        public void ChangeAction(LinkAction newAction)
        {
            if (currentAction != newAction)
            {
                currentAction = newAction;
                ChangeActionState();
            }
        }

        public Vector2 UpdateMovement()
        {
            Vector2 movement = Vector2.Zero;

            if (currentAction == LinkAction.Moving)
            {
                switch (currentDirection)
                {
                    case LinkDirection.Up:
                        movement.Y = -MovementSpeed;
                        break;
                    case LinkDirection.Down:
                        movement.Y = MovementSpeed;
                        break;
                    case LinkDirection.Left:
                        movement.X = -MovementSpeed;
                        break;
                    case LinkDirection.Right:
                        movement.X = MovementSpeed;
                        break;
                }
            }
            return movement;
        }

        private void ChangeDirectionState()
        {
            currentSprite = LinkSpriteFactory.Instance.CreateLinkSprite(currentAction, currentDirection);
        }

        private void ChangeActionState()
        {
            currentSprite = LinkSpriteFactory.Instance.CreateLinkSprite(currentAction, currentDirection);
        }

        public ILinkSprite GetCurrentSprite()
        {
            return currentSprite;
        }

        public LinkAction GetCurrentAction()
        {
            return currentAction;
        }

        public LinkDirection GetLastDirection()
        {
            return currentDirection;
        }
        public LinkDirection GetCurrentDirection()
        {
            return currentDirection;
        }


    }

}
