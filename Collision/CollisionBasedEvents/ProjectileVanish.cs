using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class ProjectileVanish : IEvent
    {
        public ProjectileVanish() { }

        public void Execute(ICollision object1, ICollision block, CollisionDirection direction)
        {
            if (object1 is IDamaging attackObject)
            {
                attackObject.HasHitWall = true;
                //Debug.WriteLine("Projectile hit wall");
            }
        }
    }
}
