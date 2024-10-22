using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace Legend_of_the_Power_Rangers.LevelCreation
{
    public class Level
    {
        LevelLoader loader;
        Texture2D levelSpriteSheet;
        Rectangle wallsSource;
        Rectangle wallsDestination;
        private String ContentPath;
        int numRooms;
        int currentRoom;
        int loadedRoom;
        int scaleFactor = 5;
        private StreamReader reader;
        //private CollisionManager collisionManager;
        //private List<ICollision> loadedObjects;
        public Level(Texture2D levelSpriteSheet, StreamReader reader, String ContentPath)
        {
            this.reader = reader;
            this.ContentPath = ContentPath;
            this.levelSpriteSheet = levelSpriteSheet;
            wallsSource = new Rectangle(0, 0, 255, 175);
            wallsDestination = new Rectangle(5, 5, 255 * scaleFactor, 175 * scaleFactor);
            loader = new LevelLoader(levelSpriteSheet);
            numRooms = 18;
            currentRoom = 0;
            loadedRoom = 0;
            loader.Load(reader);

            //Jake's last minute attempt to fix collision with the new level loading system
            //collisionManager = new();
            //loadedObjects = new();
            //loadedObjects.AddRange(loader.Blocks);
            //loadedObjects.AddRange(loader.Items);
            //loadedObjects.AddRange(loader.Enemies);
        }
        public void Draw(Texture2D enemySpritesheet, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(levelSpriteSheet, wallsDestination, wallsSource, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
            foreach (IDoor door in loader.Doors)
            {
                door.Draw(spriteBatch);
            }
            foreach (IItem item in loader.Items)
            {
                item.Draw(spriteBatch);
            }
            foreach (IBlock block in loader.Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (IEnemy enemy in loader.Enemies)
            {
                enemy.Draw(enemySpritesheet, spriteBatch);
            }
        }

        public void Update(GameTime gametime) 
        {
            
            if (currentRoom != loadedRoom)
            {
                loader.Load(reader);
                
                loadedRoom = currentRoom;
            }
            foreach (IItem item in loader.Items)
            {
                item.Update(gametime);
            }
            foreach (IBlock block in loader.Blocks)
            {
                block.Update(gametime);
            }
            foreach (IEnemy enemy in loader.Enemies)
            {
                enemy.Update(gametime);
            }
            //collisionManager.Update(gametime, loadedObjects);
        }
        public void ChangeLevel(int direction)
        {
            currentRoom += direction;
            if (currentRoom >= numRooms)
            {
                currentRoom = 1;
            }
            if (currentRoom < 1)
            {
                currentRoom = numRooms - 1;
            }
            loader.DeloadRoom();
            reader = new StreamReader(ContentPath+ "\\LinkDungeon1 - Room" + currentRoom + ".csv");
        }
    }
}
