using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Legend_of_the_Power_Rangers.LinkStateMachine;
using static Legend_of_the_Power_Rangers.Item;

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
            Idle, Attack, Item
        }

        private LinkDirection currentDirection;
        private LinkAction currentAction;
        private Texture2D linkSpriteSheet;
        private ILinkSprite currentSprite;
        private LinkDirection lastDirection;
        private Texture2D itemSpriteSheet;
        private Texture2D projectileSpriteSheet;
        private const float MovementSpeed = 2f;

      
        public LinkStateMachine(Texture2D spriteSheet, Texture2D itemSheet, Texture2D projectileSheet)

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
            if (currentAction != LinkAction.Idle)
            {
                movement = Vector2.Zero;
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
                case LinkAction.Item:
                    
                    ChangeItemState();
                    break;
                case LinkAction.Idle:
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

        private void ChangeItemState()
        {
            System.Diagnostics.Debug.WriteLine($"Item");
            switch (lastDirection)
            {
                case LinkDirection.Right:
                    System.Diagnostics.Debug.WriteLine($"Item Right");
                    currentSprite = new LinkItemRightSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Left:
                    System.Diagnostics.Debug.WriteLine($"Item Left");
                    currentSprite = new LinkItemLeftSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Up:
                    System.Diagnostics.Debug.WriteLine($"Item Up");
                    currentSprite = new LinkItemUpSprite(linkSpriteSheet);
                    break;
                case LinkDirection.Down:
                    System.Diagnostics.Debug.WriteLine($"Item Down");
                    currentSprite = new LinkItemDownSprite(linkSpriteSheet);
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
        public LinkDirection GetLastDirection() {
            return lastDirection;
        }
    }
}
