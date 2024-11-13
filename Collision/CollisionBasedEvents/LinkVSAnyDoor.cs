using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class LinkVSAnyDoor : IEvent
    {
        public LinkVSAnyDoor() { }

        public void Execute(ICollision link, ICollision collidable, CollisionDirection direction)
        {
            IDoor door = collidable as IDoor;
            if (door.IsOpen)
            {
                DelegateManager.RaiseDoorEntered(direction.ToString());

                switch (direction)
                {
                    case CollisionDirection.Left:
                        //camera move to right room
                        Debug.WriteLine("moving to right room");
                        break;
                    case CollisionDirection.Top:
                        //camera move to down room
                        Debug.WriteLine("moving to down room");
                        break;
                    case CollisionDirection.Right:
                        //camera move to left room
                        Debug.WriteLine("moving to left room");
                        break;
                    case CollisionDirection.Bottom:
                        //camera move to up room
                        Debug.WriteLine("moving to up room");
                        break;
                }
            } else
            {
                switch (direction)
                {
                    case CollisionDirection.Left:
                        MoveLinkLeft moveLinkLeft = new();
                        moveLinkLeft.Execute(link, collidable, direction);
                        break;
                    case CollisionDirection.Top:
                        MoveLinkUp moveLinkUp = new();
                        moveLinkUp.Execute(link, collidable, direction);
                        break;
                    case CollisionDirection.Right:
                        MoveLinkRight moveLinkRight = new();
                        moveLinkRight.Execute(link, collidable, direction);
                        break;
                    case CollisionDirection.Bottom:
                        MoveLinkDown moveLinkDown = new();
                        moveLinkDown.Execute(link, collidable, direction);
                        break;
                }
            }
        }
    }
}
