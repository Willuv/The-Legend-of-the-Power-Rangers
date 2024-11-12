using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class MoveLinkUp : IEvent
    {
        public MoveLinkUp() { }

        public void Execute(ICollision link, ICollision nonMovingBlock, CollisionDirection direction)
        {
            Rectangle overlap = Rectangle.Intersect(link.CollisionHitbox, nonMovingBlock.CollisionHitbox);
            Rectangle newDestination = link.CollisionHitbox;
           
            newDestination.Y -= overlap.Height;
            link.CollisionHitbox = newDestination;
        }
    }
}
