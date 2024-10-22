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
            (int, int, CollisionDirection) key = KeyGenerator.Generate(object1, object2, direction);
            
            if (eventList.TryGetValue(key, out var eventCommand)) {
                Debug.WriteLine($"Collision");
                eventCommand.Execute(object1, object2, direction);
            }
            else
            {
                Debug.WriteLine("Collision scenario not found in eventList.");
            }
        }
    }
}
