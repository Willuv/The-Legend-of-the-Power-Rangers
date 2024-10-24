﻿using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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
        AllCollisionsHandler allCollisionsHandler;
        public CollisionManager()
        {
            allCollisionsHandler = new();
        }
        public void Update(GameTime gameTime, List<ICollision> loadedObjects)
        {
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

                    //temp checks to see if collision error is a case we care about or not
                    if (object1 is IBlock && object2 is IBlock) continue;
                    if (object1 is IItem && object2 is IBlock) continue;
                    if (object1 is IBlock && object2 is IItem) continue;
                    if (object1 is IEnemy && object2 is IItem) continue;
                    if (object1 is IItem && object2 is IEnemy) continue;
                    if (object1 is IEnemy && object2 is BlockSquare) continue;
                    if (object1 is Link && object2 is BlockSquare) continue;
                    if (object1 is BlockSquare && object2 is Link) continue;
                    if (object1 is BlockSquare && object2 is IEnemy) continue;

                    if (object2.DestinationRectangle.Left > object1.DestinationRectangle.Right)
                    {
                        break; //object 2 doesn't overlap at all
                    }

                    if (object1.DestinationRectangle.Intersects(object2.DestinationRectangle))
                    {
                        HandleCollision(object1, object2);
                    }
                }
            }
        }

        private void HandleCollision(ICollision object1, ICollision object2)
        {
            //Debug.WriteLine($"Collision detected between {object1} and {object2} in direction");
            Rectangle intersection = Rectangle.Intersect(object1.DestinationRectangle,
                                                        object2.DestinationRectangle);
            if (intersection.Width > intersection.Height)
            {
                //collision is from top or from bottom
                if (object1.DestinationRectangle.Top < object2.DestinationRectangle.Top)
                {
                    //object 1 is on top
                    allCollisionsHandler.Handle(object1, object2, CollisionDirection.Top);
                } else if (object1.DestinationRectangle.Bottom > object2.DestinationRectangle.Bottom)
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
                if (object1.DestinationRectangle.Left < object2.DestinationRectangle.Left)
                {
                    //object 1 is on left
                    allCollisionsHandler.Handle(object1, object2, CollisionDirection.Left);
                } else if (object1.DestinationRectangle.Right > object2.DestinationRectangle.Right)
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