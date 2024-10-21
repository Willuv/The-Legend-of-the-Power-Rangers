using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Legend_of_the_Power_Rangers
{
    public class ItemShowNextCommand : ICommand
    {
        private readonly ItemManager itemManager;

        public ItemShowNextCommand(ItemManager itemManager) {
            this.itemManager = itemManager;
        }
        public void Execute()
        {
            itemManager.ChangeItem(1);
        }
    }
}
