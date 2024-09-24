using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Legend_of_the_Power_Rangers
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Link link;
        private LinkStateMachine stateMachine;
        private LinkMovement movement;
        private KeyboardController keyboardController;
        private Enemy enemy;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D linkSpriteSheet = Content.Load<Texture2D>("Link Sprites");
            stateMachine = new LinkStateMachine(linkSpriteSheet);
            link = new Link(linkSpriteSheet, stateMachine);
            movement = new LinkMovement(link, stateMachine);

            keyboardController = new KeyboardController(stateMachine);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemy = new Enemy(new Vector2(200, 200)); 
        }

        protected override void Update(GameTime gameTime)
        {

            keyboardController.Update();
            movement.UpdateMovement(stateMachine);
            link.Update(gameTime);

            if (enemy == null)
            {
                throw new InvalidOperationException("Enemy not initialized");
            }
            enemy.Update(gameTime);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            link.Draw(_spriteBatch);
            if (enemy != null)
            {
                enemy.Draw(_spriteBatch);
            }
            _spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
