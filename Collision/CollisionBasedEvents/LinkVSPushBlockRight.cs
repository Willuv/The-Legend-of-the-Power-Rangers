using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class LinkVSPushBlockRight : IEvent
    {
        public LinkVSPushBlockRight() { }

        public void Execute(ICollision link, ICollision pushableBlock, CollisionDirection direction)
        {
            Rectangle overlap = Rectangle.Intersect(link.CollisionHitbox, pushableBlock.CollisionHitbox);
            Rectangle newDestination = link.CollisionHitbox;

            newDestination.X += overlap.Width;
            link.CollisionHitbox = newDestination;

            if (pushableBlock is BlockPush actualBlock && actualBlock.IsPushable && actualBlock.PushableDirection == direction)
            {
                actualBlock.Push();
            }
        }
    }
}
