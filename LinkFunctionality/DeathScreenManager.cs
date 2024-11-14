using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            Complete
        }

        private DeathStage currentStage = DeathStage.TurningForward;
        private double elapsedTime = 0;
        private int spinCount = 0;
        private float fadeAlpha = 0f;
        private Texture2D screenOverlayTexture;

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

        public DeathScreenManager(GraphicsDevice graphicsDevice, Link link, GameStateMachine gameStateMachine, SpriteBatch spriteBatch)
        {
            //this.link = link;
            this.link = LinkManager.GetLink();
            this.gameStateMachine = gameStateMachine;
            this.linkStateMachine = link.GetStateMachine();
            this.spriteBatch = spriteBatch;

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
                        currentStage = DeathStage.Complete;
                    }
                    break;

                case DeathStage.Complete:
                    // Reset game or other actions as needed
                    LinkManager.GetLink().Heal(LinkManager.GetLink().GetMaxHealth());
                    gameStateMachine.ChangeState(GameStateMachine.GameState.Running);
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
            else if (currentStage == DeathStage.BlackScreen || currentStage == DeathStage.Complete)
            {
                spriteBatch.Draw(screenOverlayTexture, screenBounds, Color.Black * fadeAlpha);
            }
        }

        public void Dispose()
        {
            screenOverlayTexture?.Dispose();
        }
    }
}