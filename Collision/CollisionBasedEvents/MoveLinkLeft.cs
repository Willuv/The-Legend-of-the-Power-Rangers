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

        public static void Execute(ICollision link, ICollision nonmovingBlock, CollisionDirection direction)
        {
            Rectangle overlap = Rectangle.Intersect(link.DestinationRectangle, nonmovingBlock.DestinationRectangle);
            int shift = overlap.Width;
            
        }
    }
}
