using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;

public class StairsEvent : IEvent
{
    private readonly int basementRoom = 18;
    public StairsEvent() { }

    public void Execute(ICollision link, ICollision stairs, CollisionDirection direction)
    {
        DelegateManager.RaiseChangeToSpecificRoom(basementRoom);
        link.CollisionHitbox = new Rectangle(270, 321, link.CollisionHitbox.Width, link.CollisionHitbox.Height);
    }
}
