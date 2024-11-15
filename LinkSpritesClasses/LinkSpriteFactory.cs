using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkSpriteFactory
    {
        private Texture2D linkSpriteSheet;
        private static LinkSpriteFactory instance = new LinkSpriteFactory();

        private LinkSpriteFactory() { }

        public static LinkSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void SetLinkSpriteSheet(Texture2D spriteSheet)
        {
            linkSpriteSheet = spriteSheet;
        }

        public ILinkSprite CreateLinkSprite(LinkStateMachine.LinkAction action, LinkStateMachine.LinkDirection direction)
        {
            switch (action)
            {
                case LinkStateMachine.LinkAction.Idle:
                    return CreateIdleSprite(direction);
                case LinkStateMachine.LinkAction.Moving:
                    return CreateMovingSprite(direction);
                case LinkStateMachine.LinkAction.Attack:
                    return CreateAttackSprite(direction);
                case LinkStateMachine.LinkAction.Item:
                    return CreateItemSprite(direction);
                default:
                    return CreateIdleSprite(direction);
            }
        }

        private ILinkSprite CreateIdleSprite(LinkStateMachine.LinkDirection direction)
        {
            switch (direction)
            {
                case LinkStateMachine.LinkDirection.Right:
                    return new LinkIdleRight(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Left:
                    return new LinkIdleLeft(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Up:
                    return new LinkIdleUp(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Down:
                    return new LinkIdleDown(linkSpriteSheet);
                default:
                    return new LinkIdleRight(linkSpriteSheet);
            }
        }

        private ILinkSprite CreateMovingSprite(LinkStateMachine.LinkDirection direction)
        {
            switch (direction)
            {
                case LinkStateMachine.LinkDirection.Right:
                    return new LinkRightSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Left:
                    return new LinkLeftSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Up:
                    return new LinkUpSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Down:
                    return new LinkDownSprite(linkSpriteSheet);
                default:
                    return new LinkRightSprite(linkSpriteSheet);
            }
        }

        private ILinkSprite CreateAttackSprite(LinkStateMachine.LinkDirection direction)
        {
            switch (direction)
            {
                case LinkStateMachine.LinkDirection.Right:
                    return new LinkAttackRightSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Left:
                    return new LinkAttackLeftSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Up:
                    return new LinkAttackUpSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Down:
                    return new LinkAttackDownSprite(linkSpriteSheet);
                default:
                    return new LinkAttackRightSprite(linkSpriteSheet);
            }
        }

        private ILinkSprite CreateItemSprite(LinkStateMachine.LinkDirection direction)
        {
            switch (direction)
            {
                case LinkStateMachine.LinkDirection.Right:
                    return new LinkItemRightSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Left:
                    return new LinkItemLeftSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Up:
                    return new LinkItemUpSprite(linkSpriteSheet);
                case LinkStateMachine.LinkDirection.Down:
                    return new LinkItemDownSprite(linkSpriteSheet);
                default:
                    return new LinkItemRightSprite(linkSpriteSheet);
            }
        }
        public ILinkSprite CreateWinSprite()
        {
            return new LinkWinSprite(linkSpriteSheet);
        }
    }
}

