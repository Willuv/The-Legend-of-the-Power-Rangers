using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;
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
        private LinkItemFactory linkItemFactory;
        private IItem item = new ItemCompass();
        private Texture2D itemTexture;
        private IBlock block = new BlockStatue1();
        private Texture2D blockTexture;

        private int itemIndex;
        private int blockIndex; 



        private IItem[] ItemList = {new ItemCompass(), new ItemMap(), new ItemKey(),
                                    new ItemHeartContainer(), new ItemTriforce(), new ItemWoodBoomerang(),
                                    new ItemBow(), new ItemHeart(), new ItemRupee(), new ItemBomb(), new ItemFairy(),
                                    new ItemClock(), new ItemBlueCandle(), new ItemBluePotion()};

        private IBlock[] BlockList = {new BlockStatue1(), new BlockStatue2(), new BlockSquare(), new BlockPush(), 
                                        new BlockFire(), new BlockBlueGap(), new BlockStairs(), new BlockWhiteBrick(), 
                                        new BlockLadder(), new BlockBlueFloor(), new BlockBlueSand(), new BlockWall(), new BlockOpenDoor(), 
                                        new BlockBombedWall(), new BlockKeyHole(), new BlockDiamond()};



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ResetGame()
        {
            base.Initialize();
            LoadContent();
            block = BlockList[0];
            item = ItemList[0];
            //Alex add enemy = EnemyList[0];
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D linkSpriteSheet = Content.Load<Texture2D>("Link Sprites");
            Texture2D projectileSpriteSheet = Content.Load<Texture2D>("Projectiles");
            Texture2D blockSpriteSheet = Content.Load<Texture2D>("Blocks");
            itemTexture = Content.Load<Texture2D>("Items");
            linkItemFactory = new LinkItemFactory(itemTexture, projectileSpriteSheet, blockSpriteSheet);
            link = new Link(linkSpriteSheet);
            linkDecorator = new LinkDecorator(link);

            keyboardController = new KeyboardController(link.GetStateMachine(), linkItemFactory, linkDecorator, this);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemy = new Enemy(new Vector2(200, 200)); 
            DragonBoss = new DragonBoss(new Vector2(400, 150));
            
            itemTexture = Content.Load<Texture2D>("Items");
            blockTexture = Content.Load<Texture2D>("Blocks");
            itemIndex = 0;
            blockIndex = 0;
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

        public void ChangeBlock(int direction)
        {
            blockIndex += direction;
            if (blockIndex >= BlockList.Length)
            {
                blockIndex = 0;
            }
            if (blockIndex < 0)
            {
                blockIndex = BlockList.Length - 1;
            }
            block = BlockList[blockIndex];
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();

            link.Update(gameTime);
            linkItemFactory.Update(gameTime, link.GetPosition(), link.GetDirection());
            enemy.Update(gameTime);
            DragonBoss.Update(gameTime);
            linkDecorator.Update(gameTime);
            if (item == null)
            {
                throw new InvalidOperationException("item not initialized");
            }
            item.Update(gameTime);
            block.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            link.Draw(spriteBatch);
            linkItemFactory.Draw(spriteBatch);
            DragonBoss.Draw(spriteBatch);
            
            linkDecorator.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            item.Draw(itemTexture, spriteBatch);
            block.Draw(blockTexture, spriteBatch);
            base.Draw(gameTime);
            
            spriteBatch.End();
        }
    }
}
