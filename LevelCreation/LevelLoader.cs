using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IronXL;

namespace Legend_of_the_Power_Rangers.LevelCreation
{
    public class LevelLoader
    {
        String[,] room;
        String[] doors;
        BlockSpawner blockSpawner;
        DoorMaker doorMaker;
        EnemySpawner enemySpawner;
        int numRows;
        int numColumns;
        int numDoors;
        public LevelLoader() {
            /* 
               Higher the layer depth, the more behind
               In order of ascending layer depth
               Link = Projectiles = Enemies < Items = Pushable blocks < Level/blocks
               Rooms are 12(columns)x7(rows)
            */
            numRows = 7;
            numColumns = 12;
            numDoors = 4;
            room = new String[numRows, numColumns];
            doors = new String[numDoors];

            BlockSpawner blockSpawner = new BlockSpawner();
            DoorMaker doorMaker = new DoorMaker();
            EnemySpawner enemySpawner = new EnemySpawner();
        }

        public void Load(WorkSheet levelSheet)
        {
            int rowCount = 0;
            int columnCount = 0;
            int doorCount = 0;
            // read doors
            foreach (var cell in levelSheet["A1:D1"])
            {
                doorMaker.addDoor(cell.StringValue[4]);
                doorCount++;
            }
            //reads top-bottom left-right
            foreach (var cell in levelSheet["A2:G13"])
            {
                String blockVal = cell.StringValue[..2];
                
                
                    room[rowCount, columnCount] = cell.StringValue;
                    rowCount++;
                    if (rowCount == 7)
                    {
                        rowCount = 0;
                        columnCount++;
                    }
            }
        }

    }
}
