﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    internal class NPCShowPreviousCommand : ICommand
    {
        private int direction = -1;
        private readonly Game1 game1;
        public NPCShowPreviousCommand(Game1 game) 
        { 
            game1 = game;
        }
        public void Execute() 
        {
            game1.ChangeEnemy(direction);
        }
    }
}