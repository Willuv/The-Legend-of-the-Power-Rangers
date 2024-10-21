using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Legend_of_the_Power_Rangers
{
    public class ItemManager
    {
        private List<IItem> items = new List<IItem>();
        private int currentItemIndex = 0;

        public ItemManager(List<string> itemTypes)
        {
            InitializeItems(itemTypes);
        }

        private void InitializeItems(List<string> itemTypes)
        {
            foreach (var itemType in itemTypes)
            {
                items.Add(ItemSpriteFactory.Instance.CreateItem(itemType));
            }
        }

        public void ChangeItem(int direction)
        {
            currentItemIndex += direction;

            if (currentItemIndex >= items.Count)
            {
                currentItemIndex = 0;
            }
            else if (currentItemIndex < 0)
            {
                currentItemIndex = items.Count - 1;
            }
        }

        public IItem GetCurrentItem()
        {
            return items[currentItemIndex];
        }

        public List<IItem> GetItems()
        {
            return items;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var item in items)
            {
                item.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            IItem currentItem = GetCurrentItem();
            currentItem.Draw(spriteBatch);
        }
    }
}
