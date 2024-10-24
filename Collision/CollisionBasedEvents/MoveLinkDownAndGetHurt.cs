﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class MoveLinkDownAndGetHurt : IEvent
    {
        public MoveLinkDownAndGetHurt() { }

        public void Execute(ICollision link, ICollision enemy, CollisionDirection direction)
        {
            Rectangle overlap = Rectangle.Intersect(link.DestinationRectangle, enemy.DestinationRectangle);
            Rectangle newDestination = link.DestinationRectangle;
            newDestination.Y += overlap.Height;
            link.DestinationRectangle = newDestination;

            LinkStateMachine linkStateMachine = ((Link)link).GetStateMachine();
            linkStateMachine.ChangeAction(LinkStateMachine.LinkAction.Idle);

            LinkDecorator decoratedLink = LinkManager.GetLinkDecorator();
            LinkBecomeDamagedCommand linkGetsHurt = new LinkBecomeDamagedCommand(decoratedLink);
            linkGetsHurt.Execute();
        }
    }

}
