using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.InputCommands
{
    public class MuteUnmuteGameCommand : ICommand
    {

        public MuteUnmuteGameCommand()
        {
        }
        public void Execute() 
        {
            if (AudioManager.Instance.IsMuted())
            {
                AudioManager.Instance.Unmute();
            }
            else
            {
                AudioManager.Instance.Mute();
            }
        }

    }
}
