using Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision
{
    public class AllCollisionsHandler
    {
        private readonly Dictionary<string, IEvent> eventList;
        public AllCollisionsHandler()
        {
            eventList = new Dictionary<string, IEvent>();
            InitializeEventList();
        }

        private void InitializeEventList()
        {
            eventList.Add(KeyGenerator.Generate(ObjectType.Link, BlockType.BlueFloor, CollisionDirection.Left), new MoveLinkLeft());
            //will need more obviously
        }

        public void Handle(ICollision object1, ICollision object2, CollisionDirection direction)
        {
            (object1, object2) = CollisionCaster.CastObjects(object1, object2);
            string key = KeyGenerator.Generate(object1.ObjectType, object2.ObjectType, direction);
            
            if (eventList.TryGetValue(key, out var eventCommand)) {
                eventCommand.Execute(object1, object2, direction);
            } else
            {
                throw new InvalidOperationException("Collision scenario not found in eventList.");
            }
        }
    }
}
