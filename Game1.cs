using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Input;
using Legend_of_the_Power_Rangers.LevelCreation;
using ExcelDataReader;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;


namespace Legend_of_the_Power_Rangers
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Level level;
        private Link link;
        private LinkDecorator linkDecorator;
        private KeyboardController keyboardController;
        private MouseController mouseController;
        private LinkItemFactory linkItemFactory;
        private IItem item;
        private Texture2D itemTexture;
        private Texture2D enemySpritesheet;
        public Texture2D bossSpritesheet;
        private StreamReader reader;
        public Texture2D projectileSpriteSheet;

        private IBlock block = new BlockStatue1();

        private List<IEnemy> sprites = new List<IEnemy>();
        private int itemIndex;
        private int enemyIndex;

        //private List<ICollision> loadedObjects;
        //private CollisionManager collisionManager;
        private BlockManager blockManager;
        private ItemManager itemManager;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 880;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ResetGame()
        {
            reader.Close();
            base.Initialize();
            LoadContent();
        }

    

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            String path = Content.RootDirectory;

            reader = new StreamReader(path + "\\LinkDungeon1 - Room1.csv");

            Texture2D linkSpriteSheet = Content.Load<Texture2D>("Link Sprites");
            projectileSpriteSheet = Content.Load<Texture2D>("Projectiles");
            Texture2D blockSpriteSheet = Content.Load<Texture2D>("Blocks");
            Texture2D levelSpriteSheet = Content.Load<Texture2D>("Level");
            itemTexture = Content.Load<Texture2D>("Items");
            enemySpritesheet = Content.Load<Texture2D>("Enemies");
            bossSpritesheet = Content.Load<Texture2D>("Bosses");

            BlockSpriteFactory.Instance.SetBlockSpritesheet(blockSpriteSheet);
            ItemSpriteFactory.Instance.SetItemSpritesheet(itemTexture);
            EnemySpriteFactory.Instance.SetEnemySpritesheet(enemySpritesheet);
            EnemySpriteFactory.Instance.SetProjectileSpritesheet(projectileSpriteSheet);
            EnemySpriteFactory.Instance.SetBossSpritesheet(bossSpritesheet);

            LinkSpriteFactory.Instance.SetSpriteSheet(linkSpriteSheet);
            linkItemFactory = new LinkItemFactory(itemTexture, projectileSpriteSheet, blockSpriteSheet);

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

            

            level = new Level(levelSpriteSheet, reader, path);

            keyboardController = new KeyboardController(link.GetStateMachine(), linkItemFactory, linkDecorator, blockManager, itemManager, this);
            mouseController = new MouseController(link.GetStateMachine(), linkItemFactory, linkDecorator, level, this);


            itemIndex = 0;

            //loadedObjects = new();
            //loadedObjects.Add(link);

            //foreach (var obj in level.GetRoomObjects())
            //{
            //    loadedObjects.Add((ICollision)obj); // Assuming all objects implement ICollision

            //}
            //foreach (var obj in loadedObjects)
            //{
            //    Debug.WriteLine($"Loaded Object: {obj.GetType().Name}");
            //}
            //collisionManager = new CollisionManager();
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
            mouseController.Update();

            LinkManager.GetLink().Update(gameTime);

            linkItemFactory.Update(gameTime, link.DestinationRectangle, link.GetDirection());
            level.Update(gameTime);
            linkDecorator.Update(gameTime);
            //collisionManager.Update(gameTime, loadedObjects);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront);

            LinkManager.GetLink().Draw(spriteBatch);
            linkItemFactory.Draw(spriteBatch);
            linkDecorator.Draw(spriteBatch);

            base.Draw(gameTime);

            level.Draw(enemySpritesheet, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
