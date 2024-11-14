using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Legend_of_the_Power_Rangers;
using static Legend_of_the_Power_Rangers.GameStateMachine;
using System.Diagnostics;

public class TriforceCompletionManager
{
    private readonly Link link;
    private bool isTriforceSequenceActive = false;
    private bool hasCompletedSequence = false;
    private bool isFlashing = false;

    private double elapsedTime = 0;
    private double flashTimer = 0;
    private float fadeAlpha = 0f;
    
    private const double INITIAL_PAUSE = 750;
    private const double FLASH_INTERVAL = 200;
    private const double FLASH_DURATION = 2000;
    private const double FADE_DURATION = 2500;
    
    private Texture2D fadeTexture;
    private readonly GameStateMachine gameStateMachine;
    
    public TriforceCompletionManager(GraphicsDevice graphicsDevice, GameStateMachine gameStateMachine, Link link)
    {
        this.gameStateMachine = gameStateMachine;
        this.link = link;
        fadeTexture = new Texture2D(graphicsDevice, 1, 1);
        fadeTexture.SetData(new[] { Color.White });
    }

    public void Update(GameTime gameTime, GameStateMachine gameStateMachine)
    {
        if (LinkManager.GetLinkInventory().HasItem(ItemType.Triforce) && !isTriforceSequenceActive && !hasCompletedSequence)
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
        flashTimer = 0;
        isFlashing = false;
        fadeAlpha = 0f;
        gameStateMachine.ChangeState(GameStateMachine.GameState.Winning);
        link.FaceForward();
    }

    private void UpdateTriforceSequence(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
        // Add Link Pose
        // Initial pause
        if (elapsedTime < INITIAL_PAUSE)
        {
            return;
        }
        // Screen flashing
        else if (elapsedTime < INITIAL_PAUSE + FLASH_DURATION)
        {
            UpdateFlashingEffect(gameTime);
        }
        // Fade to black
        else if (elapsedTime < INITIAL_PAUSE + FLASH_DURATION + FADE_DURATION)
        {
            UpdateFadeEffect(gameTime);
        }
        // Finish
        else if (!hasCompletedSequence)
        {
            CompleteSequence();
        }
    }

    private void UpdateFlashingEffect(GameTime gameTime)
    {
        flashTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        if (flashTimer >= FLASH_INTERVAL)
        {
            isFlashing = !isFlashing;
            flashTimer = 0;
        }
    }

    private void UpdateFadeEffect(GameTime gameTime)
    {
        float fadeSpeed = 1f / (float)(FADE_DURATION);
        fadeAlpha = Math.Min(1f, fadeAlpha + (float)gameTime.ElapsedGameTime.TotalMilliseconds * fadeSpeed);
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
        // Draw white flash
        if (isFlashing)
        {
            spriteBatch.Draw(fadeTexture, screenBounds, Color.White);
        }

        // Draw fade to black
        if (fadeAlpha > 0)
        {
            spriteBatch.Draw(fadeTexture, screenBounds, Color.Black * fadeAlpha);
        }
    }
}