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
        private readonly HashSet<Keys> pressedMovementKeys;
        private readonly HashSet<Keys> processedActionKeys;
        private readonly LinkIdleCommand idleCommand;
        private Keys lastDirectionKey;

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
                { Keys.O, new NPCShowPreviousCommand(game) },
                { Keys.P, new NPCShowNextCommand(game) },
                { Keys.Q, new QuitCommand() },
                { Keys.R, new ResetCommand() }
            };
            idleCommand = new LinkIdleCommand(stateMachine);
            lastDirectionKey = Keys.None;

            pressedMovementKeys = new HashSet<Keys>();
            processedActionKeys = new HashSet<Keys>();

        }

        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (keyCommandMappings.ContainsKey(key))
                {
                    if (key == Keys.W || key == Keys.A || key == Keys.S || key == Keys.D)
                    {
                        if (!pressedMovementKeys.Contains(key))
                        {
                            pressedMovementKeys.Add(key);
                            lastDirectionKey = key;
                            keyCommandMappings[key].Execute();
                        }
                    }
                    else
                    {
                        if (!processedActionKeys.Contains(key))
                        {
                            keyCommandMappings[key].Execute();
                            processedActionKeys.Add(key);
                        }
                    }
                }
            }

            foreach (Keys key in pressedMovementKeys.ToList())
            {
                if (!pressedKeys.Contains(key))
                {
                    pressedMovementKeys.Remove(key);
                    if (key == lastDirectionKey)
                    {
                        if (pressedMovementKeys.Count > 0)
                        {
                            lastDirectionKey = pressedMovementKeys.Last();
                            keyCommandMappings[lastDirectionKey].Execute();
                        }
                        else
                        {
                            idleCommand.Execute();
                            lastDirectionKey = Keys.None;
                        }
                    }
                }
            }

            foreach (Keys key in processedActionKeys.ToList())
            {
                if (!pressedKeys.Contains(key))
                {
                    processedActionKeys.Remove(key);
                }
            }

            if (pressedMovementKeys.Count == 0 && lastDirectionKey == Keys.None)
            {
                idleCommand.Execute();
            }
        }

        public void RegisterCommand(Keys key, ICommand command)
        {
            keyCommandMappings[key] = command;
        }
    }
}