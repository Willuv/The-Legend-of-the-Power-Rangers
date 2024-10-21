using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class ItemShowPreviousCommand : ICommand
    {
        private readonly ItemManager itemManager;

        public ItemShowPreviousCommand(ItemManager itemManager) {
            this.itemManager = itemManager;
        }
        public void Execute()
        {
            itemManager.ChangeItem(-1);
        }
    }
}
