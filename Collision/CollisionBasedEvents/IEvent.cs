using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents
{
    public interface IEvent
    {
        void Execute(ICollision object1, ICollision object2, CollisionDirection direction);
    }
}
