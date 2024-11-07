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
        public int currentRoom;
        int loadedRoom;
        int scaleFactor = 4;
        private StreamReader reader;
        private CollisionManager collisionManager;
        private List<ICollision> loadedObjects;
        public Level(Texture2D levelSpriteSheet, StreamReader reader, String ContentPath)
        {
            this.reader = reader;
            this.ContentPath = ContentPath;
            this.levelSpriteSheet = levelSpriteSheet;
            wallsSource = new Rectangle(0, 0, 255, 175);
            wallsDestination = new Rectangle(0, 192, 255 * scaleFactor, 175 * scaleFactor);
            loader = new LevelLoader(levelSpriteSheet);
            numRooms = 18;
            currentRoom = 0;
            loadedRoom = 0;
            loader.Load(reader);
            loadedObjects = GetRoomObjects();
            loadedObjects.Add(LinkManager.GetLink());
            collisionManager = new();
        }

        public List<ICollision> GetRoomObjects()
        {
            List<ICollision> roomObjects = new();
            // Add blocks, enemies, and items to the list
            roomObjects.AddRange(loader.Blocks);
            roomObjects.AddRange(loader.Enemies);
            roomObjects.AddRange(loader.Items);

            return roomObjects;

        }

        public void Draw(Texture2D enemySpritesheet, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(levelSpriteSheet, wallsDestination, wallsSource, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
            foreach (IDoor door in loader.Doors)
            {
                door.Draw(spriteBatch);
            }
            foreach (IBlock block in loader.Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (IItem item in loader.Items)
            {
                item.Draw(spriteBatch);
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

                //changing the loaded objects based on current room
                loadedObjects = GetRoomObjects();
                loadedObjects.Add(LinkManager.GetLink());
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
            collisionManager.Update(gametime, loadedObjects);
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
            loadedObjects.Clear();
            loadedObjects.Add(LinkManager.GetLink());
            reader = new StreamReader(ContentPath+ "\\LinkDungeon1 - Room" + currentRoom + ".csv");
        }
    }
}
