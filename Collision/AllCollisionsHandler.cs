using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class AllCollisionsHandler
    {
        private readonly Dictionary<(int, int, CollisionDirection), IEvent> eventList;
        public AllCollisionsHandler()
        {
            eventList = EventListBuilder.BuildList();
        }

        public void Handle(ICollision object1, ICollision object2, CollisionDirection direction)
        {
            (object1, object2, direction) = EnsureOrder(object1, object2, direction);
            (int, int, CollisionDirection) key = KeyGenerator.Generate(object1, object2, direction);
            
            if (eventList.TryGetValue(key, out var eventCommand)) {
                eventCommand.Execute(object1, object2, direction);
            }
            else
            {
                if (object1 is not IWall && object2 is not IBlock)
                {
                    //Debug.WriteLine($"{object1}{object2}{direction} not found in eventList.");
                }
                
            }
        }

        private static (ICollision obj1, ICollision obj2, CollisionDirection dir) EnsureOrder(ICollision object1, ICollision object2, CollisionDirection direction)
        {
            if (object2 is Link || object1 is IBlock || object1 is IItem || (object1 is IEnemy && 
                object2 is Link) || object2 is ILinkItemSprite || object2 is IEnemyProjectile || 
                object1 is IDoor || object1 is IWall)
            {
                direction = ReverseDirection(direction);
                return (object2, object1, direction);
            }
            
            return (object1, object2, direction);
        }

        private static CollisionDirection ReverseDirection(CollisionDirection direction)
        {
            switch (direction)
            {
                case CollisionDirection.Left:
                    return CollisionDirection.Right;
                case CollisionDirection.Right:
                    return CollisionDirection.Left;
                case CollisionDirection.Top:
                    return CollisionDirection.Bottom;
                case CollisionDirection.Bottom:
                    return CollisionDirection.Top;
                default:
                    return direction;
            }
        }
    }
}
