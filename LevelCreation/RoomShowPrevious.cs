using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Legend_of_the_Power_Rangers
{
    public class RoomShowPrevious : ICommand
    {
        private readonly Level level;
        private int direction = -1;

        public RoomShowPrevious(Level level)
        {
            this.level = level;
        }
        public void Execute()
        {
            level.MouseChangeLevel(direction);
        }
    }
}
