using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Input;
using Legend_of_the_Power_Rangers.Collision;

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
        private LinkItemFactory linkItemFactory;
        private IItem item = new ItemCompass();
        private Texture2D itemTexture;
        private IBlock block = new BlockStatue1();
        private Texture2D blockTexture;

        private int itemIndex;
        private int blockIndex; 
        private int enemyIndex = 0;
        
        private IItem[] ItemList = {new ItemCompass(), new ItemMap(), new ItemKey(),
                                    new ItemHeartContainer(), new ItemTriforce(), new ItemWoodBoomerang(),
                                    new ItemBow(), new ItemHeart(), new ItemRupee(), new ItemBomb(), new ItemFairy(),
                                    new ItemClock(), new ItemBlueCandle(), new ItemBluePotion()};

        private IBlock[] BlockList = {new BlockStatue1(), new BlockStatue2(), new BlockSquare(), new BlockPush(), 
                                        new BlockFire(), new BlockBlueGap(), new BlockStairs(), new BlockWhiteBrick(), 
                                        new BlockLadder(), new BlockBlueFloor(), new BlockBlueSand(), new BlockWall(), new BlockOpenDoor(), 
                                        new BlockBombedWall(), new BlockKeyHole(), new BlockDiamond()};

        private string[] enemyTypes = { "RedOcto", "BlueOcto", "RedGorya", "BlueGorya", "RedMoblin", "DarkMoblin", "RedKnight" , "BlueKnight", "RedCentaur", "BlueCentaur", "DragonBoss" };

        private List<ICollision> loadedObjects;
        private CollisionManager collisionManager;

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
            //block = BlockList[0];
            block = BlockList[9];
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

            LinkSpriteFactory.Instance.SetSpriteSheet(linkSpriteSheet);

            linkItemFactory = new LinkItemFactory(itemTexture, projectileSpriteSheet, blockSpriteSheet);
            link = new Link();
            linkDecorator = new LinkDecorator(link);

            keyboardController = new KeyboardController(link.GetStateMachine(), linkItemFactory, linkDecorator, this);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemy = EnemyFactory.CreateEnemy(new Vector2(200, 200), enemyTypes[1]);

            itemTexture = Content.Load<Texture2D>("Items");
            blockTexture = Content.Load<Texture2D>("Blocks");
            itemIndex = 0;
            blockIndex = 0;

            loadedObjects = new();
            loadedObjects.Add(link);
            loadedObjects.Add(BlockList[9]);
            //add objects in current room here?
            SortingMachine.QuickSort(loadedObjects);

            collisionManager = new();
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

        public void ChangeEnemy(int direction)
        {
            enemyIndex += direction;
            if (enemyIndex >= enemyTypes.Length)
            {
                enemyIndex = 0;
            }
            else if (enemyIndex < 0)
            {
                enemyIndex = enemyTypes.Length - 1;
            }
            string newType = enemyTypes[enemyIndex];
            
            if (newType == "DragonBoss")
            {
                enemy = EnemyFactory.CreateEnemy(new Vector2(200, 200), newType);
            }
            else
            {
                if (enemy.enemyType == "DragonBoss")
                {
                    enemy = EnemyFactory.CreateEnemy(new Vector2(200, 200), newType);
                }
                else
                {
                    enemy.ChangeType(newType);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();

            link.Update(gameTime);
            //commented out because we arent using position anymore
            //linkItemFactory.Update(gameTime, link.GetPosition(), link.GetDirection());
            enemy.Update(gameTime);
            linkDecorator.Update(gameTime);
            if (item == null)
            {
                throw new InvalidOperationException("item not initialized");
            }
            item.Update(gameTime);
            block.Update(gameTime);
            base.Update(gameTime);

            collisionManager.Update(gameTime, loadedObjects);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            link.Draw(spriteBatch);
            linkItemFactory.Draw(spriteBatch);
            linkDecorator.Draw(spriteBatch);
            enemy.Draw(spriteBatch);

            item.Draw(itemTexture, spriteBatch);
            block.Draw(blockTexture, spriteBatch);
            base.Draw(gameTime);

            spriteBatch.End();
        }

    }
}
