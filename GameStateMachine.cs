﻿using Legend_of_the_Power_Rangers.LevelCreation;
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

        // State-specific objects
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
            // Initialize gameplay elements
            link = new Link();
            LinkManager.Initialize(link);
            linkDecorator = new LinkDecorator(link);
            LinkManager.SetLinkDecorator(linkDecorator);
            LinkManager.SetLink(link);

            keyboardController = new KeyboardController(link.GetStateMachine(), game.linkItemFactory, linkDecorator, game.blockManager, game.itemManager, game, this);
            mouseController = new MouseController(link.GetStateMachine(), game.linkItemFactory, linkDecorator, game.level, game);
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
            InitializeGameplayState();  // Reinitialize gameplay elements
            ResetLevel();  // Call the new method to reset the level
            ChangeState(GameState.Gameplay);  // Set the current state to Gameplay
        }

        private void ResetLevel()
        {
            // Reload the level from the first room or initial state
            string initialRoomPath = game.Content.RootDirectory + "\\LinkDungeon1 - Room1.csv";
            game.reader = new StreamReader(initialRoomPath);
            game.level = new Level(game.levelSpriteSheet, game.reader, game.Content.RootDirectory);

            // Reset other related elements (e.g., blocks, items, etc.)
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
                    // Handle paused state update (if any)
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
            game.level.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
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
            game.level.Draw(game.enemySpritesheet, spriteBatch);
            game.linkItemFactory.Draw(spriteBatch);
            linkDecorator.Draw(spriteBatch);
        }
    }
}
