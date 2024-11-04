using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class MoveLinkLeftAndPushBlockRight : IEvent
    {
        public MoveLinkLeftAndPushBlockRight() { }

        public void Execute(ICollision link, ICollision pushableBlock, CollisionDirection direction)
        {

        }
    }
}
