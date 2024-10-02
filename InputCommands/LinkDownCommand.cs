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
        private const float MovementSpeed = 2f;

        public LinkDownCommand(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public void Execute()
        {
            if (stateMachine.GetCurrentAction() == LinkStateMachine.LinkAction.Moving)
            {
                stateMachine.ChangeAction(LinkStateMachine.LinkAction.Moving);
            }
            stateMachine.ChangeDirection(LinkStateMachine.LinkDirection.Down);

        }
    }
}
