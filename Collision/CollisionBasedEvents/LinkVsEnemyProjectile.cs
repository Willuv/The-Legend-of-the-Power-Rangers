using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;

public class LinkVSEnemyProjectile : IEvent
{
    public LinkVSEnemyProjectile() { }

    public void Execute(ICollision link, ICollision collidable, CollisionDirection direction)
    {
        if (collidable is Link)
        {
            (link, collidable) = (collidable, link);
        }

        Link actualLink = LinkManager.GetLink();
        LinkStateMachine linkStateMachine = actualLink.GetStateMachine();
        LinkStateMachine.LinkAction action = linkStateMachine.GetCurrentAction();
        LinkStateMachine.LinkDirection linkDirection = linkStateMachine.GetCurrentDirection();

        IEnemyProjectile projectile = collidable as IEnemyProjectile;
        Vector2 projectileDirection = projectile.Direction;
        bool IsMatched = SameDirection(linkDirection, projectileDirection);
        
        //if standing still and facing projectile, shield blocks
        if (action == LinkStateMachine.LinkAction.Idle && IsMatched)
        {
            ProjectileVanish vanish = new();
            vanish.Execute(collidable, null, direction);
        }
        else
        {
            LinkDecorator decoratedLink = LinkManager.GetLinkDecorator();
            LinkBecomeDamagedCommand linkGetsHurt = new(decoratedLink);
            linkGetsHurt.Execute();
        }
    }

    private static bool SameDirection(LinkStateMachine.LinkDirection linkDir, Vector2 projDir)
    {
        bool result = false;
        if (linkDir == LinkStateMachine.LinkDirection.Left && projDir.X == 1 && projDir.Y == 0) result = true;
        else if (linkDir == LinkStateMachine.LinkDirection.Up && projDir.X == 0 && projDir.Y == -1) result = true;
        else if (linkDir == LinkStateMachine.LinkDirection.Right && projDir.X == -1 && projDir.Y == 0) result = true;
        else if (linkDir == LinkStateMachine.LinkDirection.Down && projDir.X == 0 && projDir.Y == 1) result = true;

        return result;
    }
}
