using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision
{
    public class AllCollisionsHandler
    {
        private readonly Dictionary<int, IEvent> eventList;
        public AllCollisionsHandler()
        {
            eventList = new Dictionary<int, IEvent>();
        }

        public void Handle(ICollision object1, ICollision object2, CollisionDirection direction)
        {
            
        }

        public void ReportError()
        {

        }
    }
}
