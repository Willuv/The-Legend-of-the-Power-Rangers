using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class PickUpItem : IEvent
    {
        public PickUpItem() { }

        public void Execute(ICollision link, ICollision item, CollisionDirection direction)
        {
            if (link is Link actualLink && item is IItem actualItem)
            {
                actualItem.PickedUp = true;
                DelegateManager.RaiseObjectRemoved(actualItem);
                //some command to add to link's inventory
                //LinkManager.GetLink().PickupItem(actualItem)

                if (!AudioManager.Instance.IsMuted())
                {
                    if ((actualItem.ItemType == ItemType.Heart || actualItem.ItemType == ItemType.Key))
                    {
                        AudioManager.Instance.PlaySound("Get_Heart");
                    }
                    else if (actualItem.ItemType == ItemType.Rupee)
                    {
                        AudioManager.Instance.PlaySound("Get_Rupee");
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("Get_Item");
                    }
                }
                }
            }
    }
}
