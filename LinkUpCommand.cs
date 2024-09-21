using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkUpCommand : ICommand
    {
        //private LinkStateMachine = stateMachine
        public LinkUpCommand() {
            //this.StateMachine = stateMachine
        }
        public void Execute()
        {
            //or is it stateMachine.ChangeState(LinkState.Up)?
        }
    }
}
