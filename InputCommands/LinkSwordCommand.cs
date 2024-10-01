using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkSwordCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        public LinkSwordCommand(LinkStateMachine stateMachine) {
            this.stateMachine = stateMachine;
        }
        public void Execute()
        {
            stateMachine.ChangeAction(LinkStateMachine.LinkAction.Attack);
        }
    }
}
