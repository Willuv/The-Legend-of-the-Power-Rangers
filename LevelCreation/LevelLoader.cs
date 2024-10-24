﻿using Microsoft.ApplicationInsights;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Legend_of_the_Power_Rangers.LevelCreation
{
    public class LevelLoader
    {
        List<IDoor> doors;
        public List<IDoor> Doors
        {
            get { return doors; }
        }
        List<IEnemy> enemies;
        public List<IEnemy> Enemies
        {
            get { return enemies; }
        }
        List<IBlock> blocks;
        public List<IBlock> Blocks
        {
            get { return blocks; }
        }
        private Dictionary<String, String> BlockDictionary = new Dictionary<string, string>
        {
            { "01", "Statue1"},
            { "02", "Statue2"},
            { "03", "Square"},
            { "04", "Push"},
            { "05", "Fire"},
            { "06", "BlueGap"},
            { "07", "Stairs"},
            { "08", "WhiteBrick"},
            { "09", "Ladder"},
            { "10", "BlueFloor"},
            { "11", "BlueSand"},
            { "12", "Wall"},
        };
        List<IItem> items;
        public List<IItem> Items
        {
            get { return items; }
        }
        private Dictionary<String, String> ItemDictionary = new Dictionary<string, string>
        {
            { "01", "Compass"},
            { "02", "Map"},
            { "03", "Key"},
            { "04", "HeartContainer"},
            { "05", "Triforce"},
            { "06", "WoodBoomerang"},
            { "07", "Bow"},
            { "08", "Heart"},
            { "09", "Rupee"},
            { "10", "Bomb"},
            { "11", "Fairy"},
            { "12", "Clock"},
            { "13", "BlueCandle"},
            { "14", "BluePotion"},
        };
        DoorMaker doorMaker;
        EnemySpawner enemySpawner;

        Texture2D texture;
        int numRows;
        int numColumns;
        int numDoors;
        public LevelLoader(Texture2D levelSpriteSheet) {
            /* 
               Higher the layer depth, the more behind
               In order of ascending layer depth
               Link = Projectiles = Enemies < Items = Pushable blocks < Level/blocks
               Rooms are 12(columns)x7(rows)
            */
            numRows = 7;
            numColumns = 12;
            numDoors = 4;

            this.texture = levelSpriteSheet;

            blocks = new List<IBlock>();
            doors = new List<IDoor>();
            items = new List<IItem>();
            enemies = new List<IEnemy>();
            doorMaker = new DoorMaker(levelSpriteSheet);
            enemySpawner = new EnemySpawner();
        }
        public void DeloadRoom()
        {
            enemies.Clear();
            blocks.Clear();
            doors.Clear();
            items.Clear();
        }
        public void Load(StreamReader reader)
        {
            String line;
            String[] splitLine;
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //unload past room
            doors.Clear();



            // read doors
            
            line = reader.ReadLine();
            splitLine = CSVParser.Split(line);
            for (int i = 0; i < 4; i++)
            {
                if (doorMaker != null)
                {
                    doors.Add(doorMaker.CreateDoor((splitLine[i])[4], i));
                }
            }
            //reads top-bottom left-right
            for (int i = 1; i < 8; i++)
            {
                line = reader.ReadLine();
                for (int j = 0; j < 12; j++)
                {
                    splitLine = CSVParser.Split(line);
                    int currentx = 165 + (80 * j);
                    int currenty = 162 + (81 * (i-1));
                    String tileCode = splitLine[j];
                    //Debug.WriteLine(tileCode);
                    String blockOneCode = tileCode.Substring(1, 2);
                    String enemyCode = tileCode.Substring(7, 2);
                    String itemCode = tileCode.Substring(10, 2);
                    

                    if (blockOneCode != "99")
                    {
                        String blockOneString = BlockDictionary[blockOneCode];
                        IBlock block = BlockSpriteFactory.Instance.CreateBlock(blockOneString);
                        block.DestinationRectangle = new Rectangle(currentx, currenty, 80, 81);
                        blocks.Add(block);
                    }
                    if (enemyCode != "99")
                    {
                        IEnemy enemy = EnemySpriteFactory.Instance.CreateEnemy(enemyCode);
                        enemy.DestinationRectangle = new Rectangle(currentx, currenty, 50, 50);
                        enemies.Add(enemy);
                    }
                    if (itemCode != "99")
                    {
                        String itemString = ItemDictionary[itemCode];
                        IItem item = ItemSpriteFactory.Instance.CreateItem(itemString);
                        item.DestinationRectangle = new Rectangle(currentx + 20, currenty + 20, 50, 50);
                        items.Add(item);
                    }
                        
                }
            }
        }
    }
}
