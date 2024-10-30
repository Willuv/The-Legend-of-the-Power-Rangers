using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Legend_of_the_Power_Rangers
{
    public class GameStateMachine
    {
        public enum GameState
        {
            Gameplay,
            Paused,
            ItemSelection,
            GameOver,
            Winning,
            RoomTransition
        }

        private GameState currentState;
        private Game1 game;
        private SpriteBatch spriteBatch;
        public Level level;

        private Link link;
        private LinkDecorator linkDecorator;
        private KeyboardController keyboardController;
        private MouseController mouseController;

        public GameStateMachine(Game1 game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            InitializeGameplayState(); // Start the game in gameplay state
        }

        public void ChangeState(GameState newState)
        {
            currentState = newState;
            switch (newState)
            {
                case GameState.Gameplay:
                    InitializeGameplayState();
                    break;
                case GameState.Paused:
                    InitializePausedState();
                    break;
                case GameState.ItemSelection:
                    InitializeItemSelectionState();
                    break;
                case GameState.GameOver:
                    InitializeGameOverState();
                    break;
                case GameState.Winning:
                    InitializeWinningState();
                    break;
                case GameState.RoomTransition:
                    InitializeRoomTransitionState();
                    break;
            }
        }

        private void InitializeGameplayState()
        {
            // Load assets only if they haven't been loaded already
            if (game.itemSpriteSheet == null) game.itemSpriteSheet = game.Content.Load<Texture2D>("Items");
            if (game.enemySpritesheet == null) game.enemySpritesheet = game.Content.Load<Texture2D>("Enemies");
            Texture2D blockSpriteSheet = game.Content.Load<Texture2D>("Blocks");
            game.levelSpriteSheet = game.Content.Load<Texture2D>("Level");
            Texture2D linkSpriteSheet = game.Content.Load<Texture2D>("Link Sprites");
            Texture2D projectileSpriteSheet = game.Content.Load<Texture2D>("Projectiles");
            Texture2D bossSpriteSheet = game.Content.Load<Texture2D>("Bosses");

            // Set up factories
            BlockSpriteFactory.Instance.SetBlockSpritesheet(blockSpriteSheet);
            ItemSpriteFactory.Instance.SetItemSpritesheet(game.itemSpriteSheet);
            EnemySpriteFactory.Instance.SetEnemySpritesheet(game.enemySpritesheet);
            LinkSpriteFactory.Instance.SetLinkSpriteSheet(linkSpriteSheet);
            EnemySpriteFactory.Instance.SetBossSpritesheet(bossSpriteSheet);
            EnemySpriteFactory.Instance.SetProjectileSpritesheet(projectileSpriteSheet);
            game.linkItemFactory = new LinkItemFactory(game.itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);

            // Initialize managers if they are null
            game.blockManager ??= new BlockManager(new List<string> { "Statue1", "Statue2" });
            game.itemManager ??= new ItemManager(new List<string> { "Compass", "Map" });

            // Initialize Link and its decorator
            link = new Link();
            LinkManager.Initialize(link);
            linkDecorator = new LinkDecorator(link);
            LinkManager.SetLinkDecorator(linkDecorator);

            // Load the level
            string path = game.Content.RootDirectory;
            game.reader = new StreamReader(path + "\\LinkDungeon1 - Room1.csv");
            level = new Level(game.levelSpriteSheet, game.reader, path);

            // Set up controllers
            keyboardController = new KeyboardController(link.GetStateMachine(), game.linkItemFactory, linkDecorator, game.blockManager, game.itemManager, game, this);
            mouseController = new MouseController(link.GetStateMachine(), game.linkItemFactory, linkDecorator, level, game);
        }

        private void InitializePausedState()
        {
            // Pause logic (e.g., show pause menu, freeze game objects)
        }

        private void InitializeItemSelectionState()
        {
            // Item selection logic
        }

        private void InitializeGameOverState()
        {
            // Game Over logic (e.g., display game over screen, stop gameplay)
        }

        private void InitializeWinningState()
        {
            // Winning logic (e.g., display winning screen, handle end of game)
        }

        private void InitializeRoomTransitionState()
        {
            // Room transition logic (e.g., fade out/in between rooms)
        }

        public void ResetGame()
        {
            InitializeGameplayState();
            ResetLevel();
            ChangeState(GameState.Gameplay);
        }

        private void ResetLevel()
        {
            string initialRoomPath = game.Content.RootDirectory + "\\LinkDungeon1 - Room1.csv";
            game.reader = new StreamReader(initialRoomPath);
            level = new Level(game.levelSpriteSheet, game.reader, game.Content.RootDirectory);

            // Reset other elements like blocks and items
            game.blockManager = new BlockManager(new List<string> { "Statue1", "Statue2" });
            game.itemManager = new ItemManager(new List<string> { "Compass", "Map" });
        }

        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.Gameplay:
                    UpdateGameplay(gameTime);
                    break;
                case GameState.Paused:
                    // Handle paused state update
                    break;
                case GameState.ItemSelection:
                    // Handle item selection update
                    break;
                case GameState.GameOver:
                    // Handle game over update
                    break;
                case GameState.Winning:
                    // Handle winning update
                    break;
                case GameState.RoomTransition:
                    // Handle room transition update
                    break;
            }
        }

        private void UpdateGameplay(GameTime gameTime)
        {
            keyboardController.Update();
            mouseController.Update();
            link.Update(gameTime);
            linkDecorator.Update(gameTime);
            game.linkItemFactory.Update(gameTime, link.DestinationRectangle, link.GetDirection());
            level.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            switch (currentState)
            {
                case GameState.Gameplay:
                    DrawGameplay();
                    break;
                case GameState.Paused:
                    // Draw paused screen
                    break;
                case GameState.ItemSelection:
                    // Draw item selection screen
                    break;
                case GameState.GameOver:
                    // Draw game over screen
                    break;
                case GameState.Winning:
                    // Draw winning screen
                    break;
                case GameState.RoomTransition:
                    // Draw room transition animation
                    break;
            }
            spriteBatch.End();
        }

        private void DrawGameplay()
        {
            level.Draw(game.enemySpritesheet, spriteBatch);
            game.linkItemFactory.Draw(spriteBatch);
            linkDecorator.Draw(spriteBatch);
        }
    }
}
