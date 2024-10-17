using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public static class LinkManager
    {
        private static Link currentLink;

        public static void Initialize(Link startingLink)
        {
            currentLink = startingLink;
        }

        public static Link GetLink()
        {
            return currentLink;
        }

        public static void SetLink(Link newLink)
        {
            currentLink = newLink;
        }
    }
}
