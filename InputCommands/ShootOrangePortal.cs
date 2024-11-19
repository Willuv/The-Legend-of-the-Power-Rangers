using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class ShootOrangePortal : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        private readonly LinkItemFactory linkItemFactory;
        public ShootOrangePortal(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory) 
        {
            this.stateMachine = stateMachine;
            this.linkItemFactory = linkItemFactory;
        }
        public void Execute()
        {
            LinkInventory inventory = LinkManager.GetLinkInventory();
            if (inventory.GetItemCount(ItemType.PortalGun) > 0 && inventory.ActiveItem == ItemType.PortalGun)
                
            {
                this.stateMachine.ChangeAction(LinkStateMachine.LinkAction.Item);
                this.linkItemFactory.CreateItem(LinkItem.CreationLinkItemType.OrangePortal);
            }
        }
    }
}
