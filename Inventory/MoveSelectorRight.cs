using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Legend_of_the_Power_Rangers
{
    public class MoveSelectorRight : ICommand
    {
        private readonly ItemSelector itemSelector;
        private int direction = 100;

        public MoveSelectorRight(ItemSelector itemSelector)
        {
            this.itemSelector = itemSelector;
        }
        public void Execute()
        {
            itemSelector.moveSelector(direction);
        }
    }
}
