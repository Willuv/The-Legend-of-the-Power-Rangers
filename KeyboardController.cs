﻿using Microsoft.Xna.Framework.Graphics;
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

        public KeyboardController(LinkStateMachine stateMachine)
        {
            keyCommandMappings = new Dictionary<Keys, ICommand>
            {
                { Keys.W, new LinkUpCommand(stateMachine) },
                { Keys.S, new LinkDownCommand(stateMachine) },
                { Keys.A, new LinkLeftCommand(stateMachine) },
                { Keys.D, new LinkRightCommand(stateMachine) },
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