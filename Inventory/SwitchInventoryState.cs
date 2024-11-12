using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class SwitchInventoryState : ICommand
    {
        private readonly GameStateMachine gameStateMachine;

        private readonly Game1 game1;
        public SwitchInventoryState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        public void Execute()
        {
            if (gameStateMachine.currentState == GameStateMachine.GameState.ItemSelection)
            {
                gameStateMachine.ChangeState(GameStateMachine.GameState.Running, "");
            }
            else
            {
                gameStateMachine.ChangeState(GameStateMachine.GameState.ItemSelection, "");
            }

        }
    }
}
