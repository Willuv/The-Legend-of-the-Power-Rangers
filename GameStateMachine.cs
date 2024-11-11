using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            RoomTransition,
            Running,
        }

        public GameState currentState;
        private Camera2D camera;
        private Game1 game;
        private SpriteBatch spriteBatch;
        public Level level;
        private HUD hud;
        private InventoryScreen inventoryScreen;
        private Link link;
        private LinkDecorator linkDecorator;
        private LinkInventory linkInventory;
        private KeyboardController keyboardController;
        private MouseController mouseController;
        private AudioManager audioManager;
        private GreenDot greenDot;

        public GameStateMachine(Game1 game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            camera = Camera2D.Instance;

            audioManager = AudioManager.Instance;
            audioManager.Initialize(game.Content);

            InitializeGameplayState(); // Start the game in gameplay state will change to start state later on
        }

        public void ChangeState(GameState newState)
        {
            if (newState == currentState)
                return;

            currentState = newState;
            switch (newState)
            {
                case GameState.Gameplay:
                    if (currentState != GameState.Running)
                    {
                        InitializeGameplayState();
                    } 
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
                case GameState.Running:
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
            linkInventory = new LinkInventory();
            LinkManager.setLinkInventory(linkInventory);

            if (hud == null)
            {
                Texture2D hudTexture = game.Content.Load<Texture2D>("HUD");
                Rectangle hudDestinationRectangle = new Rectangle(0, 0, 1020, 192);
                hud = new HUD(game.GraphicsDevice, hudTexture, hudDestinationRectangle);
            }

            // Load the level
            level = new Level(game.levelSpriteSheet, game.Content.RootDirectory);
            // Set the Camera to current level
            camera.CalculateTransformMatrix(level.CurrentRoomRow, level.CurrentRoomColumn);
            // Set up controllers
            keyboardController = new KeyboardController(link.GetStateMachine(), game.linkItemFactory, linkDecorator, game.blockManager, game.itemManager, game, this);
            mouseController = new MouseController(link.GetStateMachine(), game.linkItemFactory, linkDecorator, level, game);

            LinkManager.GetLink().UpdatePosition(new Vector2(510, 700));
        }

        private void InitializePausedState()
        {
            
        }

        private void InitializeItemSelectionState()
        {
            int currentRoom = level.currentRoom;
            // Item selection logic
            if (inventoryScreen == null)
            {
                Texture2D InventoryTexture = game.Content.Load<Texture2D>("HUD");
                Rectangle InventoryDestinationRectangle = new Rectangle(0, 0, 1020, 1020);
                inventoryScreen = new InventoryScreen(game.GraphicsDevice, InventoryTexture, InventoryDestinationRectangle);
            }
            Texture2D greenDotTexture = game.Content.Load<Texture2D>("HUD");
            greenDot = new GreenDot(game.GraphicsDevice, greenDotTexture, currentRoom);
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
            ResetLevel();
            InitializeGameplayState();
            //ResetLevel();
            ChangeState(GameState.Gameplay);
        }

        private void ResetLevel()
        {

            level = new Level(game.levelSpriteSheet, game.Content.RootDirectory);

            game.blockManager = new BlockManager(new List<string> { "Statue1", "Statue2" });
            game.itemManager = new ItemManager(new List<string> { "Compass", "Map" });
        }

        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.Gameplay:
                case GameState.Running:
                    // Handle the gameplay updates for both Gameplay and Running states
                    UpdateGameplay(gameTime);
                    break;
                case GameState.Paused:
                    keyboardController.Update();

                    // Handle paused state update
                    break;
                case GameState.ItemSelection:
                    // Handle item selection update
                    keyboardController.Update();
                    greenDot.Update();
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
            camera.CalculateTransformMatrix(level.CurrentRoomRow, level.CurrentRoomColumn);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.TransformMatrix);
            switch (currentState)
            {
                case GameState.Gameplay:
                    DrawGameplay();
                    hud.Draw();
                    break;
                case GameState.Paused:
                    // Draw paused screen
                    DrawGameplay();
                    hud.Draw();
                    break;
                case GameState.ItemSelection:
                    // Draw item selection screen
                    inventoryScreen.Draw();
                    greenDot.Draw();
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
                case GameState.Running:
                    DrawGameplay();
                    hud.Draw();
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
