using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class MoveEnemyDown : IEvent
    {
        public MoveEnemyDown() { }

        public void Execute(ICollision enemy, ICollision nonMovingBlock, CollisionDirection direction)
        {
            Rectangle overlap = Rectangle.Intersect(enemy.CollisionHitbox, nonMovingBlock.CollisionHitbox);
            Rectangle newDestination = enemy.CollisionHitbox;
           
            newDestination.Y += overlap.Height;
            enemy.CollisionHitbox = newDestination;
        }
    }
}
