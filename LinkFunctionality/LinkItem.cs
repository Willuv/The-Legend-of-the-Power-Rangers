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
    public class LinkItem
    {

        private Texture2D itemSpriteSheet;
        private Texture2D projectileSpriteSheet;
        private Texture2D blockSpriteSheet; 
        private ILinkItemSprite item;
        public enum CreationLinkItemType
        {
            Boomerang, Arrow, Sword, Bomb, Candle, BluePortal, OrangePortal
        }

        public LinkItem(CreationLinkItemType type, Rectangle position, LinkDirection direction, Texture2D itemSpriteSheet, Texture2D projectileSpriteSheet, Texture2D blockSpriteSheet, Texture2D portalSpriteSheet)
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
                case CreationLinkItemType.BluePortal:
                    item = new BluePortalProjectileSprite(portalSpriteSheet, position, direction);
                    break;
                case CreationLinkItemType.OrangePortal:
                    item = new OrangePortalProjectileSprite(portalSpriteSheet, position, direction);
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

        //for collision. returns the item as ICollision so we don't have to go multiple classes deep
        public ICollision CollisionObject => item as ICollision;
        public IDamaging DamagingObject => item as IDamaging;
    }
}
