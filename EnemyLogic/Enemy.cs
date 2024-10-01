using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class Enemy
{
    private EnemySprite sprite;
    public Vector2 position;
    private float speed = 33f;
    public Vector2 direction;
    private Random random = new Random();
    private double directionChangeTimer;
    public String enemyType;

        public Enemy(Vector2 initialPosition, String enemyType)
        {
            this.position = initialPosition;
            this.enemyType = enemyType;
            InitializeEnemy();
        }
        protected void InitializeEnemy()
        {
            LoadNewSprite(enemyType);
            if (enemyType == "DragonBoss") {
                SetDragonDirection();
            } else {
                SetRandomDirection();
            }
        }

        private void LoadNewSprite(string type)
        {
            sprite = EnemySpriteFactory.Instance.CreateEnemySprite(type);
        }
        private void SetRandomDirection()
    {
        Vector2[] directions = new[] { new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0) };
        direction = directions[random.Next(directions.Length)];
        sprite.SetDirection(direction);
    }
        private void SetDragonDirection()
    {
        Vector2[] directions = new[] { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0)};
        direction = directions[random.Next(directions.Length)];
        sprite.SetDragonDirection(direction);
    }

    public virtual void Update(GameTime gameTime)
    {
        directionChangeTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (directionChangeTimer >= 2)
        {
            if (enemyType == "DragonBoss") {
                SetDragonDirection();
            } else {
                SetRandomDirection();
            }
            
            directionChangeTimer = 0;
        }
            if (enemyType == "DragonBoss") {
                    position += direction * 2 * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            } else {
                    position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
        sprite.Update(gameTime);        
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, position);
    }
    public void ChangeType(string newType)
        {
            enemyType = newType;
            InitializeEnemy();
        }
}

}
