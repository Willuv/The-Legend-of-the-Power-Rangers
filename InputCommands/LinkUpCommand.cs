using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkUpCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;

        public LinkUpCommand(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Execute()
        {
            if (stateMachine.GetCurrentAction() != LinkStateMachine.LinkAction.Moving)
            {
                stateMachine.ChangeAction(LinkStateMachine.LinkAction.Moving);
            }
            stateMachine.ChangeDirection(LinkStateMachine.LinkDirection.Up);
        }
    }
}
