using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Legend_of_the_Power_Rangers
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Link link;
        private LinkStateMachine stateMachine;
        private LinkMovement movement;
        private KeyboardController keyboardController;
        private Enemy enemy;
        private DragonBoss DragonBoss;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

            Texture2D linkSpriteSheet = Content.Load<Texture2D>("Link Sprites");
            stateMachine = new LinkStateMachine(linkSpriteSheet);
            link = new Link(linkSpriteSheet);
            movement = new LinkMovement(link);

            keyboardController = new KeyboardController(stateMachine);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemy = new Enemy(new Vector2(200, 200)); 
            DragonBoss = new DragonBoss(new Vector2(400, 150));
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();

            movement.UpdateMovement(stateMachine);
            link.Update(gameTime);
            enemy.Update(gameTime);
            DragonBoss.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            link.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            DragonBoss.Draw(spriteBatch);
            base.Draw(gameTime);
            
            spriteBatch.End();
        }
    }
}
