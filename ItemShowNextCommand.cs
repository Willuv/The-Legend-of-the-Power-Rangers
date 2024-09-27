using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class ItemShowNextCommand : ICommand
    { 
        private int direction = 1;

        private readonly Game1 _game;
        
        
        public ItemShowNextCommand(Game1 game) {
           _game = game;
        }
        public void Execute()
        { 
            _game.ChangeItem(direction);  
        }
        
    }
}
