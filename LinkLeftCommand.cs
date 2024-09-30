using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkLeftCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        public LinkLeftCommand(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public void Execute()
        {
            this.stateMachine.ChangeDirection(LinkStateMachine.LinkDirection.Left);
        }
    }
}
