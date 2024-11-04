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

    public static void RaiseObjectCreated(ICollision newObject)
    {
        OnObjectCreated(newObject);
    }

    public static void RaiseObjectRemoved(ICollision removedObject)
    {
        OnObjectRemoved(removedObject);
    }
}
