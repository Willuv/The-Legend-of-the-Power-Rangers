using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class BombVSBombableWall : IEvent
    {
        public BombVSBombableWall() { }

        public void Execute(ICollision bombObj, ICollision doorObj, CollisionDirection direction)
        {
            BombSprite bomb = bombObj as BombSprite;
            BlockBombedWall wall = doorObj as BlockBombedWall;
            //if (bomb.blowing && )
            {

            }
        }
    }
}
