using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legend_of_the_Power_Rangers;
using Legend_of_the_Power_Rangers.Portals;
using Microsoft.Xna.Framework;

public class LinkVSTeleporter : IEvent
{
    public LinkVSTeleporter() { }

    public void Execute(ICollision link, ICollision teleport, CollisionDirection direction)
    {
        InvisibleTeleportBlock block = teleport as InvisibleTeleportBlock;
        link = LinkManager.GetLink();
        link.CollisionHitbox = new Rectangle((int)block.DesiredPosition.X, (int)block.DesiredPosition.Y, 
            link.CollisionHitbox.Width, link.CollisionHitbox.Height);
        DelegateManager.RaiseChangeToSpecificRoom(block.DesiredRoom);
        
    }
}
