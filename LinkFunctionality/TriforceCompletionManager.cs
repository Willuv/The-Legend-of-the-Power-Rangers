using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Legend_of_the_Power_Rangers;
using static Legend_of_the_Power_Rangers.GameStateMachine;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

public class TriforceCompletionManager
{
    private enum SequenceStage
    {
        InitialPause,
        FadeToBlack,
        Menu,
        Complete
    }

    private enum MenuOption
    {
        Continue,
        Quit,
        Reset
    }
    private SequenceStage currentStage = SequenceStage.InitialPause;
    private MenuOption currentMenuOption = MenuOption.Continue;
    private bool isTriforceSequenceActive = false;
    private bool hasCompletedSequence = false;

    private double elapsedTime = 0;
    private float fadeAlpha = 0f;
    private const double INITIAL_PAUSE = 750;
    private const double FADE_DURATION = 2500;
    
    private readonly Link link;
    private SpriteFont font;
    private Texture2D fadeTexture;

    private KeyboardState previousKeyboardState;
    private readonly GameStateMachine gameStateMachine;
    private ILinkSprite currentSprite;
    
    public TriforceCompletionManager(GraphicsDevice graphicsDevice, GameStateMachine gameStateMachine, Link link, SpriteFont font)
    {
        this.gameStateMachine = gameStateMachine;
        this.link = link;
        this.font = font;
        fadeTexture = new Texture2D(graphicsDevice, 1, 1);
        fadeTexture.SetData(new[] { Color.White });
    }
    
    public void Update(GameTime gameTime, GameStateMachine gameStateMachine)
    {
        if (LinkManager.GetLinkInventory().HasItem(ItemType.Triforce) && !isTriforceSequenceActive && !hasCompletedSequence )
        {
            StartTriforceSequence(gameStateMachine);
        }
        if (isTriforceSequenceActive)
        {
            UpdateTriforceSequence(gameTime);
        }
    }

    public void StartTriforceSequence(GameStateMachine gameStateMachine)
    {
        isTriforceSequenceActive = true;
        elapsedTime = 0;
        fadeAlpha = 0f;
        currentStage = SequenceStage.InitialPause;
        currentMenuOption = MenuOption.Continue;
        
        gameStateMachine.ChangeState(GameStateMachine.GameState.Winning);
        link.FaceForward();

        currentSprite = LinkSpriteFactory.Instance.CreateLinkSprite(LinkManager.GetLink().CurrentAction, LinkManager.GetLink().CurrentDirection);
        //linkWinSprite.Draw(spriteBatch, new Rectangle(100, 100, 28, 32), Color.White);
    }

    private void UpdateTriforceSequence(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

        switch (currentStage)
        {
            case SequenceStage.InitialPause:
                if (elapsedTime >= INITIAL_PAUSE)
                {
                    elapsedTime = 0;
                    currentStage = SequenceStage.FadeToBlack;
                }
                break;

            case SequenceStage.FadeToBlack:
                UpdateFadeEffect(gameTime);
                if (fadeAlpha >= 1f)
                {
                    currentStage = SequenceStage.Menu;
                }
                break;

            case SequenceStage.Menu:
                HandleMenuInput();
                break;

            case SequenceStage.Complete:
                CompleteSequence();
                break;
        }
    }

    private void UpdateFadeEffect(GameTime gameTime)
    {
        float fadeSpeed = 1f / (float)(FADE_DURATION);
        fadeAlpha = Math.Min(1f, fadeAlpha + (float)gameTime.ElapsedGameTime.TotalMilliseconds * fadeSpeed);
    }
    private void HandleMenuInput()
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
        {
            currentMenuOption = (MenuOption)(((int)currentMenuOption + 2) % 3); // Cycle backward
        }
        else if (keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
        {
            currentMenuOption = (MenuOption)(((int)currentMenuOption + 1) % 3); // Cycle forward
        }

        if (keyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
        {
            switch (currentMenuOption)
            {
                case MenuOption.Continue:
                    currentStage = SequenceStage.Complete;
                    break;
                case MenuOption.Quit:
                    gameStateMachine.ChangeState(GameStateMachine.GameState.Quit);
                    break;
                case MenuOption.Reset:
                    gameStateMachine.ChangeState(GameStateMachine.GameState.Reset);
                    break;
            }
        }

        previousKeyboardState = keyboardState;
    }

    private void CompleteSequence()
    {
        hasCompletedSequence = true;
        isTriforceSequenceActive = false;
        gameStateMachine.ChangeState(GameStateMachine.GameState.Running);
    }

    public void Draw(SpriteBatch spriteBatch, Rectangle screenBounds)
    {
        if (!isTriforceSequenceActive) return;
        

        // Draw fade to black
        if (currentStage == SequenceStage.FadeToBlack || currentStage == SequenceStage.Menu)
        {
            spriteBatch.Draw(fadeTexture, screenBounds, Color.Black * fadeAlpha);
        }

        // Draw menu
        if (currentStage == SequenceStage.Menu)
        {
            Vector2 position = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2);
            string[] menuItems = { "Continue", "Quit", "Reset" };

            for (int i = 0; i < menuItems.Length; i++)
            {
                Color color = (i == (int)currentMenuOption) ? Color.Yellow : Color.White;
                spriteBatch.DrawString(font, menuItems[i], position + new Vector2(0, i * 30), color, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
        }
    }
}