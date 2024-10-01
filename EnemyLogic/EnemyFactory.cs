using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
public static class EnemyFactory
{
    public static Enemy CreateEnemy(Vector2 initialPosition, string enemyType)
    {
        switch (enemyType)
        {
            case "DragonBoss":
                return new DragonBoss(initialPosition);
            default:
                return new Enemy(initialPosition, enemyType);
        }
    }
}
}