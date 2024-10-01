using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class BlockNextCommand : ICommand
    {
        private int direction = 1;

        private readonly Game1 game1;
        public BlockNextCommand(Game1 game) {
            game1 = game;
        }
        public void Execute()
        {
            game1.ChangeBlock(direction);
        }
    }
}
