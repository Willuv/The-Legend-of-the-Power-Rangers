using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace Legend_of_the_Power_Rangers.LevelCreation
{
    public class Level
    {
        int[,] map;
        List<IWall> walls;
        LevelLoader loader;
        Texture2D levelSpriteSheet;
        StreamReader reader;
        Rectangle wallsSource;
        Rectangle wallsDestination;
        private String ContentPath;
        int numRooms;
        public int currentRoom;
        int currentRoomRow;
        public int CurrentRooom
        {
            get { return currentRoom; }
        }
        public int CurrentRoomRow
        {
            get { return currentRoomRow; }
        }
        int currentRoomColumn;
        public int CurrentRoomColumn
        {
            get { return currentRoomColumn; }
        }
        int loadedRoom;
        int scaleFactor = 4;
        private CollisionManager collisionManager;
        private Camera2D camera;
        private List<ICollision> loadedObjects;
        private int LinkWidth;
        private int LinkHeight;
        public Level(Texture2D levelSpriteSheet, String ContentPath)
        {
            this.ContentPath = ContentPath;
            this.levelSpriteSheet = levelSpriteSheet;
            loader = new LevelLoader(levelSpriteSheet);
            LinkWidth = LinkManager.GetLink().CollisionHitbox.Width;
            LinkHeight = LinkManager.GetLink().CollisionHitbox.Height;
            numRooms = 18;
            currentRoom = 3;
            loadedRoom = 3;
            currentRoomRow = 5;
            currentRoomColumn = 3;
            walls = new List<IWall>();
            map = new int[,]
            {
                { 18, 17, 16, -1, -1, 0},
                { -1, -1, 13, -1, 14, 15},
                { 10, 9, 8, 11, 12, -1},
                { -1, 6, 5, 7, -1, -1},
                { -1, -1, 4, -1, -1, -1},
                { -1, 2, 1, 3, -1, -1}
            };
            for (int i = 0; i < 6; i++)
            { 
                for (int j = 0; j < 6; j++) 
                { 
                    if (map[j,i] != -1)
                    {
                        if (map[j,i] != 18)
                        {
                            CreateWalls(i, j);
                        }
                        reader = new StreamReader(ContentPath + "/LinkDungeon1 - Room" + map[j,i] + ".csv");
                        loader.ReadData(reader, i, j);
                    }
                }
            }
            reader = new StreamReader(ContentPath + "/LinkDungeon1 - Room" + currentRoom + ".csv");
            loadedObjects = GetRoomObjects();
            loadedObjects.Add(LinkManager.GetLink());
            collisionManager = new();
            LinkManager.GetLink().CollisionHitbox = new Rectangle(1020 * currentRoomColumn + 400, 698 * currentRoomRow + 500, LinkWidth, LinkHeight);
        }
        private void CreateWalls(int RoomRow, int RoomColumn)
        {
            for (int i = 0; i < 8; i++)
            {
                walls.Add(new Wall(i, RoomRow, RoomColumn));
            }

        }
        public List<ICollision> GetRoomObjects()
        {
            List<ICollision> roomObjects = new();
            // Add blocks, enemies, and items to the list
            roomObjects.AddRange(loader.Blocks);
            roomObjects.AddRange(loader.Enemies);
            roomObjects.AddRange(loader.Items);
            roomObjects.AddRange(loader.Doors);
            roomObjects.AddRange(walls);

            return roomObjects;

        }

        public void Draw(Texture2D enemySpritesheet, SpriteBatch spriteBatch)
        {
            foreach (IWall wall in walls)
            {
                wall.Draw(spriteBatch, levelSpriteSheet);
            }
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
            List<int> toRemove = new List<int>();
            if (currentRoom != loadedRoom)
            {
                loader.LoadEnemies(reader, currentRoomRow, currentRoomColumn);

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
                /*if (enemy.Health == 0)
                {
                    toRemove.Add(loader.Enemies.IndexOf(enemy));
                }*/
            }
            foreach (int removeIndex in toRemove)
            {
                loader.Enemies.RemoveAt(removeIndex);
            }
            toRemove.Clear();
            foreach (IDoor door in loader.Doors)
            {
                if (door.DoorType == DoorType.Diamond)
                {
                    if (loader.Enemies.Count == 0)
                    {
                        door.IsOpen = true;
                    }
                }
            }
            collisionManager.Update(loadedObjects);
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
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (map[j,i] == currentRoom)
                    {
                        currentRoomRow = j;
                        currentRoomColumn = i;
                    }
                }
            }
            loader.DeloadRoom();
            loadedObjects.Clear();
            loadedObjects.Add(LinkManager.GetLink());
            LinkManager.GetLink().CollisionHitbox = new Rectangle(1020 * currentRoomColumn + 400, 698 * currentRoomRow + 500, LinkWidth, LinkHeight);
            reader = new StreamReader(ContentPath + "/LinkDungeon1 - Room" + currentRoom + ".csv");
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
                    currentRoomRow++;
                    break;
            }
            loader.DeloadRoom();
            loadedObjects.Clear();
            loadedObjects.Add(LinkManager.GetLink());
            currentRoom = map[currentRoomRow, currentRoomColumn];
            if (currentRoom != -1)
            {
                reader = new StreamReader(ContentPath + "/LinkDungeon1 - Room" + currentRoom + ".csv");
            }
        }
    }
}
