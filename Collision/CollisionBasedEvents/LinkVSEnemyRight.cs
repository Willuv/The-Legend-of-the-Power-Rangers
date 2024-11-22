using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class LinkVSEnemyRight : IEvent
    {
        private const int KnockbackDistance = 30;
        public LinkVSEnemyRight() { }

        public void Execute(ICollision link, ICollision enemy, CollisionDirection direction)
        {
            LinkStateMachine linkStateMachine = ((Link)link).GetStateMachine();
            LinkStateMachine.LinkAction action = linkStateMachine.GetCurrentAction();
            Vector2 knockback = Vector2.Zero;
            IEnemy enemy1 = (IEnemy)enemy;


            if (action == LinkStateMachine.LinkAction.Attack)
            {
                if (!enemy1.IsHurt())
                {
                    enemy1.TakeDamage(1);

                    if (!AudioManager.Instance.IsMuted())
                    {
                        string sound = enemy1.EnemyType != EnemyType.DragonBoss ? "Enemy_Hit" : "Boss_Hit";
                        AudioManager.Instance.PlaySound(sound);
                    }

                }
            }
            else
            {
                Rectangle overlap = Rectangle.Intersect(link.CollisionHitbox, enemy.CollisionHitbox);
                Rectangle newDestination = link.CollisionHitbox;
                newDestination.X += overlap.Width;
                link.CollisionHitbox = newDestination;

                knockback = new Vector2(KnockbackDistance, 0);
                LinkManager.GetLink().UpdatePosition(knockback);

                linkStateMachine.ChangeAction(LinkStateMachine.LinkAction.Idle);

                LinkDecorator decoratedLink = LinkManager.GetLinkDecorator();
                LinkBecomeDamagedCommand linkGetsHurt = new LinkBecomeDamagedCommand(decoratedLink);
                linkGetsHurt.Execute();
            }
        }
    }
}
