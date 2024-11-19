using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class PortalProjectileVSDoor : IEvent
    {
        public PortalProjectileVSDoor() { }

        public void Execute(ICollision projectile, ICollision collidable, CollisionDirection direction)
        {
            IDoor door = collidable as IDoor;
            if (!door.IsOpen)
            {
                SpawnPortal spawnPortal = new();
                spawnPortal.Execute(projectile, collidable, direction);
            } else
            {
                ProjectileVanish projectileVanish = new();
                projectileVanish.Execute(projectile, collidable, direction);
            }
        }
    }
}
