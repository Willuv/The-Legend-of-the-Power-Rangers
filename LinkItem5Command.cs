﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkItem5Command : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        public LinkItem5Command(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public void Execute()
        {
            this.stateMachine.ChangeState(LinkStateMachine.LinkState.Item5);
        }
    }
}
