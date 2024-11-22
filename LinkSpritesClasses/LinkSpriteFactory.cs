using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkSpriteFactory
    {
        private Texture2D linkSpriteSheet;
        private static LinkSpriteFactory instance;
        private GameStateMachine gameStateMachine;

        public LinkSpriteFactory() { }

        public static LinkSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LinkSpriteFactory();
                }
                return instance;
            }
        }

        public void SetLinkSpriteSheet(Texture2D spriteSheet)
        {
            linkSpriteSheet = spriteSheet;
        }
        public void SetGameStateMachine(GameStateMachine gameStateMachine1)
        {
            gameStateMachine = gameStateMachine1;
        }


        public ILinkSprite CreateLinkSprite(LinkStateMachine.LinkAction action, LinkStateMachine.LinkDirection direction)
        {

            if (gameStateMachine.currentState == GameStateMachine.GameState.Winning)
            {
                return new LinkWinSprite(linkSpriteSheet);
            }

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
    }
}

