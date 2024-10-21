using IronXL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.LevelCreation
{
    public class Level
    {
        LevelLoader loader;
        WorkBook dungeonBook;
        int numRooms;
        int currentRoom;
        int loadedRoom;
        String[,] room;
        String[] doors;
        public Level(Texture2D levelSpriteSheet, WorkBook dungeonBook)
        {
            this.dungeonBook = dungeonBook;
            loader = new LevelLoader();
            numRooms = dungeonBook.WorkSheets.Count();
            currentRoom = 0;
            loadedRoom = 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
        }  

        public void Update(GameTime gametime) 
        {
            if (currentRoom != loadedRoom)
            {
                loader.Load(dungeonBook.WorkSheets[currentRoom]);
            }
        }
        public void ChangeLevel(int direction)
        {
            currentRoom += direction;
            if (currentRoom >= numRooms)
            {
                currentRoom = 0;
            }
            if (currentRoom < 0)
            {
                currentRoom = numRooms - 1;
            }

        }
    }
}
