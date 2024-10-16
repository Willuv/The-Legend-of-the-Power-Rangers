using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents
{
    public class MoveLinkLeftAndGetHurt : IEvent
    {
        public MoveLinkLeftAndGetHurt() { }

        public void Execute(ICollision link, ICollision enemy, CollisionDirection direction)
        {
            Debug.WriteLine("Link hits an enemy");
        }
    }
}
