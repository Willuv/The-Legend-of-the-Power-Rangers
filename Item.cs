using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class Item
    {

        private Texture2D itemSpriteSheet;
        private Texture2D projectileSpriteSheet;
        private Vector2 position;
        private LinkState direction;
        private IitemSprite currentItem;
        public enum ItemType
        {
            Boomerang, Arrow, Sword, Bomb, Candle, NONE
        }
        private ItemType type;

        public Item(Texture2D itemSpriteSheet, Texture2D projectileSpriteSheet)
        {
            this.itemSpriteSheet = itemSpriteSheet;
            this.projectileSpriteSheet = projectileSpriteSheet;
            this.type = ItemType.NONE;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public void SetDirection(LinkState direction) 
        { 
            this.direction = direction;
        }

        public void SetType(ItemType type)
        {
            this.type = type;
        }


        //public Texture2D GetitemSpriteSheet()
        //{
        //    return itemSpriteSheet;
        //}

        //public Texture2D GetprojectileSpriteSheet()
        //{ 
        //    return projectileSpriteSheet;
        //}

        public virtual void Update(GameTime gameTime)
        {
            switch (type)
            {
                case ItemType.Bomb:
                    currentItem = new BombSprite(itemSpriteSheet, direction);
                    break;
                case ItemType.Arrow:
                    //currentItem = new ArrowSprite(projectileSpriteSheet);
                    break;
                case ItemType.Sword:
                    //currentItem = new SwordSprite(projectileSpriteSheet);
                    break;
                case ItemType.Boomerang:
                    //currentItem = new BoomerangSprite(itemSpriteSheet);
                    break;
                case ItemType.Candle:
                    //currentItem = new CandleSprite(enemySpriteSheet);
                    break;
                case ItemType.NONE:
                    currentItem = null;
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (currentItem != null)
            {
                currentItem.Draw(spriteBatch, position);
            }
        }
    }
}
