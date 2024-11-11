using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class LinkVSEnemyTop : IEvent
    {
        private const int KnockbackDistance = 50;
        public LinkVSEnemyTop() { }

        public void Execute(ICollision link, ICollision enemy, CollisionDirection direction)
        {
            LinkStateMachine linkStateMachine = ((Link)link).GetStateMachine();
            LinkStateMachine.LinkAction action = linkStateMachine.GetCurrentAction();
            Vector2 knockback = Vector2.Zero;

            if (action == LinkStateMachine.LinkAction.Attack)
            {
                //enemy.gethurt whatever the actual method is when alex implements
                Debug.WriteLine("enemy hurt");
            }
            else
            {
                Rectangle overlap = Rectangle.Intersect(link.DestinationRectangle, enemy.DestinationRectangle);
                Rectangle newDestination = link.DestinationRectangle;
                newDestination.Y -= overlap.Height;
                link.DestinationRectangle = newDestination;

                knockback = new Vector2(0, -KnockbackDistance);
                LinkManager.GetLink().UpdatePosition(knockback);

                linkStateMachine.ChangeAction(LinkStateMachine.LinkAction.Idle);

                LinkDecorator decoratedLink = LinkManager.GetLinkDecorator();
                LinkBecomeDamagedCommand linkGetsHurt = new LinkBecomeDamagedCommand(decoratedLink);
                linkGetsHurt.Execute();
            }
        }
    }
}
