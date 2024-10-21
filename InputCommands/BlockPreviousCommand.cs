using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class BlockPreviousCommand : ICommand
    {
        private readonly BlockManager blockManager;

        private readonly Game1 game1;
        public BlockPreviousCommand(BlockManager blockManager) {
            this.blockManager = blockManager;
        }
        public void Execute()
        {
            blockManager.ChangeBlock(-1);
        }
    }
}
