using Legend_of_the_Power_Rangers.Portals;
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
        List<int> toRemove;
        Texture2D levelSpriteSheet;
        StreamReader reader;
        Rectangle wallsSource;
        Rectangle wallsDestination;
        private String ContentPath;
        private String oldManQuote;
        private Vector2 quotePos;
        private SpriteFont font;
        int numRooms;
        public int currentRoom;
        int currentRoomRow;
        public int CurrentRoom
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
        Dictionary<int, TimeSpan> deathTimers = new Dictionary<int, TimeSpan>();
        
        private PortalManager portalManager;
        private Camera2D camera;
        
        private int enemiesKilled = 0;
        private bool keyDropped1 = false;
        private bool keyDropped2 = false;
        private bool bombDropped1 = false;
        private bool bombDropped2 = false;
        private bool secretActivated = false;
        private bool doorUnlocked = false;

        private List<ICollision> loadedObjects;
        public Level(Texture2D levelSpriteSheet, String ContentPath, SpriteFont font)
        {
            this.ContentPath = ContentPath;
            this.levelSpriteSheet = levelSpriteSheet;
            toRemove = new List<int>();
            rooms = new Dictionary<int, LevelLoader>();
            this.font = font;
            oldManQuote = "SKIBIDI TOILET";
            quotePos = new Vector2(300, 1800);
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

            portalManager = new();

            //listener for room change from stairs or wall master
            DelegateManager.OnChangeToSpecificRoom += (roomNum) =>
            {
                if (roomNum != currentRoom) SelectLevel(roomNum);
            };
        }
        private void CreateWalls(int RoomRow, int RoomColumn)
        {
            bool visible = (currentRoom != 18);
            for (int i = 0; i < 8; i++)
            {
                walls.Add(new Wall(i, RoomRow, RoomColumn, visible));
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
            // for loop to draw for every room, even if not current
            // necessary to keep look during transitions and such
            for (int i = 0; i < 19; i++)
            {
                foreach (IDoor door in rooms[i].Doors)
                {
                    door.Draw(spriteBatch);
                }
                foreach (IBlock block in rooms[i].Blocks)
                {
                    block.Draw(spriteBatch);
                }
            }
            foreach (IItem item in rooms[currentRoom].Items)
            {
                item.Draw(spriteBatch);
            }
            foreach (IEnemy enemy in rooms[currentRoom].Enemies)
            {
                enemy.Draw(enemySpritesheet, spriteBatch);
            }
            if (currentRoom == 10)
            {
                spriteBatch.DrawString(font, oldManQuote, quotePos, Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 1.0f);
            }
            portalManager.Draw(spriteBatch);
        }
        public void Update(GameTime gametime) 
        {
            
            if (currentRoom != loadedRoom)
            {

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
                if (block.BlockType == BlockType.Push)
                {
                    if (block.IsMoving)
                    {
                        secretActivated = true;
                    }
                }
            }
            
            foreach (IEnemy enemy in rooms[currentRoom].Enemies)
            {
                enemy.Update(gametime);
                if (enemy.isDead)
                {
                    int enemyIndex = rooms[currentRoom].Enemies.IndexOf(enemy);
                    //toRemove.Add(rooms[currentRoom].Enemies.IndexOf(enemy));
                    if (!deathTimers.ContainsKey(enemyIndex))
                    {
                        deathTimers[enemyIndex] = gametime.TotalGameTime;
                    }
                    if (gametime.TotalGameTime - deathTimers[enemyIndex] >= TimeSpan.FromMilliseconds(1500))
                    {
                        toRemove.Add(enemyIndex);
                        deathTimers.Remove(enemyIndex);
                        //toRemove.Add(rooms[currentRoom].Enemies.IndexOf(enemy));
                    }
                    if (enemy.isDead)
                    {
                        if (!enemy.HasBeenCounted)
                        {
                            enemiesKilled++;
                            enemy.HasBeenCounted = true; // Count enemies only once
                        }
                        
                        // Enemies 4 and 8 drop bombs
                        if (enemiesKilled == 4 && !bombDropped1)
                        {
                            IItem bomb = ItemSpriteFactory.Instance.CreateItem("Bomb");
                            bomb.CollisionHitbox = new Rectangle(enemy.CollisionHitbox.X, enemy.CollisionHitbox.Y, 40, 40);
                            rooms[currentRoom].Items.Add(bomb);
                            DelegateManager.RaiseObjectCreated(bomb);
                            bombDropped1 = true;
                            
                        }
                        if (enemiesKilled == 8 && !bombDropped2)
                        {
                            IItem bomb = ItemSpriteFactory.Instance.CreateItem("Bomb");
                            bomb.CollisionHitbox = new Rectangle(enemy.CollisionHitbox.X, enemy.CollisionHitbox.Y, 40, 40);
                            rooms[currentRoom].Items.Add(bomb);
                            DelegateManager.RaiseObjectCreated(bomb);
                            bombDropped2 = true;
                            
                        }

                        // Enemies 5 and 9 drop keys
                        if (enemiesKilled == 5 && !keyDropped1)
                        {
                            IItem key = ItemSpriteFactory.Instance.CreateItem("Key");
                            key.CollisionHitbox = new Rectangle(enemy.CollisionHitbox.X, enemy.CollisionHitbox.Y, 40, 40);
                            rooms[currentRoom].Items.Add(key);
                            DelegateManager.RaiseObjectCreated(key);
                            if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Key_Appear");
                            keyDropped1 = true;
                        }
                        if (enemiesKilled == 9 && !keyDropped2)
                        {
                            IItem key = ItemSpriteFactory.Instance.CreateItem("Key");
                            key.CollisionHitbox = new Rectangle(enemy.CollisionHitbox.X, enemy.CollisionHitbox.Y, 40, 40);
                            rooms[currentRoom].Items.Add(key);
                            DelegateManager.RaiseObjectCreated(key);
                            keyDropped2 = true;
                        }
                    }
                }
            }
            foreach (var key in deathTimers.Keys.ToList())
            {
                if (!rooms[currentRoom].Enemies.Any(enemy => rooms[currentRoom].Enemies.IndexOf(enemy) == key))
                {
                    deathTimers.Remove(key);
                }
            }
            foreach (int removeIndex in toRemove)
            {
                rooms[currentRoom].Enemies.RemoveAt(removeIndex);
            }
            toRemove.Clear();
            foreach (int removeIndex in toRemove)
            {
                rooms[currentRoom].Enemies.RemoveAt(removeIndex);
            }
            toRemove.Clear();
            foreach (IDoor door in rooms[currentRoom].Doors)
            {
                if (door.DoorType == DoorType.Diamond)
                {
                    if (currentRoom == 9)
                    {
                        if (secretActivated)
                        {
                            door.IsOpen = true;
                        }
                    }
                    else
                    {
                        if (rooms[currentRoom].Enemies.Count == 0)
                        {
                            door.IsOpen = true;
                        }
                    }
                }
                if (door.DoorType == DoorType.Key)
                {
                    if ((currentRoom == 9  || currentRoom == 6))
                    {
                        if (door.IsOpen == true)
                        {
                            doorUnlocked = true;
                        }
                        if (doorUnlocked)
                        {
                            door.IsOpen = true;
                        }
                    }
                    
                }

                door.Update(gametime);
            }
            portalManager.Update(loadedObjects, currentRoom);
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
                    LinkManager.GetLink().UpdatePosition(new Vector2(0, 310));
                    break;
                case (CollisionDirection.Bottom):
                    currentRoomRow--;
                    LinkManager.GetLink().UpdatePosition(new Vector2(0, -310));
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

            if (currentRoom != -1)
            {
                reader = new StreamReader(ContentPath + "/LinkDungeon1 - Room" + currentRoom + ".csv");
            }

        }
    }
}