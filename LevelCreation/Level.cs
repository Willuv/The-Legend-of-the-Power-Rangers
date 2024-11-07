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
        int[,] map;
        IWall[] walls;
        LevelLoader loader;
        Texture2D levelSpriteSheet;
        Rectangle wallsSource;
        Rectangle wallsDestination;
        private String ContentPath;
        int numRooms;
        int currentRoom;
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
            loader = new LevelLoader(levelSpriteSheet);
            numRooms = 18;
            currentRoom = 0;
            loadedRoom = 0;
            currentRoomRow = 5;
            currentRoomColumn = 2;
            map = new int[,]
            {
                { 18, 17, 16, -1, -1, -1},
                { -1, -1, 13, -1, -1, -1},
                { 10, 9, 8, 11, 12, -1},
                { -1, 6, 5, 7, -1, -1},
                { -1, -1, 4, -1, -1, -1},
                { -1, 2, 1, 3, -1, -1}
            };
            walls = CreateWalls();
            loader.Load(reader);
            loadedObjects = GetRoomObjects();
            loadedObjects.Add(LinkManager.GetLink());
            collisionManager = new();
        }
        private IWall[] CreateWalls()
        {
            IWall[] walls = new IWall[8];
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i] = new Wall(i, currentRoomRow, currentRoomColumn);
            }

            return walls;
        }
        public List<ICollision> GetRoomObjects()
        {
            List<ICollision> roomObjects = new();
            // Add blocks, enemies, and items to the list
            roomObjects.AddRange(loader.Blocks);
            roomObjects.AddRange(loader.Enemies);
            roomObjects.AddRange(loader.Items);
            //roomObjects.AddRange(loader.Doors);

            return roomObjects;

        }

        public void Draw(Texture2D enemySpritesheet, SpriteBatch spriteBatch)
        {
            if (currentRoom != 18)
            {
                foreach (IWall wall in walls)
                {
                    wall.Draw(spriteBatch, levelSpriteSheet);
                }
                foreach (IDoor door in loader.Doors)
                {
                    door.Draw(spriteBatch);
                }
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
            List<int> toRemove = new List<int>();
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
                if (item.PickedUp)
                {
                    toRemove.Add(loader.Items.IndexOf(item));
                }
            }
            foreach (int removeIndex in toRemove)
            {
                loader.Items.RemoveAt(removeIndex);
            }
            toRemove.Clear();
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
        public void MouseChangeLevel(int direction)
        {
            currentRoom += direction;
            if (currentRoom >= numRooms)
            {
                currentRoom = 0;
            }
            if (currentRoom < 0)
            {
                currentRoom = numRooms;
            }
            loader.DeloadRoom();
            loadedObjects.Clear();
            loadedObjects.Add(LinkManager.GetLink());
            reader = new StreamReader(ContentPath + "\\LinkDungeon1 - Room" + currentRoom + ".csv");
        }
        public void ChangeLevel(String direction)
        {
            switch (direction) 
            {
                case ("Left"):
                    currentRoomColumn--;
                    break;
                case ("Right"):
                    currentRoomColumn++;
                    break;
                case ("Up"):
                    currentRoomRow--;
                    break;
                case ("Down"):
                    currentRoomRow--;
                    break;
            }
            loader.DeloadRoom();
            loadedObjects.Clear();
            loadedObjects.Add(LinkManager.GetLink());
            currentRoom = map[currentRoomRow, currentRoomColumn];
            reader = new StreamReader(ContentPath + "\\LinkDungeon1 - Room" + currentRoom + ".csv");
        }
    }
}
