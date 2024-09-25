using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Legend_of_the_Power_Rangers.Item;

namespace Legend_of_the_Power_Rangers
{
    public class LinkStateMachine
    {
        public enum LinkState
        {
            Left, Right, Up, Down, Idle, Attack,
            Item1, Item2, Item3, Item4, Item5
        }

        private LinkState currentState;
        private LinkState lastDirection;
        private Texture2D linkSpriteSheet;
        private ILinkSprite currentSprite;
        private Texture2D itemSpriteSheet;
        private Texture2D projectileSpriteSheet;
        private Item item;

        public LinkStateMachine(Item item, Texture2D spriteSheet, Texture2D itemSheet, Texture2D projectileSheet)
        {
            linkSpriteSheet = spriteSheet;
            itemSpriteSheet = itemSheet;
            projectileSpriteSheet = projectileSheet;
            currentState = LinkState.Idle;
            lastDirection = LinkState.Right;
            currentSprite = new LinkRightSprite(linkSpriteSheet);
            this.item = item;
        }

        public void ChangeState(LinkState newState)
        {
            if (newState == currentState)
            {
                return;
            }
            if (newState == LinkState.Attack)
            {
                ChangeAttackState();
            }
            else if (newState == LinkState.Item1 || newState == LinkState.Item2 ||
                     newState == LinkState.Item3 || newState == LinkState.Item4 ||
                     newState == LinkState.Item5)
            {
                ChangeItemState(newState);
            }
            else
            {
                lastDirection = newState;
                currentState = newState;
                ChangeDirectionState();
            }
        }


        private void ChangeDirectionState()
        {
            switch (currentState)
            {
                case LinkState.Right:
                    currentSprite = new LinkRightSprite(linkSpriteSheet);
                    break;
                case LinkState.Left:
                    currentSprite = new LinkLeftSprite(linkSpriteSheet);
                    break;
                case LinkState.Up:
                    currentSprite = new LinkUpSprite(linkSpriteSheet);
                    break;
                case LinkState.Down:
                    currentSprite = new LinkDownSprite(linkSpriteSheet);
                    break;

            }
        } 
        private void ChangeAttackState()
            {
                switch(currentState)
                {
                    case LinkState.Right:
                        currentSprite = new LinkAttackRightSprite(linkSpriteSheet);
                        break;
                    case LinkState.Left:
                        currentSprite = new LinkAttackLeftSprite(linkSpriteSheet);
                        break;
                    case LinkState.Up:
                        currentSprite = new LinkAttackUpSprite(linkSpriteSheet);
                        break;
                    case LinkState.Down:
                        currentSprite = new LinkAttackDownSprite(linkSpriteSheet);
                        break;
                }

            currentState = LinkState.Attack;
            }
        private void ChangeItemState(LinkState itemState)
        {
            switch (lastDirection)
            {
                case LinkState.Right:
                    currentSprite = new LinkItemRightSprite(linkSpriteSheet);
                    ItemState(itemState, lastDirection);
                    break;
                case LinkState.Left:
                    currentSprite = new LinkItemLeftSprite(linkSpriteSheet);
                    ItemState(itemState, lastDirection);
                    break;
                case LinkState.Up:
                    currentSprite = new LinkItemUpSprite(linkSpriteSheet);
                    ItemState(itemState, lastDirection);
                    break;
                case LinkState.Down:
                    currentSprite = new LinkItemDownSprite(linkSpriteSheet);
                    ItemState(itemState, lastDirection);
                    break;
            }
            currentState = lastDirection;
        }

        private void ItemState(LinkState itemState, LinkState direction)
        {
            switch (itemState)
            {
                case LinkState.Item1:
                    item.SetType(ItemType.Bomb);
                    break;
                case LinkState.Item2:
                    item.SetType(ItemType.Arrow);                    
                    break;
                case LinkState.Item3:
                    item.SetType(ItemType.Sword);
                    break;
                case LinkState.Item4:
                    item.SetType(ItemType.Boomerang);
                    break;
                case LinkState.Item5:
                    item.SetType(ItemType.Candle);
                    break;
            }
        }

        public ILinkSprite GetCurrentSprite()
        {
            return currentSprite;
        }
        public LinkState GetCurrentState()
        {
            return currentState;
        }

        public LinkState GetLastDirection()
        {
            return lastDirection;
        }

    }
}
