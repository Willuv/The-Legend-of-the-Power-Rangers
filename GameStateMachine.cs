using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private Level level;

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
            linkDecorator = new LinkDecorator(link);
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
            // Reset game to initial state
            InitializeGameplayState();  // Reinitialize the gameplay state
            ChangeState(GameState.Gameplay);  // Set the current state to Gameplay
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
            linkDecorator.Draw(spriteBatch);
            game.linkItemFactory.Draw(spriteBatch);
        }
    }
}
