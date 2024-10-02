using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Legend_of_the_Power_Rangers
{
    public class LinkUpCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        private const float MovementSpeed = 2f;


        public LinkUpCommand(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Execute()
        {
            if (stateMachine.GetCurrentAction() == LinkStateMachine.LinkAction.Moving) { 
                stateMachine.ChangeAction(LinkStateMachine.LinkAction.Moving);
            }
            stateMachine.ChangeDirection(LinkStateMachine.LinkDirection.Up);
        }
    }
}
