using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Portals;

internal class PortalDelegator
{
    public static event Action<Vector2, CollisionDirection> OnBluePortalCreated = delegate { };
    public static event Action<Vector2, CollisionDirection> OnOrangePortalCreated = delegate { };

    public static void RaiseBluePortalCreated(Vector2 spawnLocation, CollisionDirection direction)
    {
        OnBluePortalCreated(spawnLocation, direction);
    }

    public static void RaiseOrangePortalCreated(Vector2 spawnLocation, CollisionDirection direction)
    {
        OnOrangePortalCreated(spawnLocation, direction);
    }
}
