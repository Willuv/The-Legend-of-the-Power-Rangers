﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class ShootBluePortal : ICommand
    {
        private readonly LinkStateMachine stateMachine;
        private readonly LinkItemFactory linkItemFactory;
        public ShootBluePortal(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory) 
        {
            this.stateMachine = stateMachine;
            this.linkItemFactory = linkItemFactory;
        }
        public void Execute()
        {
            //if (LinkManager.GetLinkInventory().GetItemCount(ItemType.PortalGun) > 0)
            {
                this.stateMachine.ChangeAction(LinkStateMachine.LinkAction.Item);
                this.linkItemFactory.CreateItem(LinkItem.CreationLinkItemType.BluePortal);
                Debug.WriteLine("blue portal shot");
            }
        }
    }
}