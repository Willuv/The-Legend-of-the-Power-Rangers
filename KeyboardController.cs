using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Legend_of_the_Power_Rangers
{
    public class KeyboardController : IController<Keys>
    {
        private readonly Dictionary<Keys, ICommand> keyCommandMappings;
        private readonly LinkIdleCommand idleCommand;

        public KeyboardController(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory, LinkDecorator linkDecorator, Game1 game)
        {
            keyCommandMappings = new Dictionary<Keys, ICommand>
            {
                { Keys.W, new LinkUpCommand(stateMachine) },
                { Keys.S, new LinkDownCommand(stateMachine) },
                { Keys.A, new LinkLeftCommand(stateMachine) },
                { Keys.D, new LinkRightCommand(stateMachine) },
                { Keys.Z, new LinkSwordCommand(stateMachine) },
                { Keys.N, new LinkSwordCommand(stateMachine) },
                { Keys.D1, new LinkItem1Command(stateMachine, linkItemFactory) },
                { Keys.D2, new LinkItem2Command(stateMachine, linkItemFactory) },
                { Keys.D3, new LinkItem3Command(stateMachine, linkItemFactory) },
                { Keys.D4, new LinkItem4Command(stateMachine, linkItemFactory) },
                { Keys.D5, new LinkItem5Command(stateMachine, linkItemFactory) },
                { Keys.E, new LinkBecomeDamagedCommand(linkDecorator) },
                { Keys.T, new BlockPreviousCommand(game) },
                { Keys.Y, new BlockNextCommand(game) },
                { Keys.U, new ItemShowPreviousCommand(game) },
                { Keys.I, new ItemShowNextCommand(game) },
                { Keys.O, new NPCShowPreviousCommand() },
                { Keys.P, new NPCShowNextCommand() },
                { Keys.Q, new QuitCommand() },
                { Keys.R, new ResetCommand() }
            };
            idleCommand = new LinkIdleCommand(stateMachine);
        }
        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();


            if (pressedKeys.Length == 0)
            {
                idleCommand.Execute();
                return;
            }

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