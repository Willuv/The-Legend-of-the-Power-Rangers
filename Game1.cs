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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Link link;
        private LinkDecorator linkDecorator;
        private KeyboardController keyboardController;
        private Enemy enemy;
        private DragonBoss DragonBoss;
        private IItem item = new ItemCompass();
        private Texture2D itemTexture;
        
        private int itemIndex = 0;

         
        
        private IItem[] ItemList = {new ItemCompass(), new ItemMap(), new ItemKey(), 
                                    new ItemHeartContainer(), new ItemTriforce(), new ItemWoodBoomerang(), 
                                    new ItemBow(), new ItemHeart(), new ItemRupee(), new ItemBomb(), new ItemFairy(), 
                                    new ItemClock(), new ItemBlueCandle(), new ItemBluePotion()};


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D linkSpriteSheet = Content.Load<Texture2D>("Link Sprites");
            link = new Link(linkSpriteSheet);
            linkDecorator = new LinkDecorator(link);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemy = new Enemy(new Vector2(200, 200)); 
            DragonBoss = new DragonBoss(new Vector2(400, 150));
            itemTexture = Content.Load<Texture2D>("Items");

            keyboardController = new KeyboardController(stateMachine, linkDecorator, this);
            
        }

        protected override void Update(GameTime gameTime)
        {


            link.Update(gameTime);
            enemy.Update(gameTime);
            if (item == null)
            {
                throw new InvalidOperationException("item not initialized");
            }
            item.Update(gameTime);
            keyboardController.Update();

            DragonBoss.Update(gameTime);
            linkDecorator.Update(gameTime);

            base.Update(gameTime);
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            link.Draw(spriteBatch);
            DragonBoss.Draw(spriteBatch);
            
            linkDecorator.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            item.Draw(itemTexture, spriteBatch);
            base.Draw(gameTime);
            
            spriteBatch.End();
        }
        

    }
}
