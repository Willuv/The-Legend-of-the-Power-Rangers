using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkInventory
    {
        private int rupeeCount {get; set;}
        private int bombCount { get; set;}
        private int keyCount { get; set;}

        public LinkInventory()
        {
            rupeeCount = 0;
            bombCount = 0;
            keyCount = 0;
        }

        private void pikcupItem(IItem item)
        {
            switch (item.ItemType)
            {
                case ItemType.BlueCandle:
                    break;
                case ItemType.BluePotion:
                    break;
                case ItemType.Bomb:
                    this.bombCount++;
                    break;
                case ItemType.Bow:
                    break;
                case ItemType.Clock:
                    break;
                case ItemType.Compass:
                    break;
                case ItemType.Fairy:
                    break;
                case ItemType.Heart:
                    break;
                case ItemType.HeartContainer:
                    break;
                case ItemType.Key:
                    this.keyCount++;
                    break;
                case ItemType.Map:
                    break;
                case ItemType.Rupee:
                    this.rupeeCount++;
                    break;
                case ItemType.Triforce:
                    break;
                case ItemType.WoodBoomerang:
                    break;


            }
        }
    }
}
