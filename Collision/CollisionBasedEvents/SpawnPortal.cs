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
    public class SpawnPortal : IEvent
    {
        public SpawnPortal() { }

        public void Execute(ICollision object1, ICollision block, CollisionDirection direction)
        {
            if (object1 is BluePortalProjectileSprite)
            {
                BluePortalProjectileSprite projectile = object1 as BluePortalProjectileSprite;
                projectile.HasHitWall = true;
                PortalDelegator.RaiseBluePortalCreated(new Vector2(object1.CollisionHitbox.X, object1.CollisionHitbox.Y), direction);

                ProjectileVanish projectileVanish = new();
                projectileVanish.Execute(object1, block, direction);

                Debug.WriteLine("Blue portal spawned");
            } else //orange
            {
                OrangePortalProjectileSprite projectile = object1 as OrangePortalProjectileSprite;
                projectile.HasHitWall = true;
                PortalDelegator.RaiseOrangePortalCreated(new Vector2(object1.CollisionHitbox.X, object1.CollisionHitbox.Y), direction);

                ProjectileVanish projectileVanish = new();
                projectileVanish.Execute(object1, block, direction);

                Debug.WriteLine("Orange portal spawned");
            }
        }
    }
}
