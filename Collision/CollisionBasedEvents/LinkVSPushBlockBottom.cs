using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class LinkVSPushBlockBottom : IEvent
    {
        public LinkVSPushBlockBottom() { }

        public void Execute(ICollision link, ICollision pushableBlock, CollisionDirection direction)
        {
            Rectangle overlap = Rectangle.Intersect(link.DestinationRectangle, pushableBlock.DestinationRectangle);
            Rectangle newDestination = link.DestinationRectangle;

            newDestination.Y += overlap.Height;
            link.DestinationRectangle = newDestination;

            if (pushableBlock is BlockPush actualBlock && actualBlock.IsPushable && actualBlock.PushableDirection == direction)
            {
                actualBlock.Push();
            }
        }
    }
}
