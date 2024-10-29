using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class MoveEnemyRight : IEvent
    {
        public MoveEnemyRight() { }

        public void Execute(ICollision enemy, ICollision nonMovingBlock, CollisionDirection direction)
        {
            Rectangle overlap = Rectangle.Intersect(enemy.DestinationRectangle, nonMovingBlock.DestinationRectangle);
            Rectangle newDestination = enemy.DestinationRectangle;
           
            newDestination.X += overlap.Width;
            enemy.DestinationRectangle = newDestination;
        }
    }
}
