using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Legend_of_the_Power_Rangers.Collision;

namespace Legend_of_the_Power_Rangers
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Link link;
        private LinkDecorator linkDecorator;
        private KeyboardController keyboardController;
        private LinkItemFactory linkItemFactory;
        private IItem item;
        private Texture2D itemTexture;
        private Texture2D enemySpritesheet;
        public Texture2D bossSpritesheet;

        private List<IEnemy> sprites = new List<IEnemy>();
        private int itemIndex;
        private int enemyIndex;

        private List<ICollision> loadedObjects;
        private CollisionManager collisionManager;
        private BlockManager blockManager;
        private ItemManager itemManager;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ResetGame()
        {
            base.Initialize();
            LoadContent();
        }

        private void InitializeEnemies()
        {
            sprites.Add(new RedOcto());
            sprites.Add(new BlueOcto());
            sprites.Add(new BlueCentaur());
            sprites.Add(new BlueGorya());
            sprites.Add(new BlueKnight());
            sprites.Add(new DarkMoblin());
            sprites.Add(new DragonBoss(bossSpritesheet, enemySpritesheet));
            sprites.Add(new RedCentaur());
            sprites.Add(new RedGorya());
            sprites.Add(new RedKnight());
            sprites.Add(new RedMoblin());
            sprites.Add(new BatKeese());
            sprites.Add(new Skeleton());
            sprites.Add(new GelSmallBlack());
            sprites.Add(new GelSmallTeal());
            sprites.Add(new GelBigGreen());
            sprites.Add(new GelBigGray());
            sprites.Add(new WallMaster());
        }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeEnemies();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D linkSpriteSheet = Content.Load<Texture2D>("Link Sprites");
            Texture2D projectileSpriteSheet = Content.Load<Texture2D>("Projectiles");
            itemTexture = Content.Load<Texture2D>("Items");
            enemySpritesheet = Content.Load<Texture2D>("Enemies");
            bossSpritesheet = Content.Load<Texture2D>("Bosses");
            Texture2D blockTexture = Content.Load<Texture2D>("Blocks");

            BlockSpriteFactory.Instance.SetBlockSpritesheet(blockTexture);
            ItemSpriteFactory.Instance.SetItemSpritesheet(itemTexture);


            LinkSpriteFactory.Instance.SetSpriteSheet(linkSpriteSheet);
            linkItemFactory = new LinkItemFactory(itemTexture, projectileSpriteSheet, blockTexture);

            link = new Link();
            LinkManager.SetLink(link);

            linkDecorator = new LinkDecorator(link);
            LinkManager.SetLink(linkDecorator);

            var blockTypes = new List<string>
            {
                "Statue1", "Statue2", "Square", "Push", "Fire",
                "BlueGap", "Stairs", "WhiteBrick", "Ladder",
                "BlueFloor", "BlueSand", "BombedWall", "Diamond",
                "KeyHole", "OpenDoor", "Wall"
            };
            blockManager = new BlockManager(blockTypes);

            var itemTypes = new List<string>
            {
                "Compass", "Map", "Key", "HeartContainer", "Triforce", "WoodBoomerang",
                "Bow", "Heart", "Rupee", "Bomb", "Fairy", "Clock", "BlueCandle", "BluePotion"
            };
            itemManager = new ItemManager(itemTypes);


            keyboardController = new KeyboardController(link.GetStateMachine(), linkItemFactory, linkDecorator, blockManager, itemManager, this);

            itemIndex = 0;

            loadedObjects = new();
            InitializeEnemies();
            loadedObjects.Add(link);
            loadedObjects.Add(sprites[0]);
            loadedObjects.Add(sprites[6]); 

            //Very ugly way to do this for now
            foreach (var block in blockManager.GetBlocks())
            {
                if (block is BlockBlueFloor)
                {
                    loadedObjects.Add(block);
                }
            }

            collisionManager = new CollisionManager();
        }

        public void ChangeEnemy(int direction)
        {
            enemyIndex += direction;
            if (enemyIndex >= sprites.Count)
            {
                enemyIndex = 0;
            }
            else if (enemyIndex < 0)
            {
                enemyIndex = sprites.Count - 1;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();
            LinkManager.GetLink().Update(gameTime);

            linkItemFactory.Update(gameTime, link.DestinationRectangle, link.GetDirection());
            if (enemyIndex < sprites.Count)
            {
                sprites[enemyIndex].Update(gameTime);
            }
            linkDecorator.Update(gameTime);
            blockManager.Update(gameTime);
            itemManager.Update(gameTime);
            collisionManager.Update(gameTime, loadedObjects);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            LinkManager.GetLink().Draw(spriteBatch);
            linkItemFactory.Draw(spriteBatch);
            linkDecorator.Draw(spriteBatch);

            if (enemyIndex < sprites.Count)
            {
                sprites[enemyIndex].Draw(enemySpritesheet, spriteBatch);
            }

            itemManager.Draw(spriteBatch);
            blockManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
