using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class MoveEnemyDown : IEvent
    {
        public MoveEnemyDown() { }

        public void Execute(ICollision link, ICollision nonMovingBlock, CollisionDirection direction)
        {
            //fix
            Rectangle overlap = Rectangle.Intersect(link.DestinationRectangle, nonMovingBlock.DestinationRectangle);
            Rectangle newDestination = link.DestinationRectangle;
           
            newDestination.Y += overlap.Height;
            link.DestinationRectangle = newDestination;
            Debug.WriteLine("MoveLinkDown is called");
        }
    }
}
