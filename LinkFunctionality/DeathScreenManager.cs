using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Legend_of_the_Power_Rangers
{
    public class DeathScreenManager
    {
        private enum DeathStage
        {
            TurningForward,
            RedScreen,
            Spinning,
            BlackScreen,
            Menu
        }
        private enum MenuOption
        {
            Continue,
            Quit,
            Reset
        }

        private DeathStage currentStage = DeathStage.TurningForward;
        private MenuOption currentMenuOption = MenuOption.Continue;

        private double elapsedTime = 0;
        private int spinCount = 0;
        private float fadeAlpha = 0f;
        private Texture2D screenOverlayTexture;

        private SpriteFont font;
        private readonly Link link;
        private readonly GameStateMachine gameStateMachine;
        private readonly LinkStateMachine linkStateMachine;
        private LinkDecorator linkDecorator;
        private SpriteBatch spriteBatch;

        private const double TURN_FORWARD_DURATION = 500;
        private const double RED_SCREEN_DURATION = 1000;
        private const double SPIN_DURATION = 1000;
        private const float SPIN_SPEED = 200;
        private const double BLACK_SCREEN_DURATION = 3000;

        private KeyboardState previousKeyboardState;

        public DeathScreenManager(GraphicsDevice graphicsDevice, Link link, GameStateMachine gameStateMachine, SpriteBatch spriteBatch, SpriteFont font)
        {
            //this.link = link;
            this.link = LinkManager.GetLink();
            this.gameStateMachine = gameStateMachine;
            this.linkStateMachine = link.GetStateMachine();
            this.spriteBatch = spriteBatch;
            this.font = font;

            linkDecorator = new LinkDecorator(link);
            screenOverlayTexture = new Texture2D(graphicsDevice, 1, 1);
            screenOverlayTexture.SetData(new[] { Color.White });
        }

        public void StartDeathSequence()
        {
            currentStage = DeathStage.TurningForward;
            elapsedTime = 0;
            spinCount = 0;
            fadeAlpha = 0f;
            gameStateMachine.ChangeState(GameStateMachine.GameState.GameOver); // Switch to death state
        }

        public void deathUpdateCheck(GameTime gameTime) {
            int hp = LinkManager.GetLink().GetCurrentHealth();
            if (hp < 1) {
                StartDeathSequence();
            }
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            switch (currentStage)
            {
                case DeathStage.TurningForward:
                    if (elapsedTime >= TURN_FORWARD_DURATION)
                    {
                        link.FaceForward();
                        elapsedTime = 0;
                        currentStage = DeathStage.RedScreen;
                    }
                    break;

                case DeathStage.RedScreen:
                    if (elapsedTime >= RED_SCREEN_DURATION)
                    {
                        elapsedTime = 0;
                        currentStage = DeathStage.Spinning;
                    }
                    break;

                case DeathStage.Spinning:
                    PerformSpin(gameTime);
                    if (spinCount >= 12) // Three full x four dirrections
                    {
                        elapsedTime = 0;
                        currentStage = DeathStage.BlackScreen;
                    }
                    break;

                case DeathStage.BlackScreen:
                    fadeAlpha = Math.Min(1f, fadeAlpha + (float)gameTime.ElapsedGameTime.TotalSeconds);
                    if (elapsedTime >= BLACK_SCREEN_DURATION)
                    {
                        currentStage = DeathStage.Menu;
                    }
                    break;
                case DeathStage.Menu:
                    HandleMenuInput();
                    break;

            }
        }

        private void HandleMenuInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
            {
                currentMenuOption = (MenuOption)(((int)currentMenuOption - 1 + 3) % 3); // Wrap around
            }
            else if (keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
            {
                currentMenuOption = (MenuOption)(((int)currentMenuOption + 1) % 3); // Wrap around
            }
            else if (keyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
            {
                ExecuteMenuOption();
            }

            // Update previous keyboard state for next frame
            previousKeyboardState = keyboardState;
        }

        private void ExecuteMenuOption()
        {
            switch (currentMenuOption)
            {
                case MenuOption.Continue:
                    // Resume w/ full health
                    LinkManager.GetLink().Heal(LinkManager.GetLink().GetMaxHealth());
                    gameStateMachine.ChangeState(GameStateMachine.GameState.Running);
                    break;
                case MenuOption.Quit:
                    gameStateMachine.ChangeState(GameStateMachine.GameState.Quit);
                    break;
                case MenuOption.Reset:
                    gameStateMachine.ChangeState(GameStateMachine.GameState.Reset);
                    break;
            }
        }

        private void PerformSpin(GameTime gameTime)
        {
            // Change direction every SPIN_SPEED milliseconds
            if (elapsedTime >= SPIN_SPEED)
            {
                linkStateMachine.ChangeDirection((LinkStateMachine.LinkDirection)(((int)linkStateMachine.GetCurrentDirection() + 1) % 4));
                elapsedTime = 0;
                spinCount++;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle screenBounds)
        {
            if (currentStage == DeathStage.RedScreen)
            {
                spriteBatch.Draw(screenOverlayTexture, screenBounds, Color.Red * 0.6f);
            }
            else if (currentStage == DeathStage.BlackScreen || currentStage == DeathStage.Menu) 
            {
                spriteBatch.Draw(screenOverlayTexture, screenBounds, Color.Black * fadeAlpha);
                if (currentStage == DeathStage.Menu)
                {
                    DrawMenu(spriteBatch, screenBounds);
                }
            }
        }
        private void DrawMenu(SpriteBatch spriteBatch, Rectangle screenBounds)
        {
            string[] menuOptions = { "Continue", "Quit", "Reset" };
            Vector2 center = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2);
            float scale = 2.0f;

            for (int i = 0; i < menuOptions.Length; i++)
            {
                string text = menuOptions[i];
                Vector2 textSize = font.MeasureString(text);
                Vector2 position = center + new Vector2(0, (i - 1) * 50);

                Color color = (i == (int)currentMenuOption) ? Color.Yellow : Color.White;
                spriteBatch.DrawString(font, text, position - textSize / 2, color,0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        public void Dispose()
        {
            screenOverlayTexture?.Dispose();
        }
    }
}