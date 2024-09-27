using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        private LinkDecorator linkDecorator;
        private LinkMovement movement;
        private KeyboardController keyboardController;
        private Enemy enemy;
        private IItem item = new ItemCompass();
        private Texture2D itemTexture;
        private int itemIndex = 0;

         
        
        private IItem[] ItemList = {new ItemCompass(), new ItemMap(), new ItemKey(), 
                                    new ItemHeartContainer(), new ItemArrow(), new ItemTriforce(), new ItemWoodBoomerang(), 
                                    new ItemBow(), new ItemHeart(), new ItemRupee(), new ItemBomb(), new ItemFairy(), 
                                    new ItemClock(), new ItemBlueCandle(), new ItemBluePotion()};

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void ChangeItem(int direction)
        {
            itemIndex += direction;
            if (itemIndex >= ItemList.Length)
            {
                itemIndex = 0;
            }
            if (itemIndex < 0)
            {
                itemIndex = ItemList.Length - 1;
            }
            item = ItemList[itemIndex];
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
            linkDecorator = new LinkDecorator(link);
            movement = new LinkMovement(link, stateMachine);

            

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemy = new Enemy(new Vector2(200, 200)); 
            itemTexture = Content.Load<Texture2D>("Items");

            keyboardController = new KeyboardController(stateMachine, linkDecorator, this);
            
        }

        protected override void Update(GameTime gameTime)
        {

            
            movement.UpdateMovement(stateMachine);
            linkDecorator.Update(gameTime);

            if (enemy == null)
            {
                throw new InvalidOperationException("Enemy not initialized");
            }
            enemy.Update(gameTime);
            if (item == null)
            {
                throw new InvalidOperationException("item not initialized");
            }
            item.Update(gameTime);
            base.Update(gameTime);
            keyboardController.Update();

        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            linkDecorator.Draw(_spriteBatch);
            if (enemy != null)
            {
                enemy.Draw(_spriteBatch);
            }
            item.Draw(itemTexture, _spriteBatch);
            _spriteBatch.End(); 
            base.Draw(gameTime);
        }
        

    }
}
