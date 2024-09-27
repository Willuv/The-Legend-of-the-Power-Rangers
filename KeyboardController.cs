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
        private readonly Dictionary<Keys, ICommand> keyCommandMappings;
        private readonly HashSet<Keys> actionKeysPressed;
        private readonly LinkIdleCommand idleCommand;

        public KeyboardController(LinkStateMachine stateMachine, LinkDecorator linkDecorator, Game1 game)
        {
            keyCommandMappings = new Dictionary<Keys, ICommand>
            {
                { Keys.W, new LinkUpCommand(stateMachine) },
                { Keys.S, new LinkDownCommand(stateMachine) },
                { Keys.A, new LinkLeftCommand(stateMachine) },
                { Keys.D, new LinkRightCommand(stateMachine) },
                { Keys.Z, new LinkSwordCommand(stateMachine) },
                { Keys.N, new LinkSwordCommand(stateMachine) },
                { Keys.D1, new LinkItem1Command(stateMachine) },
                { Keys.D2, new LinkItem2Command(stateMachine) },
                { Keys.D3, new LinkItem3Command(stateMachine) },
                { Keys.D4, new LinkItem4Command(stateMachine) },
                { Keys.D5, new LinkItem5Command(stateMachine) },
                { Keys.E, new LinkBecomeDamagedCommand(linkDecorator) },
                { Keys.T, new BlockPreviousCommand() },
                { Keys.Y, new BlockNextCommand() },
                { Keys.U, new ItemShowPreviousCommand(game) },
                { Keys.I, new ItemShowNextCommand(game) },
                { Keys.O, new NPCShowPreviousCommand() },
                { Keys.P, new NPCShowNextCommand() },
                { Keys.Q, new QuitCommand() },
                { Keys.R, new ResetCommand() }
            };
            actionKeysPressed = new HashSet<Keys>();
            idleCommand = new LinkIdleCommand(stateMachine);
        }

        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            bool movementKeyPressed = false;

            foreach (Keys key in pressedKeys)
            {
                if (keyCommandMappings.ContainsKey(key))
                {
                    ICommand command = keyCommandMappings[key];

                    if (actionKeysPressed.Contains(key))
                    {
                        continue;
                    }

                    if (key == Keys.W || key == Keys.A || key == Keys.S || key == Keys.D)
                    {
                        if (actionKeysPressed.Count == 0)
                        {
                            command.Execute();
                            movementKeyPressed = true;
                        }
                    }
                    else
                    {
                        command.Execute();
                        actionKeysPressed.Add(key);
                        movementKeyPressed = false;
                    }
                }
            }

            if (!movementKeyPressed)
            {
                idleCommand.Execute();
            }

            foreach (var key in actionKeysPressed.ToList())
            {
                if (!pressedKeys.Contains(key))
                {
                    actionKeysPressed.Remove(key);
                }
            }
        }

        public void RegisterCommand(Keys key, ICommand command)
        {
            keyCommandMappings[key] = command;
        }
    }
}
