using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class LinkMovement
    {
        private Link link;
        private const float MovementSpeed = 2f;

        public LinkMovement(Link link)
        {
            this.link = link;
        }

        public void UpdateMovement(LinkStateMachine stateMachine)
        {
            var currentState = stateMachine.GetCurrentState();
            Vector2 movement = Vector2.Zero;

            switch (currentState)
            {
                case LinkStateMachine.LinkState.Up:
                    movement.Y = -MovementSpeed;
                    break;
                case LinkStateMachine.LinkState.Down:
                    movement.Y = MovementSpeed;
                    break;
                case LinkStateMachine.LinkState.Left:
                    movement.X = -MovementSpeed;
                    break;
                case LinkStateMachine.LinkState.Right:
                    movement.X = MovementSpeed;
                    break;
                case LinkStateMachine.LinkState.Idle:
                default:    
                    movement = Vector2.Zero;
                    break;
            }

            link.UpdatePosition(movement);
        }
    }
}
