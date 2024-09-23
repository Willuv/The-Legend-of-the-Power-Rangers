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
    }

    public ISprite CreateExampleEnemySprite()
    {
        // Assuming the enemy uses 8 frames starting from row 0
        return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 180);
        //return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 0); is red octo
        //return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 60); is red gorya
        //return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 180); is red knight
        //return new EnemySprite(enemySpritesheet, 8, 15, 15, 0, 240); is centaur
        //return new EnemySprite(enemySpritesheet, 8, 15, 15, 120, 0); is blue octo
        //return new EnemySprite(enemySpritesheet, 8, 15, 15, 330, 0); are projectiles
        // Most sprites are 120x60 for x/yofset
        
    }
    public ISprite CreateExampleEnemy2Sprite()
    {
        // Assuming the enemy uses 8 frames starting from row 0
        return new EnemySprite(enemySpritesheet, 8, 15, 15, 120, 120);
        //return new EnemySprite(enemySpritesheet, 8, 15, 15, 330, 0); is moblin
        
    }
}

}
