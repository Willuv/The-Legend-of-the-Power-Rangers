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
            //Link can either go through door or can't
            IDoor door = collidable as IDoor;
            //if (door.IsOpen)
            //{
            //    switch (direction)
            //    {
            //        case CollisionDirection.Left:
            //            IEvent doorLeft = new
            //    }
            //}
        }
    }
}
