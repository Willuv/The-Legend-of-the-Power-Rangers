using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class LinkStateMachine
    {
        public enum LinkDirection
        {
            Left, Right, Up, Down, Idle
        }
        public enum LinkAction
        {
            Idle, Attack,
            Item1, Item2, Item3, Item4, Item5
        }

        private LinkDirection currentDirection;
        private LinkAction currentAction;
        private Texture2D linkSpriteSheet;
        private ILinkSprite currentSprite;
        private LinkDirection lastDirection;

        private const float MovementSpeed = 2f;


        public LinkStateMachine(Texture2D spriteSheet)
        {
            linkSpriteSheet = spriteSheet;
            currentAction = LinkAction.Idle;
            currentDirection = LinkDirection.Right;
            lastDirection =  LinkDirection.Right;
            currentSprite = new LinkRightSprite(linkSpriteSheet);

    }

    public void ChangeDirection(LinkDirection newDirection)
        {
            if (currentDirection != newDirection)
            {
                if (newDirection != LinkDirection.Idle)
                {
                    lastDirection = newDirection;
                }
                currentDirection = newDirection;
                ChangeDirectionState();
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
                case LinkDirection.Idle:
                    break;
            }
            return movement;
        }
        private void ChangeDirectionState()
        {
            switch (currentDirection)
            {
                case LinkDirection.Right:
                    currentSprite = new LinkRightSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Left:
                    currentSprite = new LinkLeftSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Up:
                    currentSprite = new LinkUpSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Down:
                    currentSprite = new LinkDownSprite(linkSpriteSheet);
                    break;
            }
            }
        private void ChangeActionState()
        {
            switch (currentAction)
            {
                case LinkAction.Attack:
                    ChangeAttackState();
                    break;
                case LinkAction.Item1:
                    // Handle Item1 action
                    break;
                case LinkAction.Item2:
                    // Handle Item2 action
                    break;
                case LinkAction.Item3:
                    // Handle Item3 action
                    break;
                case LinkAction.Item4:
                    // Handle Item4 action
                    break;
                case LinkAction.Item5:
                    // Handle Item5 action
                    break;
                case LinkAction.Idle:
                default:
                    // Default idle state
                    break;
            }
        }

        private void ChangeAttackState()
        {
            System.Diagnostics.Debug.WriteLine($"Attack");
            switch (lastDirection)
            {
                case LinkDirection.Right:
                    System.Diagnostics.Debug.WriteLine($"Attack Right");
                    currentSprite = new LinkAttackRightSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Left:
                    System.Diagnostics.Debug.WriteLine($"Attack Left");
                    currentSprite = new LinkAttackLeftSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Up:
                    System.Diagnostics.Debug.WriteLine($"Attack Up");
                    currentSprite = new LinkAttackUpSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Down:
                    System.Diagnostics.Debug.WriteLine($"Attack Down");
                    currentSprite = new LinkAttackDownSprite(linkSpriteSheet);
                    break;
            }
        }

        public ILinkSprite GetCurrentSprite()
        {
            return currentSprite;
        }

        public LinkAction GetCurrentAction()
        {
            return currentAction;
        }
        public LinkDirection GetCurrentDirection()
        {
            return currentDirection;
        }
    }
}
