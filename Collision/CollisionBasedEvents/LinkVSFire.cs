﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;

public class LinkVSFire : IEvent
{
    public LinkVSFire() { }

    public void Execute(ICollision link, ICollision collidable, CollisionDirection direction)
    {
        LinkDecorator decoratedLink = LinkManager.GetLinkDecorator();
        LinkBecomeDamagedCommand linkGetsHurt = new(decoratedLink);
        linkGetsHurt.Execute();
    }
}
