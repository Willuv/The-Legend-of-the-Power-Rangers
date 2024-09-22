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
        return new EnemySprite(enemySpritesheet, 8, 15, 15);
        //return new EnemySprite(enemySpritesheet, 15, 15 * 3);
    }
}

}
