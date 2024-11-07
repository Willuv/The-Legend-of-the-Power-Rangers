using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{

    public class CollisionManager
    {
        private readonly AllCollisionsHandler allCollisionsHandler;
        private List<ICollision> loadedObjects;
        public CollisionManager()
        {
            allCollisionsHandler = new();
            loadedObjects = new();

            DelegateManager.OnObjectCreated += (obj) =>
            {
                if (obj != null)
                {
                    loadedObjects.Add(obj);
                    //Debug.WriteLine("projectile added");
                }
            };
            DelegateManager.OnObjectRemoved += (obj) =>
            {
                if (obj != null)
                {
                    loadedObjects.Remove(obj);
                    //Debug.WriteLine("projectile removed");
                }
            };
        }
        public void Update(GameTime gameTime, List<ICollision> loadedObjects)
        {
            this.loadedObjects = loadedObjects;

            //Sort
            SortingMachine.BubbleSort(loadedObjects);

            //Sweep
            Sweep(loadedObjects);
        }

        private void Sweep(List<ICollision> loadedObjects)
        {
            for (int i = 0; i < loadedObjects.Count - 1; i++)
            {
                for (int j = i + 1; j < loadedObjects.Count; j++)
                {
                    ICollision object1 = loadedObjects[i];
                    ICollision object2 = loadedObjects[j];

                    if (object2.CollisionHitbox.Left > object1.CollisionHitbox.Right)
                    {
                        break; //object 2 doesn't overlap at all
                    }

                    if (object1.CollisionHitbox.Intersects(object2.CollisionHitbox))
                    {
                        HandleCollision(object1, object2);
                    }
                }
            }
        }

        private void HandleCollision(ICollision object1, ICollision object2)
        {
            //Debug.WriteLine($"Collision detected between {object1} and {object2} in direction");
            Rectangle intersection = Rectangle.Intersect(object1.CollisionHitbox,
                                                        object2.CollisionHitbox);
            if (intersection.Width > intersection.Height)
            {
                //collision is from top or from bottom
                if (object1.CollisionHitbox.Top < object2.CollisionHitbox.Top)
                {
                    //object 1 is on top
                    allCollisionsHandler.Handle(object1, object2, CollisionDirection.Top);
                } else if (object1.CollisionHitbox.Bottom > object2.CollisionHitbox.Bottom)
                {
                    //object 1 is on bottom
                    allCollisionsHandler.Handle(object1, object2, CollisionDirection.Bottom);
                } else
                {
                    //Debug.WriteLine("object 1 is not top or bottom.");
                }
            } else if (intersection.Height > intersection.Width)
            {
                //collision is from left or right
                if (object1.CollisionHitbox.Left < object2.CollisionHitbox.Left)
                {
                    //object 1 is on left
                    allCollisionsHandler.Handle(object1, object2, CollisionDirection.Left);
                } else if (object1.CollisionHitbox.Right > object2.CollisionHitbox.Right)
                {
                    //object 1 is on right
                    allCollisionsHandler.Handle(object1, object2, CollisionDirection.Right);
                } else
                {
                    //Debug.WriteLine("object1 is not left or right.");
                }
            }
        }
    }
}