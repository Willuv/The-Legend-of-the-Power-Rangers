﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents
{
    public class MoveLinkLeftAndGetHurt : IEvent
    {
        public MoveLinkLeftAndGetHurt() { }

        public void Execute(ICollision link, ICollision enemy, CollisionDirection direction)
        {
            (link, enemy) = CollisionCaster.CastObjects(link, enemy);

            Rectangle overlap = Rectangle.Intersect(link.DestinationRectangle, enemy.DestinationRectangle);
            Rectangle newDestination = link.DestinationRectangle;
            newDestination.X -= overlap.Width;
            link.DestinationRectangle = newDestination;

            LinkStateMachine linkStateMachine = ((Link)link).GetStateMachine();
            linkStateMachine.ChangeAction(LinkStateMachine.LinkAction.Idle);

            LinkDecorator decoratedLink = (LinkDecorator)LinkManager.GetLink();
            LinkBecomeDamagedCommand linkGetsHurt = new LinkBecomeDamagedCommand(decoratedLink);
            linkGetsHurt.Execute();
        }
    }

}
