﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class LinkVSAnyDoor : IEvent
    {
        public LinkVSAnyDoor() { }

        public void Execute(ICollision link, ICollision collidable, CollisionDirection direction)
        {
            IDoor door = collidable as IDoor;
            if (door.IsOpen)
            {
                DelegateManager.RaiseDoorEntered(direction);
            }
            else if (door is keyDoor && LinkManager.GetLinkInventory().GetItemCount(ItemType.Key) > 0)
            {
                LinkManager.GetLinkInventory().SetItemCount(ItemType.Key, LinkManager.GetLinkInventory().GetItemCount(ItemType.Key) - 1);
                if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Door_Unlock");
                door.IsOpen = true;
            }
            else
            {
                switch (direction)
                {
                    case CollisionDirection.Left:
                        MoveLinkLeft moveLinkLeft = new();
                        moveLinkLeft.Execute(link, collidable, direction);
                        break;
                    case CollisionDirection.Top:
                        MoveLinkUp moveLinkUp = new();
                        moveLinkUp.Execute(link, collidable, direction);
                        break;
                    case CollisionDirection.Right:
                        MoveLinkRight moveLinkRight = new();
                        moveLinkRight.Execute(link, collidable, direction);
                        break;
                    case CollisionDirection.Bottom:
                        MoveLinkDown moveLinkDown = new();
                        moveLinkDown.Execute(link, collidable, direction);
                        break;
                }
            }
        }
    }
}
