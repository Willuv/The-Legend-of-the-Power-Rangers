using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Legend_of_the_Power_Rangers
{
    public class KeyboardController : IController<Keys>
    {
        private readonly Dictionary<Keys, ICommand> keyCommandMappings;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private readonly LinkIdleCommand idleCommand;

        public KeyboardController(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory, LinkDecorator linkDecorator, BlockManager blockManager, ItemManager itemManager ,Game1 game, GameStateMachine gameStateMachine)
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
                { Keys.T, new BlockPreviousCommand(blockManager) },
                { Keys.Y, new BlockNextCommand(blockManager) },
                { Keys.U, new ItemShowPreviousCommand(itemManager) },
                //{ Keys.I, new ItemShowNextCommand(itemManager) },
                { Keys.I, new SwitchInventoryState(gameStateMachine) },
                //{ Keys.O, new NPCShowPreviousCommand(game) },
                { Keys.P, new SwitchState(gameStateMachine) },
                { Keys.Q, new QuitCommand(game) },
                { Keys.R, new ResetCommand(gameStateMachine) }
            };
            idleCommand = new LinkIdleCommand(stateMachine);
        }

        public void Update()
        {
            currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();
            var currentPressedKeySet = new HashSet<Keys>(pressedKeys);

            foreach (Keys key in new[] { Keys.W, Keys.A, Keys.S, Keys.D })
            {
                if (currentPressedKeySet.Contains(key))
                {
                    if (keyCommandMappings.TryGetValue(key, out var command))
                    {
                        command.Execute();
                    }
                    break;
                }
            }

            foreach (Keys key in pressedKeys)
            {
                if (!previousKeyboardState.IsKeyDown(key) && keyCommandMappings.ContainsKey(key))
                {
                    ICommand command = keyCommandMappings[key];
                    command.Execute();
                }
            }

            if (pressedKeys.Length == 0)
            {
                idleCommand.Execute();
            }

            previousKeyboardState = currentKeyboardState;
        }


        public void RegisterCommand(Keys key, ICommand command)
        {
            keyCommandMappings[key] = command;
        }
    }
}
