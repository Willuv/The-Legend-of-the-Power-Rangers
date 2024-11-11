using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkSwordCommand : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        private readonly LinkItemFactory linkItemFactory;
        public LinkSwordCommand(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory) {
            this.stateMachine = stateMachine;
            this.linkItemFactory = linkItemFactory;
        }
        public void Execute()
        {
            stateMachine.ChangeAction(LinkStateMachine.LinkAction.Attack);
            if(LinkManager.GetLink().GetCurrentHealth() == LinkManager.GetLink().GetMaxHealth())
            {
                this.stateMachine.ChangeAction(LinkStateMachine.LinkAction.Item);
                this.linkItemFactory.CreateItem(LinkItem.CreationLinkItemType.Sword);
            }
        }
    }
}
