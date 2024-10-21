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

        public void Execute(ICollision link, ICollision enemy, CollisionDirection direction)
        {
            LinkDecorator decoratedLink = (LinkDecorator)LinkManager.GetLink();

            LinkBecomeDamagedCommand linkGetsHurt = new(decoratedLink);
            linkGetsHurt.Execute();
        }
    }
}
