using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public interface ICollision
    {
        ObjectType ObjectType { get; }
        Rectangle CollisionHitbox { get; set; }
    }
}