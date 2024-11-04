using Legend_of_the_Power_Rangers.InputCommands;
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
        private Keys activeMovementKey = Keys.None; // Track the active movement key

        public KeyboardController(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory, LinkDecorator linkDecorator, BlockManager blockManager, ItemManager itemManager, Game1 game, GameStateMachine gameStateMachine)
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
                { Keys.I, new SwitchInventoryState(gameStateMachine) },
                { Keys.P, new SwitchState(gameStateMachine) },
                { Keys.M, new MuteUnmuteGameCommand() },
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

            // Check if active movement key is released
            if (activeMovementKey != Keys.None && !currentPressedKeySet.Contains(activeMovementKey))
            {
                activeMovementKey = Keys.None; // Reset if the active movement key is released
            }

            // Set active movement key only if none is currently active
            foreach (Keys key in new[] { Keys.W, Keys.A, Keys.S, Keys.D })
            {
                if (currentPressedKeySet.Contains(key) && activeMovementKey == Keys.None)
                {
                    activeMovementKey = key;
                }
            }

            // Execute the command for the active movement key if set
            if (activeMovementKey != Keys.None && keyCommandMappings.TryGetValue(activeMovementKey, out var movementCommand))
            {
                movementCommand.Execute();
            }

            // Execute non-movement key commands if they're pressed (one-time press)
            foreach (Keys key in pressedKeys)
            {
                if (!previousKeyboardState.IsKeyDown(key) && keyCommandMappings.ContainsKey(key) && !IsMovementKey(key))
                {
                    ICommand command = keyCommandMappings[key];
                    command.Execute();
                }
            }

            // Execute idle command if no keys are pressed
            if (pressedKeys.Length == 0)
            {
                idleCommand.Execute();
            }

            previousKeyboardState = currentKeyboardState;
        }

        // Helper method to check if a key is a movement key
        private bool IsMovementKey(Keys key)
        {
            return key == Keys.W || key == Keys.A || key == Keys.S || key == Keys.D;
        }

        public void RegisterCommand(Keys key, ICommand command)
        {
            keyCommandMappings[key] = command;
        }
    }
}
