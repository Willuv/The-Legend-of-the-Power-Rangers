using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Legend_of_the_Power_Rangers.Portals;

namespace Legend_of_the_Power_Rangers
{
    public class SpawnBluePortal : IEvent
    {
        public SpawnBluePortal() { }

        public void Execute(ICollision object1, ICollision block, CollisionDirection direction)
        {
            BluePortalProjectileSprite projectile = object1 as BluePortalProjectileSprite;
            projectile.HasHitWall = true;
            PortalDelegator.RaiseBluePortalCreated(new Vector2(object1.CollisionHitbox.X, object1.CollisionHitbox.Y), direction);
            
            ProjectileVanish projectileVanish = new();
            projectileVanish.Execute(object1, block, direction);

            Debug.WriteLine("Blue portal spawned");
        }
    }
}
