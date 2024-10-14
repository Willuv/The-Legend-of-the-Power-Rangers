using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


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
            stateMachine.ChangeAction(LinkStateMachine.LinkAction.Moving);
            stateMachine.ChangeDirection(LinkStateMachine.LinkDirection.Down);
        }
    }
}
