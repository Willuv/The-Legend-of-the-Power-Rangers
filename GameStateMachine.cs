using Legend_of_the_Power_Rangers.LevelCreation;
using Legend_of_the_Power_Rangers.Portals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Reset,
            Quit
        }

        public GameState currentState;
        private SpriteFont font;
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
        private ItemSelector itemSelector;
        private TriforceCompletionManager triforceManager;
        private DeathScreenManager deathManager;

        public GameStateMachine(Game1 game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            camera = Camera2D.Instance;

            audioManager = AudioManager.Instance;
            audioManager.Initialize(game.Content);

            InitializeGameplayState(); // Start the game in gameplay state will change to start state later on

            //Subscribing to room change call from collision
            DelegateManager.OnDoorEntered += (direction) =>
            {
                level.ChangeLevel(direction);
                ChangeState(GameState.RoomTransition);
            };
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
            Texture2D portalSpriteSheet = game.Content.Load<Texture2D>("Portal");
            font = game.Content.Load<SpriteFont>("ZeldaFont");

            // Set up factories
            BlockSpriteFactory.Instance.SetBlockSpritesheet(blockSpriteSheet);
            ItemSpriteFactory.Instance.SetItemSpritesheet(game.itemSpriteSheet);
            EnemySpriteFactory.Instance.SetEnemySpritesheet(game.enemySpritesheet);
            LinkSpriteFactory.Instance.SetLinkSpriteSheet(linkSpriteSheet);
            LinkSpriteFactory.Instance.SetGameStateMachine(this);
            EnemySpriteFactory.Instance.SetBossSpritesheet(bossSpriteSheet);
            EnemySpriteFactory.Instance.SetProjectileSpritesheet(projectileSpriteSheet);
            EnemySpriteFactory.Instance.SetItemSpritesheet(game.itemSpriteSheet);
            PortalSpriteFactory.Instance.SetPortalSpritesheet(portalSpriteSheet);

            game.linkItemFactory = new LinkItemFactory(game.itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet, portalSpriteSheet);

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
            linkInventory.PickUpItem(new ItemPortalGun());
            linkInventory.ActiveItem = ItemType.PortalGun;

            // Load the level
            level = new Level(game.levelSpriteSheet, game.Content.RootDirectory, font);

            if (hud == null)
            {
                Texture2D hudTexture = game.Content.Load<Texture2D>("HUD");
                Rectangle hudDestinationRectangle = new Rectangle(0, 0, 1020, 192);
                hud = new HUD(game.GraphicsDevice, hudTexture, hudDestinationRectangle, level.currentRoom);
            }


            // Set the Camera to current level
            camera = new Camera2D(level.CurrentRoomRow, level.CurrentRoomColumn);
            camera.CalculateRoomCamera(level.CurrentRoomRow, level.CurrentRoomColumn);
            // Set up controllers
            Texture2D itemSelectTexture = game.Content.Load<Texture2D>("HUD");
            Rectangle itemSelectorDestinationRectangle = new Rectangle(500, 180, 60, 60);
            int destX = 505;
            itemSelector = new ItemSelector(game.GraphicsDevice, itemSelectTexture, destX, linkInventory);

            keyboardController = new KeyboardController(link.GetStateMachine(), game.linkItemFactory, linkDecorator, game.blockManager, game.itemManager, game, this, itemSelector, linkInventory);
            mouseController = new MouseController(link.GetStateMachine(), game.linkItemFactory, linkDecorator, level, game);

            if (!AudioManager.Instance.IsMuted()) audioManager.PlayMusic("Dungeon");



            triforceManager = new TriforceCompletionManager(game.GraphicsDevice, this, link, font);
            deathManager = new DeathScreenManager(game.GraphicsDevice, link, this, spriteBatch, font);
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
                inventoryScreen = new InventoryScreen(game.GraphicsDevice, InventoryTexture, InventoryDestinationRectangle, linkInventory);
            }
            Texture2D greenDotTexture = game.Content.Load<Texture2D>("HUD");
            greenDot = new GreenDot(game.GraphicsDevice, greenDotTexture, currentRoom, linkInventory);

            
        }

        private void InitializeGameOverState()
        {
            // Game Over logic (e.g., display game over screen, stop gameplay)
            if (deathManager == null)
            {
                deathManager = new DeathScreenManager(game.GraphicsDevice,link,  this, spriteBatch, font);
            }
            MediaPlayer.Stop();
            if (!AudioManager.Instance.IsMuted()) audioManager.PlaySound("Link_Die");
        }

        private void InitializeWinningState()
        {
            if (triforceManager == null)
            {
                triforceManager = new TriforceCompletionManager(game.GraphicsDevice, this, link, font);
            }
            MediaPlayer.Stop();
            if (!AudioManager.Instance.IsMuted()) audioManager.PlayMusic("Win");
        }

        private void InitializeRoomTransitionState()
        {
            camera.CalculateRoomCamera(level.CurrentRoomRow, level.CurrentRoomColumn);
            camera.IsMoving = true;
        }


        public void ResetGame()
        {
            ResetLevel();
            InitializeGameplayState();
            hud.UpdateLink();
            ChangeState(GameState.Gameplay);
        }

        private void ResetLevel()
        {

            level = new Level(game.levelSpriteSheet, game.Content.RootDirectory, font);

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
                    hud.Update(level.currentRoom);
                    hud.UpdateLink();
                    triforceManager.Update(gameTime, this);
                    deathManager.deathUpdateCheck(gameTime);
                    break;
                case GameState.Paused:
                    keyboardController.Update();
                    hud.Update(level.currentRoom);
                    // Handle paused state update
                    break;
                case GameState.ItemSelection:
                    // Handle item selection update
                    keyboardController.Update();
                    itemSelector.Update(gameTime);
                    inventoryScreen.UpdateLinkInventory();
                    greenDot.Update();
                    break;
                case GameState.GameOver:
                    // Handle game over update
                    deathManager.Update(gameTime);
                    break;
                case GameState.Winning:
                    // Handle winning update
                    triforceManager.Update(gameTime, this);
                    break;
                case GameState.RoomTransition:
                    UpdateRoomTranstion(gameTime);
                    break;
                case GameState.Reset: 
                    ResetGame();
                    break;
                case GameState.Quit: 
                    game.Exit();
                    break;
            }
        }

        private void UpdateGameplay(GameTime gameTime)
        {
            keyboardController.Update();
            mouseController.Update();
            link.Update(gameTime);
            linkDecorator.Update(gameTime);
            game.linkItemFactory.Update(gameTime, link.destinationRectangle, link.GetDirection());
            level.Update(gameTime);
            camera.CalculateRoomCamera(level.CurrentRoomRow, level.CurrentRoomColumn);
            camera.CalculateTransformMatrix();

        }

        private void UpdateRoomTranstion(GameTime gameTime)
        {
            if (camera.IsMoving)
            {
                camera.CalculateMovement();
            }
            else
            {
                ChangeState(GameState.Running);
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.TransformMatrix);
            switch (currentState)
            {
                case GameState.Gameplay:
                    DrawGameplay();
                    break;
                case GameState.Paused:
                    // Draw paused screen
                    DrawGameplay();
                    break;
                case GameState.ItemSelection:
                    // Draw item selection screen
                    inventoryScreen.Draw();
                    itemSelector.Draw();
                    greenDot.Draw();
                    break;
                case GameState.GameOver:
                    // Draw game over screen
                    DrawGameplay();
                    spriteBatch.End(); // End gameplay SpriteBatch to prepare for Triforce
                    spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    deathManager.Draw(spriteBatch, new Rectangle(0, 0, 1020, 892));
                    break;
                case GameState.Winning:
                    // Draw winning screen
                    DrawGameplay();
                    spriteBatch.End(); // End gameplay SpriteBatch to prepare for Triforce
                    spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    triforceManager.Draw(spriteBatch, new Rectangle(0, 0, 1020, 892));
                    break;
                case GameState.RoomTransition:
                    level.Draw(game.enemySpritesheet, spriteBatch);
                    hud.Draw();
                    break;
                case GameState.Running:
                    DrawGameplay();
                    break;
            }
            spriteBatch.End();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            switch (currentState)
            {
                case GameState.Gameplay:
                case GameState.Running:
                case GameState.Paused:
                case GameState.RoomTransition:
                case GameState.GameOver:
                case GameState.Winning:
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
            hud.Draw();
        }

        
    }
}
