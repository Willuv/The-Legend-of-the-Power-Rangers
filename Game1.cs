using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private LinkItemFactory linkItemFactory;
        private IItem item = new ItemCompass();
        private Texture2D itemTexture;
        private IBlock block = new BlockStatue1();
        private Texture2D blockTexture;

        private List<IEnemy> sprites = new List<IEnemy>();
        private Texture2D enemySpritesheet;
        public Texture2D bossSpritesheet;

        private int itemIndex;
        private int blockIndex;
        private int enemyIndex;

        private IItem[] ItemList = {new ItemCompass(), new ItemMap(), new ItemKey(),
                                    new ItemHeartContainer(), new ItemTriforce(), new ItemWoodBoomerang(),
                                    new ItemBow(), new ItemHeart(), new ItemRupee(), new ItemBomb(), new ItemFairy(),
                                    new ItemClock(), new ItemBlueCandle(), new ItemBluePotion()};

        private IBlock[] BlockList = {new BlockStatue1(), new BlockStatue2(), new BlockSquare(), new BlockPush(),
                                        new BlockFire(), new BlockBlueGap(), new BlockStairs(), new BlockWhiteBrick(),
                                        new BlockLadder(), new BlockBlueFloor(), new BlockBlueSand(), new BlockWall(), new BlockOpenDoor(),
                                        new BlockBombedWall(), new BlockKeyHole(), new BlockDiamond()};

        private List<ICollision> loadedObjects;
        private CollisionManager collisionManager;


        private void InitializeEnemies()
        {
            sprites.Add(new RedOcto());
            sprites.Add(new BlueOcto());
            sprites.Add(new BlueCentaur());
            sprites.Add(new BlueGorya());
            sprites.Add(new BlueKnight());
            sprites.Add(new DarkMoblin());
            sprites.Add(new DragonBoss(bossSpritesheet, enemySpritesheet));
            sprites.Add(new RedCentaur());
            sprites.Add(new RedGorya());
            sprites.Add(new RedKnight());
            sprites.Add(new RedMoblin());
            sprites.Add(new BatKeese());
            sprites.Add(new Skeleton());
            sprites.Add(new GelSmallBlack());
            sprites.Add(new GelSmallTeal());
            sprites.Add(new GelBigGreen());
            sprites.Add(new GelBigGray());
            sprites.Add(new WallMaster());
        }

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
        }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeEnemies();
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



            enemySpritesheet = Content.Load<Texture2D>("Enemies");
            bossSpritesheet = Content.Load<Texture2D>("Bosses");

            itemTexture = Content.Load<Texture2D>("Items");
            blockTexture = Content.Load<Texture2D>("Blocks");

            itemIndex = 0;
            blockIndex = 0;

            loadedObjects = new();
            //these add calls are for testing collision. will be gone for real sprint
            InitializeEnemies(); //also remove later
            loadedObjects.Add(link);
            loadedObjects.Add(sprites[0]);
            loadedObjects.Add(sprites[6]); //should be dragon boss for testing
            loadedObjects.Add(BlockList[9]);

            //keep these
            SortingMachine.QuickSort(loadedObjects);
            collisionManager = new();


            enemyIndex = 0;
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
            if (enemyIndex >= sprites.Count)
            {
                enemyIndex = 0;
            }
            else if (enemyIndex < 0)
            {
                enemyIndex = sprites.Count - 1;

            }
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();

            link.Update(gameTime);

            //commented out because we arent using position anymoreq
            linkItemFactory.Update(gameTime, link.DestinationRectangle, link.GetDirection());
            sprites[enemyIndex].Update(gameTime);
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

            sprites[enemyIndex].Draw(enemySpritesheet, spriteBatch);
            item.Draw(itemTexture, spriteBatch);
            block.Draw(blockTexture, spriteBatch);
            base.Draw(gameTime);

            spriteBatch.End();
        }

    }
}
