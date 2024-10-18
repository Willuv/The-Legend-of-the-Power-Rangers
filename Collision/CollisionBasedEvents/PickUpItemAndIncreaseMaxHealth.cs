using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents
{
    public class PickUpItemAndIncreaseMaxHealth : IEvent
    {
        public PickUpItemAndIncreaseMaxHealth() { }

        public void Execute(ICollision link, ICollision item, CollisionDirection direction)
        {
            if (link is Link actualLink && item is IItem actualItem)
            {
                actualItem.PickedUp = true;
                //some command to add to link's inventory
                Debug.WriteLine("Item picked up");
                //some command to increase max health when health is implemented
            }
        }
    }
}
