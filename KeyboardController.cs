using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class KeyboardController : IController<Keys>
    {
        private readonly Dictionary<Keys, ICommand> keyCommandMappings;
        private readonly LinkIdleCommand idleCommand;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        

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
                { Keys.Q, new QuitCommand(game) },
                { Keys.R, new ResetCommand(game) }
            };
            idleCommand = new LinkIdleCommand(stateMachine);
        }

        public void Update()
        {
            currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();
            var currentPressedKeySet = new HashSet<Keys>(pressedKeys);

            foreach (var key in new[] { Keys.W, Keys.A, Keys.S, Keys.D })
            {
                //if i remove the second part of the if statement I'm able to constantly call the movement commands by holding the key
                if (currentPressedKeySet.Contains(key) && !previousKeyboardState.IsKeyDown(key))
                {
                    Debug.WriteLine("Movement.");
                    keyCommandMappings[key].Execute();
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

            previousKeyboardState = currentKeyboardState;
        }

        public void RegisterCommand(Keys key, ICommand command)
        {
            keyCommandMappings[key] = command;
        }
    }
}
