using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Legend_of_the_Power_Rangers
{
    public class LinkMovement
    {
        private Link link;
        private LinkStateMachine stateMachine;
        private const float MovementSpeed = 2f;

        public LinkMovement(Link link, LinkStateMachine stateMachine)
        {
            this.link = link;
            this.stateMachine = stateMachine;
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
