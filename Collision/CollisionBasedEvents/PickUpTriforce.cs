using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class PickUpTriforce : IEvent
    {
        public PickUpTriforce() { }

        public void Execute(ICollision link, ICollision triforce, CollisionDirection direction)
        {
            if (link is Link actualLink && triforce is ItemTriforce actualTriforce)
            {
                //set game state to win
            }
        }
    }
}
