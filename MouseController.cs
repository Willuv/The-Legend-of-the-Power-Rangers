using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Legend_of_the_Power_Rangers
{
    public class MouseController : IController<MouseButton>
    {
        private readonly Dictionary<MouseButton, ICommand> mouseCommandMappings;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        public MouseController(LinkStateMachine stateMachine, LinkItemFactory linkItemFactory, LinkDecorator linkDecorator, Level level, Game1 game)
        {
            mouseCommandMappings = new Dictionary<MouseButton, ICommand>
            {
                { MouseButton.Left, new RoomShowPrevious(level) },
                { MouseButton.Right, new RoomShowNext(level) }, 
            };
        }

        public void Update()
        {
            currentMouseState = Mouse.GetState();

            // Check for mouse button presses
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (mouseCommandMappings.TryGetValue(MouseButton.Left, out var command))
                {
                    command.Execute();
                }
            }

            if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                if (mouseCommandMappings.TryGetValue(MouseButton.Right, out var command))
                {
                    command.Execute();
                }
            }

            // Update the previous mouse state
            previousMouseState = currentMouseState;
        }

        public void RegisterCommand(MouseButton button, ICommand command)
        {
            mouseCommandMappings[button] = command;
        }
    }

    public enum MouseButton
    {
        Left,
        Right,
    }
}

