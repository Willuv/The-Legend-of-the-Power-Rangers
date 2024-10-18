using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents
{
    public class HurtLink : IEvent
    {
        public HurtLink() { }

        public void Execute(ICollision link, ICollision fire, CollisionDirection direction)
        {
            LinkBecomeDamagedCommand linkGetsHurt = new(new LinkDecorator((Link)link)); //idk fix this to work with will's version
        }
    }
}
