using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class BombVSBombableDoor : IEvent
    {
        public BombVSBombableDoor() { }

        public void Execute(ICollision bombObj, ICollision doorObj, CollisionDirection direction)
        {
            BombSprite bomb = bombObj as BombSprite;
            holeDoor door = doorObj as holeDoor;
            if (bomb.blowing && !door.IsOpen)
            {
                door.IsOpen = true;
                door.blownUp = true;
                Debug.WriteLine("door should be open");
                if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Secret");
            }
        }
    }
}
