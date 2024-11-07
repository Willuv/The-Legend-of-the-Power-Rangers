using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class MoveEnemyLeft : IEvent
    {
        public MoveEnemyLeft() { }

        public void Execute(ICollision enemy, ICollision nonMovingBlock, CollisionDirection direction)
        {
            Rectangle overlap = Rectangle.Intersect(enemy.CollisionHitbox, nonMovingBlock.CollisionHitbox);
            Rectangle newDestination = enemy.CollisionHitbox;
           
            newDestination.X -= overlap.Width;
            enemy.CollisionHitbox = newDestination;
        }
    }
}
