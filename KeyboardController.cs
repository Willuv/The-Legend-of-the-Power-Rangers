using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Legend_of_the_Power_Rangers
{
    public class KeyboardController : IController<Keys>
    {
        private Dictionary<Keys, ICommand> keyCommandMappings;

        public KeyboardController(Game1 game)
        {
            keyCommandMappings = new Dictionary<Keys, ICommand>
            {
                { Keys.W, new LinkUpCommand() },
                { Keys.S, new LinkDownCommand() },
                { Keys.A, new LinkLeftCommand() },
                { Keys.D, new LinkRightCommand() },
                { Keys.Z, new LinkSwordCommand() },
                { Keys.N, new LinkSwordCommand() },
                { Keys.D1, new LinkItem1Command() },
                { Keys.D2, new LinkItem2Command() },
                { Keys.D3, new LinkItem3Command() },
                { Keys.D4, new LinkItem4Command() },
                { Keys.D5, new LinkItem5Command() },
                { Keys.E, new LinkBecomeDamagedCommand() },
                { Keys.T, new BlockPreviousCommand() },
                { Keys.Y, new BlockNextCommand() },
                { Keys.U, new ItemShowPreviousCommand() },
                { Keys.I, new ItemShowNextCommand() },
                { Keys.O, new NPCShowPreviousCommand() },
                { Keys.P, new NPCShowNextCommand() },
                { Keys.Q, new QuitCommand() },
                { Keys.R, new ResetCommand() }
            };
        }
        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in pressedKeys)
            {
                if (keyCommandMappings.ContainsKey(key))
                {
                    ICommand command = keyCommandMappings[key];
                    command.Execute();
                }
            }
        }
        public void RegisterCommand(Keys key, ICommand command)
        {
            keyCommandMappings[key] = command;
        }
    }
}