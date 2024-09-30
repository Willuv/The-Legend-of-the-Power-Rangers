using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Legend_of_the_Power_Rangers
{
public class EnemySpriteFactory
{
    private Texture2D enemySpritesheet;
    private Texture2D bossSpritesheet;
    private static EnemySpriteFactory instance;

    public static EnemySpriteFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemySpriteFactory();
            }
            return instance;
        }
    }

    public void LoadAllTextures(ContentManager content)
    {
        enemySpritesheet = content.Load<Texture2D>("Enemies");
        bossSpritesheet = content.Load<Texture2D>("Bosses");
    }

    public EnemySprite CreateEnemySprite(string type)
    {
        switch (type)
        {
            case "RedKnight":
                return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 180);
            case "BlueOcto":
                return new EnemySprite(enemySpritesheet, 8, 15, 15, 120, 0);
            case "RedOcto":
                return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 0);
            case "RedGorya":
                return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 60);
            case "Centaur":
                return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 240);
            case "Moblin":
                return new EnemySprite(enemySpritesheet, 8, 15, 15, 120, 120);
            case "Projectile":
                return new EnemySprite(enemySpritesheet, 8, 15, 15, 330, 0);
            case "DragonBoss":
                return new EnemySprite(bossSpritesheet, 4, 40, 40, 0, 0);
            default:
                throw new ArgumentException("Unknown enemy type", nameof(type));
        }
        // Most sprites are 120x60 for x/y ofset
    }
    }
}