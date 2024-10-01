using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class DragonBoss : Enemy
    {
        private List<Tuple<EnemySprite, Vector2>> projectiles;
        private double projectileFireTimer = 0;
        private double projectileFireInterval = 1;

        public DragonBoss(Vector2 initialPosition) : base(initialPosition, "DragonBoss")
        {
            projectiles = new List<Tuple<EnemySprite, Vector2>>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateProjectiles(gameTime);
        }

        private void UpdateProjectiles(GameTime gameTime)
        {
            projectileFireTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (projectileFireTimer >= projectileFireInterval)
            {
                ShootProjectiles();
                projectileFireTimer = 0;//Reset timer
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                var projectile = projectiles[i];
                Vector2 projectileDirection = GetProjectileDirection(i);
                Vector2 newPosition = projectile.Item2 + projectileDirection * 200 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                projectiles[i] = new Tuple<EnemySprite, Vector2>(projectile.Item1, newPosition);
            }
        }

        private void ShootProjectiles()
        {
            for (int i = 0; i < 3; i++)
            {
                var projectile = EnemySpriteFactory.Instance.CreateEnemySprite("Projectile");
                projectiles.Add(new Tuple<EnemySprite, Vector2>(projectile, position));
            }
        }

        private Vector2 GetProjectileDirection(int index)
        {
            switch (index % 3)
            {
                case 0: return new Vector2(-1, -1);//Southwest
                case 1: return new Vector2(-1, 0);//West
                case 2: return new Vector2(-1, 1);//Northwest
                default: return new Vector2(0, 0);//Should not reach here
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);//Draw boss
            foreach (var projectile in projectiles)//Draw projectiles
            {
                projectile.Item1.Draw(spriteBatch, projectile.Item2);
            }
        }
    }
}
