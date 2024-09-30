using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkItem3Command : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        private readonly LinkItemFactory linkItemFactory;
        public LinkItem3Command(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory)
        {
            this.stateMachine = stateMachine;
            this.linkItemFactory = linkItemFactory;
        }
        public void Execute()
        {
            this.stateMachine.ChangeAction(LinkStateMachine.LinkAction.Item);
            this.linkItemFactory.CreateItem(Item.ItemType.Sword);
        }
    }
}
