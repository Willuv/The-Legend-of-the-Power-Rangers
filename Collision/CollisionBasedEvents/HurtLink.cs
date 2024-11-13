using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;

public class HurtLink : IEvent
{
    private const int KnockbackDistance = 60;  // Define your knockback distance

    public HurtLink() { }

    public void Execute(ICollision link, ICollision enemy, CollisionDirection direction)
    {
        LinkDecorator decoratedLink = LinkManager.GetLinkDecorator();

        
            LinkBecomeDamagedCommand linkGetsHurt = new(decoratedLink);
            linkGetsHurt.Execute();
        
    }
}
