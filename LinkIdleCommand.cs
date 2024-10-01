using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkIdleCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        public LinkIdleCommand(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public void Execute()
        {
            if (!stateMachine.IsAttacking())
            {
                stateMachine.ChangeAction(LinkStateMachine.LinkAction.Idle);
            }
            stateMachine.ChangeDirection(LinkStateMachine.LinkDirection.Idle);

        }
    }
}
