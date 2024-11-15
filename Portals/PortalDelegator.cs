using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Portals;

internal class PortalDelegator
{
    public static event Action<Rectangle, CollisionDirection> OnBluePortalCreated = delegate { };
    public static event Action<Rectangle, CollisionDirection> OnOrangePortalCreated = delegate { };

    public static void RaiseBluePortalCreated(Rectangle spawnLocation, CollisionDirection direction)
    {
        OnBluePortalCreated(spawnLocation, direction);
    }

    public static void RaiseOrangePortalCreated(Rectangle spawnLocation, CollisionDirection direction)
    {
        OnOrangePortalCreated(spawnLocation, direction);
    }
}
