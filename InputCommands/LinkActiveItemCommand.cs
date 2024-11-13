using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkActiveItemCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        private readonly LinkItemFactory linkItemFactory;
        private readonly LinkInventory linkInventory;
        public LinkActiveItemCommand(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory, LinkInventory linkInventory)
        {
            this.stateMachine = stateMachine;
            this.linkItemFactory = linkItemFactory;
            this.linkInventory = linkInventory;
        }
        public void Execute()
        {
            switch (linkInventory.ActiveItem)
            {
                case ItemType.WoodBoomerang:
                    this.stateMachine.ChangeAction(LinkStateMachine.LinkAction.Item);
                    this.linkItemFactory.CreateItem(LinkItem.CreationLinkItemType.Boomerang);
                    break;
                case ItemType.Bomb:
                    this.stateMachine.ChangeAction(LinkStateMachine.LinkAction.Item);
                    this.linkItemFactory.CreateItem(LinkItem.CreationLinkItemType.Bomb);
                    LinkManager.GetLinkInventory().SetItemCount(ItemType.Bomb, LinkManager.GetLinkInventory().GetItemCount(ItemType.Bomb) - 1);
                    break;
                case ItemType.Bow:
                    this.stateMachine.ChangeAction(LinkStateMachine.LinkAction.Item);
                    this.linkItemFactory.CreateItem(LinkItem.CreationLinkItemType.Arrow);
                    break;
                default:
                    break;
            }
        }
    }
}