using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents
{
    public class MoveLinkLeft : IEvent
    {
        public MoveLinkLeft() { }

        public void Execute(ICollision link, ICollision nonMovingBlock, CollisionDirection direction)
        {
            (link, nonMovingBlock) = CollisionCaster.CastObjects(link, nonMovingBlock);
            Rectangle overlap = Rectangle.Intersect(link.DestinationRectangle, nonMovingBlock.DestinationRectangle);
            Rectangle newDestination = link.DestinationRectangle; // Get the current rectangle
            newDestination.X -= overlap.Width; // Move it left by the overlap width
            link.DestinationRectangle = newDestination;
        }
    }
}
