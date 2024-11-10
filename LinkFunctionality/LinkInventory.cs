using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkInventory
    {
        private Dictionary<ItemType, int> itemCounts = new Dictionary<ItemType, int>();
        private HashSet<ItemType> obtainedItems = new HashSet<ItemType>();

        public LinkInventory()
        {
            itemCounts[ItemType.Rupee] = 0;
            itemCounts[ItemType.Bomb] = 0;
            itemCounts[ItemType.Key] = 0;
        }

        public void PickUpItem(IItem item)
        {
            switch (item.ItemType)
            {
                case ItemType.Rupee:
                case ItemType.Key:
                    itemCounts[item.ItemType]++;
                    break;
                case ItemType.Bomb:
                    itemCounts[item.ItemType] = itemCounts[item.ItemType] + 4;
                    break;
                case ItemType.Fairy:
                case ItemType.Heart:
                    LinkManager.GetLink().Heal(2);
                    break;
                case ItemType.HeartContainer:
                    LinkManager.GetLink().IncreaseMaxHealth();
                    break;
                default:
                    obtainedItems.Add(item.ItemType);
                    break;
            }
        }

        public int GetItemCount(ItemType itemType)
        {
            return itemCounts.ContainsKey(itemType) ? itemCounts[itemType] : 0;
        }

        public void SetItemCount(ItemType itemType, int count)
        {
            itemCounts[itemType] = count;  
        }

        public bool HasItem(ItemType itemType)
        {
            return obtainedItems.Contains(itemType);
        }

    }
}
