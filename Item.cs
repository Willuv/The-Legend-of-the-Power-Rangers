﻿using Microsoft.Xna.Framework.Graphics;
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
        private Texture2D blockSpriteSheet; 
        private IitemSprite item;
        public enum CreationLinkItemType
        {
            Boomerang, Arrow, Sword, Bomb, Candle
        }

        public Item(CreationLinkItemType type, Rectangle position, LinkDirection direction, Texture2D itemSpriteSheet, Texture2D projectileSpriteSheet, Texture2D blockSpriteSheet)
        {
            switch (type)
            {
                case CreationLinkItemType.Bomb:
                    item = new BombSprite(itemSpriteSheet, position, direction);
                    break;
                case CreationLinkItemType.Arrow:
                    item = new ArrowSprite(projectileSpriteSheet, position, direction);
                    break;
                case CreationLinkItemType.Sword:
                    item = new SwordSprite(projectileSpriteSheet, position, direction);
                    break;
                case CreationLinkItemType.Boomerang:
                    item = new BoomerangSprite(itemSpriteSheet, position, direction);
                    break;
                case CreationLinkItemType.Candle:
                    item = new CandleSprite(blockSpriteSheet, position, direction);
                    break;
            }
            this.itemSpriteSheet = itemSpriteSheet;
            this.projectileSpriteSheet = projectileSpriteSheet;
            this.blockSpriteSheet = blockSpriteSheet;
        }
        public virtual void Update(GameTime gameTime)
        {
            item.Update(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch);
        }
        public bool GetState()
        {
            return item.GetState();
        }
    }
}
