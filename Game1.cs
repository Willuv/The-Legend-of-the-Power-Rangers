using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Legend_of_the_Power_Rangers
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Texture2D itemSpriteSheet;
        public Texture2D enemySpritesheet;
        public Texture2D levelSpriteSheet;
        public BlockManager blockManager;
        public ItemManager itemManager;
        public LinkItemFactory linkItemFactory;
        public Level level;
        public StreamReader reader;
        private GameStateMachine gameStateMachine;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1275;
            graphics.PreferredBackBufferHeight = 875;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load assets
            itemSpriteSheet = Content.Load<Texture2D>("Items");
            enemySpritesheet = Content.Load<Texture2D>("Enemies");
            Texture2D blockSpriteSheet = Content.Load<Texture2D>("Blocks");
            levelSpriteSheet = Content.Load<Texture2D>("Level");
            Texture2D linkSpriteSheet = Content.Load<Texture2D>("Link Sprites");
            Texture2D projectileSpriteSheet = Content.Load<Texture2D>("Projectiles");
            Texture2D bossSpriteSheet = Content.Load<Texture2D>("Bosses");

            // Set up factories
            BlockSpriteFactory.Instance.SetBlockSpritesheet(blockSpriteSheet);
            ItemSpriteFactory.Instance.SetItemSpritesheet(itemSpriteSheet);
            EnemySpriteFactory.Instance.SetEnemySpritesheet(enemySpritesheet);
            LinkSpriteFactory.Instance.SetLinkSpriteSheet(linkSpriteSheet);
            EnemySpriteFactory.Instance.SetBossSpritesheet(bossSpriteSheet);
            linkItemFactory = new LinkItemFactory(itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);

            // Initialize Managers
            blockManager = new BlockManager(new List<string> { "Statue1", "Statue2", /* ... */ });
            itemManager = new ItemManager(new List<string> { "Compass", "Map", /* ... */ });

            // Initialize state machine
            gameStateMachine = new GameStateMachine(this, spriteBatch);
        }


        protected override void Update(GameTime gameTime)
        {
            gameStateMachine.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            gameStateMachine.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
