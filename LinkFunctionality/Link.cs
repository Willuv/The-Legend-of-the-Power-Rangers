﻿using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

public class Link : ICollision
{
    private LinkStateMachine stateMachine;
    private ILinkSprite currentSprite;

    private const float ScaleFactor = 3.36f;
    private const int LinkWidth = 48;
    private const int LinkHeight = 48;
    private Rectangle destinationRectangle;
    private const int MaxHealth = 6;
    private int currentHealth = MaxHealth;

    public Rectangle DestinationRectangle
    {
        get { return destinationRectangle; }
        set { destinationRectangle = value; }
    }
    public ObjectType ObjectType { get { return ObjectType.Link; } }

    public Link()
    {
        stateMachine = new LinkStateMachine();
        currentSprite = stateMachine.GetCurrentSprite();
        UpdateDestinationRectangle();
    }

    public void LoseHealth()
    {
        currentHealth = Math.Max(0, currentHealth - 1);
    }

    public void Heal(int amount)
    {
        currentHealth = Math.Min(MaxHealth, currentHealth + amount);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    private void UpdateDestinationRectangle()
    {
        Rectangle sourceRectangle = currentSprite.SourceRectangle;

        int newWidth = (int)(sourceRectangle.Width * ScaleFactor);
        int newHeight = (int)(sourceRectangle.Height * ScaleFactor);

        LinkStateMachine.LinkAction currentAction = stateMachine.GetCurrentAction();
        LinkStateMachine.LinkDirection currentDirection = stateMachine.GetCurrentDirection();

        int xOffset = 0;
        int yOffset = 0;

        switch (currentDirection)
        {
            case LinkStateMachine.LinkDirection.Up:
                yOffset = destinationRectangle.Height - newHeight;
                break;

            case LinkStateMachine.LinkDirection.Left:
                xOffset = destinationRectangle.Width - newWidth;
                break;

            case LinkStateMachine.LinkDirection.Right:
            case LinkStateMachine.LinkDirection.Down:
                break;
        }

        destinationRectangle = new Rectangle(
            destinationRectangle.X + xOffset,
            destinationRectangle.Y + yOffset,
            newWidth,
            newHeight
        );
    }


    public void UpdatePosition(Vector2 movement)
    {
        destinationRectangle.X += (int)movement.X;
        destinationRectangle.Y += (int)movement.Y;
    }

    public LinkStateMachine GetStateMachine()
    {
        return stateMachine;
    }

    public LinkStateMachine.LinkDirection GetDirection()
    {
        return stateMachine.GetCurrentDirection();
    }

    public virtual void Update(GameTime gameTime)
    {
        stateMachine.UpdateActionTimer(gameTime);

        Vector2 movement = stateMachine.UpdateMovement();
        bool isMoving = (movement != Vector2.Zero);

        if (isMoving)
        {
            UpdatePosition(movement);
        }
        else if (!stateMachine.IsActionLocked())
        {
            stateMachine.ChangeAction(LinkStateMachine.LinkAction.Idle);
        }

        currentSprite = stateMachine.GetCurrentSprite();
        currentSprite.Update(gameTime);

        UpdateDestinationRectangle();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        currentSprite = stateMachine.GetCurrentSprite();

        currentSprite.Draw(spriteBatch, destinationRectangle, Color.White);
    }
}