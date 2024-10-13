using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision
{
    public enum CollisionDirection
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public interface ICollision
    {
        ObjectType ObjectType { get; }
        Rectangle DestinationRectangle { get; set; }
    }
}