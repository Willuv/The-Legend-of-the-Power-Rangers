using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public interface ILinkSprite: ISprite
    {
        void Update(GameTime gameTime, LinkStateMachine.LinkState currentState);
    }
}
