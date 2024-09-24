using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkDownCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        public LinkDownCommand(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public void Execute()
        {
            this.stateMachine.ChangeState(LinkStateMachine.LinkState.Down);
        }
    }
}
