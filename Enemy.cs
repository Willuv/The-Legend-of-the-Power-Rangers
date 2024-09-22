using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class Enemy
{
    private EnemySprite sprite;
    private Vector2 position;
    private float speed = 25f;
    private Vector2 direction;
    private Random random = new Random();
    private double directionChangeTimer;

    public Enemy(Vector2 initialPosition)
    {
        sprite = (EnemySprite)EnemySpriteFactory.Instance.CreateExampleEnemySprite();
        position = initialPosition;
        SetRandomDirection();
    }

    private void SetRandomDirection()
    {
        Vector2[] directions = new[] { new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0) };
        direction = directions[random.Next(directions.Length)];
        sprite.SetDirection(direction);
    }

    public void Update(GameTime gameTime)
    {
        directionChangeTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (directionChangeTimer >= 2) // Change direction every 2 seconds
        {
            SetRandomDirection();
            directionChangeTimer = 0;
        }

        // Move enemy in the current direction
        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, position);
    }
}

}
