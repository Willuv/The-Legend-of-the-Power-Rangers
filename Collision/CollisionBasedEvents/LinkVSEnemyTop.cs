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
        public LinkVSEnemyTop() { }

        public void Execute(ICollision link, ICollision enemy, CollisionDirection direction)
        {
            LinkStateMachine linkStateMachine = ((Link)link).GetStateMachine();
            LinkStateMachine.LinkAction action = linkStateMachine.GetCurrentAction();
            if (action == LinkStateMachine.LinkAction.Attack)
            {
                //enemy.gethurt whatever the actual method is when alex implements
                Debug.WriteLine("enemy hurt");
            }
            else
            {
                Rectangle overlap = Rectangle.Intersect(link.CollisionHitbox, enemy.CollisionHitbox);
                Rectangle newDestination = link.CollisionHitbox;
                newDestination.Y -= overlap.Height;
                link.CollisionHitbox = newDestination;

                linkStateMachine.ChangeAction(LinkStateMachine.LinkAction.Idle);

                LinkDecorator decoratedLink = LinkManager.GetLinkDecorator();
                LinkBecomeDamagedCommand linkGetsHurt = new LinkBecomeDamagedCommand(decoratedLink);
                linkGetsHurt.Execute();
            }
        }
    }
}
