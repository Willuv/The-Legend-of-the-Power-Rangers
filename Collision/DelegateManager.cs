using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers;

internal class DelegateManager
{
    public static event Action<ICollision> OnObjectCreated = delegate { };
    public static event Action<ICollision> OnObjectRemoved = delegate { };
    public static event Action<CollisionDirection> OnDoorEntered = delegate { };
    public static event Action<int> OnChangeToSpecificRoom = delegate { };

    public static void RaiseObjectCreated(ICollision newObject)
    {
        OnObjectCreated(newObject);
    }

    public static void RaiseObjectRemoved(ICollision removedObject)
    {
        OnObjectRemoved(removedObject);
    }

    public static void RaiseDoorEntered(CollisionDirection direction)
    {
        OnDoorEntered(direction);
    }

    public static void RaiseChangeToSpecificRoom(int room)
    {
        OnChangeToSpecificRoom(room);
    }
}
