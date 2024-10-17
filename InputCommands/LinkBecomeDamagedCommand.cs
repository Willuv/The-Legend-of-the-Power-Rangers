using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class LinkBecomeDamagedCommand : ICommand
    {
        private LinkDecorator linkDecorator;
        public LinkBecomeDamagedCommand(LinkDecorator linkDecorator) {
            this.linkDecorator = linkDecorator;
        }
        public void Execute()
        {
            linkDecorator.TakeDamage();
        }
    }
}
