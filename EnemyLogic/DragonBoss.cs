using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class DragonBoss : Enemy
{
    private List<Tuple<EnemySprite, Vector2>> projectiles;
    private EnemySprite sprite;
    private double projectileFireTimer = 0;
    private double projectileFireInterval = 1;

    public DragonBoss(Vector2 initialPosition) : base(initialPosition)
    {
        sprite = EnemySpriteFactory.Instance.CreateEnemySprite("DragonBoss");
        projectiles = new List<Tuple<EnemySprite, Vector2>>();
    }

    public override void Update(GameTime gameTime)
{
    base.Update(gameTime);
    sprite.Update(gameTime);
    ShootProjectiles(gameTime);

    for (int i = 0; i < projectiles.Count; i++)
    {
        var projectile = projectiles[i];
        Vector2 direction;
        if (i % 3 == 0)//Southwest
        {
            direction = new Vector2(-1, -1);
        }
        else if (i % 3 == 1)//West
        {
            direction = new Vector2(-1, 0);
        }
        else//Northwest
        {
            direction = new Vector2(-1, 1);
        }
        //Update position
        Vector2 newPosition = projectile.Item2 + direction * 200 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        projectiles[i] = new Tuple<EnemySprite, Vector2>(projectile.Item1, newPosition);
    }
}

    private void ShootProjectiles(GameTime gameTime)
    {
        projectileFireTimer += gameTime.ElapsedGameTime.TotalSeconds;

        if (projectileFireTimer >= projectileFireInterval)
        {
            //3 projectiles
            var projectile1 = EnemySpriteFactory.Instance.CreateEnemySprite("Projectile");
            projectiles.Add(new Tuple<EnemySprite, Vector2>(projectile1, position));

            var projectile2 = EnemySpriteFactory.Instance.CreateEnemySprite("Projectile");
            projectiles.Add(new Tuple<EnemySprite, Vector2>(projectile2, position));

            var projectile3 = EnemySpriteFactory.Instance.CreateEnemySprite("Projectile");
            projectiles.Add(new Tuple<EnemySprite, Vector2>(projectile3, position));

            projectileFireTimer = 0;//Reset timer
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, position);//Draw boss
        foreach (var projectile in projectiles)//Draw projectile
        {
            projectile.Item1.Draw(spriteBatch, projectile.Item2);
        }
    }
}
}