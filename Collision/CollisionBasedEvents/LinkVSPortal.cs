using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.Portals;
using Microsoft.Xna.Framework;

public class LinkVSPortal : IEvent
{
    public LinkVSPortal() { }

    public void Execute(ICollision link, ICollision collidable, CollisionDirection direction)
    {
        Link actualLink = LinkManager.GetLink();
        if (actualLink.CanTeleport())
        {
            IPortal portal = collidable as IPortal;
            if (portal != null)
            {
                actualLink.EnterPortal(); //cooldown
                PortalDelegator.RaisePortalEntered(portal);
                Debug.WriteLine("Link teleported");
            }
        }
    }
}
