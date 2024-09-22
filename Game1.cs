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
        private KeyboardController keyboardControl;
        private Enemy enemy; // Consolidate to use only 'enemy'

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            keyboardControl = new KeyboardController();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load textures in the sprite factory
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            // Initialize enemy at a starting position
            enemy = new Enemy(new Vector2(200, 200)); 
        }

        protected override void Update(GameTime gameTime)
        {
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
            if (enemy != null)
            {
                enemy.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
