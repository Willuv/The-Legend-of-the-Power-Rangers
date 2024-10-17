﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents
{
    public class PickUpItem : IEvent
    {
        public PickUpItem() { }

        public void Execute(ICollision link, ICollision item, CollisionDirection direction)
        {
            if (link is Link actualLink && item is IItem actualItem)
            {
                actualItem.PickedUp = true;
                //some command to add to link's inventory
            }
        }
    }
}
