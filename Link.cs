using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class Link
    {
        private LinkStateMachine stateMachine;

        public Link()
        {
            stateMachine = new LinkStateMachine();
        }
    }
}
