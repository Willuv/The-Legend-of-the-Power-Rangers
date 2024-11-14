using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static Legend_of_the_Power_Rangers.GameStateMachine;

namespace Legend_of_the_Power_Rangers.LevelCreation
{
    public class Level
    {
        int[,] map;
        List<IWall> walls;
        Dictionary<int, LevelLoader> rooms;
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
        private List<ICollision> loadedObjects;
        public Level(Texture2D levelSpriteSheet, String ContentPath)
        {
            this.ContentPath = ContentPath;
            this.levelSpriteSheet = levelSpriteSheet;
            rooms = new Dictionary<int, LevelLoader>();
            numRooms = 18;
            currentRoom = 1;
            loadedRoom = 1;
            currentRoomRow = 5;
            currentRoomColumn = 2;
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
                        rooms.Add(map[j,i], new LevelLoader(levelSpriteSheet, reader, i, j));
                        rooms[map[j, i]].ReadData();
                    }
                }
            }
            reader = new StreamReader(ContentPath + "/LinkDungeon1 - Room" + currentRoom + ".csv");
            loadedObjects = GetRoomObjects();
            loadedObjects.Add(LinkManager.GetLink());
            collisionManager = new();
            LinkManager.GetLink().UpdatePosition(new Vector2(currentRoomColumn * 1020 + 490, currentRoomRow * 698 + 755));

            //listener for room change from stairs or wall master
            DelegateManager.OnChangeToSpecificRoom += (roomNum) =>
            {
                SelectLevel(roomNum);
            };
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
            roomObjects.AddRange(rooms[currentRoom].Blocks);
            roomObjects.AddRange(rooms[currentRoom].Enemies);
            roomObjects.AddRange(rooms[currentRoom].Items);
            roomObjects.AddRange(rooms[currentRoom].Doors);
            roomObjects.AddRange(walls);

            return roomObjects;

        }
        public void Draw(Texture2D enemySpritesheet, SpriteBatch spriteBatch)
        {
            foreach (IWall wall in walls)
            {
                wall.Draw(spriteBatch, levelSpriteSheet);
            }
            foreach (IDoor door in rooms[currentRoom].Doors)
            {
                door.Draw(spriteBatch);
            }
            foreach (IBlock block in rooms[currentRoom].Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (IItem item in rooms[currentRoom].Items)
            {
                item.Draw(spriteBatch);
            }
            foreach (IEnemy enemy in rooms[currentRoom].Enemies)
            {
                    enemy.Draw(enemySpritesheet, spriteBatch);
            }
        }
        public void Update(GameTime gametime) 
        {
            List<int> toRemove = new List<int>();
            if (currentRoom != loadedRoom)
            {
                //rooms[currentRoom].LoadEnemies();

                loadedRoom = currentRoom;

                //changing the loaded objects based on current room
                loadedObjects = GetRoomObjects();
                loadedObjects.Add(LinkManager.GetLink());
            }
            foreach (IItem item in rooms[currentRoom].Items)
            {
                item.Update(gametime);
                if (item.PickedUp)
                {
                    toRemove.Add(rooms[currentRoom].Items.IndexOf(item));
                }
            }
            foreach (int removeIndex in toRemove)
            {
                rooms[currentRoom].Items.RemoveAt(removeIndex);
            }
            toRemove.Clear();
            foreach (IBlock block in rooms[currentRoom].Blocks)
            {
                block.Update(gametime);
            }
            foreach (IEnemy enemy in rooms[currentRoom].Enemies)
            {
                enemy.Update(gametime);
            }
            foreach (int removeIndex in toRemove)
            {
                rooms[currentRoom].Enemies.RemoveAt(removeIndex);
            }
            toRemove.Clear();
            foreach (IDoor door in rooms[currentRoom].Doors)
            {
                if (door.DoorType == DoorType.Diamond)
                {
                    if (rooms[currentRoom].Enemies.Count == 0)
                    {
                        door.IsOpen = true;
                    }
                }

                door.Update(gametime);
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
            loadedObjects.Clear();
            loadedObjects.Add(LinkManager.GetLink());
            LinkManager.GetLink().CollisionHitbox = new Rectangle(1020 * currentRoomColumn + 400, 698 * currentRoomRow + 500, 48, 48);
        }
        public void ChangeLevel(CollisionDirection direction)
        {
            switch (direction) 
            {
                case (CollisionDirection.Left):
                    currentRoomColumn++;
                    LinkManager.GetLink().UpdatePosition(new Vector2(300, 0));
                    break;
                case (CollisionDirection.Right):
                    currentRoomColumn--;
                    LinkManager.GetLink().UpdatePosition(new Vector2(-300, 0));
                    break;
                case (CollisionDirection.Top):
                    currentRoomRow++;
                    LinkManager.GetLink().UpdatePosition(new Vector2(0, 350));
                    break;
                case (CollisionDirection.Bottom):
                    currentRoomRow--;
                    LinkManager.GetLink().UpdatePosition(new Vector2(0, -350));
                    break;
            }
            currentRoom = map[currentRoomRow, currentRoomColumn];
            loadedObjects.Clear();
            loadedObjects.Add(LinkManager.GetLink());
        }
        public void SelectLevel(int level)
        {
            currentRoom = level;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (map[j, i] == currentRoom)
                    {
                        currentRoomRow = j;
                        currentRoomColumn = i;
                    }
                }
            }
            currentRoom = map[currentRoomRow, currentRoomColumn];
            loadedObjects.Clear();
            loadedObjects.Add(LinkManager.GetLink());
        }
    }
}
