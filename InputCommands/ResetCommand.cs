using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class ResetCommand : ICommand
    {
        GameStateMachine gameStateMachine;
        public ResetCommand(GameStateMachine gameStateMachine) {
            this.gameStateMachine = gameStateMachine;
        }
        public void Execute()
        {
            gameStateMachine.ResetGame();
        }
    }
}
