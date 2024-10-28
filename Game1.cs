using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

            // Initialize the game state machine, which will handle specific content loading
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
