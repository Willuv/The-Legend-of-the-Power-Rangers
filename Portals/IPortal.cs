using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Portals
{
    public interface IPortal : ICollision
    {
        PortalType PortalType { get; }
    }
}
