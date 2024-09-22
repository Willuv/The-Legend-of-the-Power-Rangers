using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkRightCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        public LinkRightCommand()
        {
            this.stateMachine = new LinkStateMachine();
        }
        public void Execute()
        {
            this.stateMachine.ChangeState(LinkStateMachine.LinkState.Right);
        }
    }
}
