using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class Enemy
{
    private EnemySprite sprite;
    private EnemySprite sprite2;
    private Vector2 position;
    private Vector2 position2;

    private float speed = 33f;
    private Vector2 direction;
    private Random random = new Random();
    private double directionChangeTimer;

    public Enemy(Vector2 initialPosition)
    {
        sprite = (EnemySprite)EnemySpriteFactory.Instance.CreateExampleEnemySprite();
        sprite2 = (EnemySprite)EnemySpriteFactory.Instance.CreateExampleEnemy2Sprite();
        position = initialPosition;
        position2 = initialPosition;
        SetRandomDirection();
    }

    private void SetRandomDirection()
    {
        Vector2[] directions = new[] { new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0) };
        direction = directions[random.Next(directions.Length)];
        sprite.SetDirection(direction);
        sprite2.SetDirection(direction);
    }

    public void Update(GameTime gameTime)
    {
        directionChangeTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (directionChangeTimer >= 2)
        {
            SetRandomDirection();
            directionChangeTimer = 0;
        }

        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        position2 += direction * speed * 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        sprite.Update(gameTime);
        sprite2.Update(gameTime);
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, position);
        sprite2.Draw(spriteBatch, position2 + new Vector2(60, 0));
    }
}

}
