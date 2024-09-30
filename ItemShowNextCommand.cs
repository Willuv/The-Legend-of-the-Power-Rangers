using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Legend_of_the_Power_Rangers
{
    public class ItemShowNextCommand : ICommand
    {
        private int direction = 1;

        private readonly Game1 game1;

        public ItemShowNextCommand(Game1 game) {
            game1 = game;
        }
        public void Execute()
        {
            game1.ChangeItem(direction);
        }
    }
}
