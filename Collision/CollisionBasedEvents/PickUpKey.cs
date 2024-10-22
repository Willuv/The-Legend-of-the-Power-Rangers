using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class PickUpKey : IEvent
    {
        public PickUpKey() { }

        public void Execute(ICollision link, ICollision key, CollisionDirection direction)
        {
            if (link is Link actualLink && key is ItemKey actualKey)
            {
                actualKey.PickedUp = true;
                //add to link inventory
                //set room to be unlocked
            }
        }
    }
}
