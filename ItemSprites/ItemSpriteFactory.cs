using Microsoft.Xna.Framework.Graphics;
using System;

namespace Legend_of_the_Power_Rangers
{
    public class ItemSpriteFactory
    {
        private static ItemSpriteFactory instance = new ItemSpriteFactory();
        private Texture2D itemSpritesheet;

        public static ItemSpriteFactory Instance
        {
            get { return instance; }
        }

        private ItemSpriteFactory() { }

        public void SetItemSpritesheet(Texture2D spritesheet)
        {
            itemSpritesheet = spritesheet;
        }

        public IItem CreateItem(string itemType)
        {
            switch (itemType)
            {
                case "Compass":
                    return new ItemCompass();
                case "Map":
                    return new ItemMap();
                case "Key":
                    return new ItemKey();
                case "HeartContainer":
                    return new ItemHeartContainer();
                case "Triforce":
                    return new ItemTriforce();
                case "WoodBoomerang":
                    return new ItemWoodBoomerang();
                case "Bow":
                    return new ItemBow();
                case "Heart":
                    return new ItemHeart();
                case "Rupee":
                    return new ItemRupee();
                case "Bomb":
                    return new ItemBomb();
                case "Fairy":
                    return new ItemFairy();
                case "Clock":
                    return new ItemClock();
                case "BlueCandle":
                    return new ItemBlueCandle();
                case "BluePotion":
                    return new ItemBluePotion();
                default:
                    throw new ArgumentException($"Item type {itemType} not recognized");
            }
        }

        public Texture2D GetItemSpritesheet() => itemSpritesheet;
    }
}
