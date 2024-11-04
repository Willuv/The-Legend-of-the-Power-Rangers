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
        private static LinkDecorator currentDecorator;
        private static LinkInventory currentInventory;


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

        public static void SetLinkDecorator(LinkDecorator newDecorator)
        {
            currentDecorator = newDecorator;
        }

        public static LinkDecorator GetLinkDecorator()
        {
            return currentDecorator;
        }

        public static void setLinkInventory(LinkInventory linkInventory)
        {
            currentInventory = linkInventory;
        }

        public static LinkInventory GetLinkInventory()
        {
            return currentInventory;
        }

    }
}
