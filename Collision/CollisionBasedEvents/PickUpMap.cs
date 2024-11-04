using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class PickUpMap : IEvent
    {
        public PickUpMap() { }

        public void Execute(ICollision link, ICollision map, CollisionDirection direction)
        {
            if (link is Link actualLink && map is ItemMap actualMap)
            {
                actualMap.PickedUp = true;
                //add to link inventory
                //show map on screen
            }
        }
    }
}
