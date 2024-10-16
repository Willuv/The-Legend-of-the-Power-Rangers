using Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            eventList.Add(KeyGenerator.Generate(new Link(), new BlockBlueFloor(), CollisionDirection.Left), new MoveLinkLeft());
            eventList.Add(KeyGenerator.Generate(new Link(), new BlockBlueFloor(), CollisionDirection.Top), new MoveLinkUp());
            eventList.Add(KeyGenerator.Generate(new Link(), new BlockBlueFloor(), CollisionDirection.Right), new MoveLinkRight());
            eventList.Add(KeyGenerator.Generate(new Link(), new BlockBlueFloor(), CollisionDirection.Bottom), new MoveLinkDown());
            //eventList.Add(KeyGenerator.Generate(new Link(), new BlockPush(), CollisionDirection.Left), new SlowLinkAndPushBlockRight());
            eventList.Add(KeyGenerator.Generate(new Link(), new DragonBoss(null, null), CollisionDirection.Left), new MoveLinkLeftAndGetHurt());
            //will need more obviously
            // a lot of these are placeholders to test
        }

        public void Handle(ICollision object1, ICollision object2, CollisionDirection direction)
        {
            string key = KeyGenerator.Generate(object1, object2, direction);
            
            if (eventList.TryGetValue(key, out var eventCommand)) {
                Debug.WriteLine("Command allegedly run");
                eventCommand.Execute(object1, object2, direction);
            } else
            {
                Debug.WriteLine("Collision scenario not found in eventList.");
            }
        }
    }
}
