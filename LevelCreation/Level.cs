using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Legend_of_the_Power_Rangers.LevelCreation
{
    public class Level
    {
        LevelLoader loader;
        Texture2D levelSpriteSheet;
        Rectangle wallsSource;
        Rectangle wallsDestination;
        int numRooms;
        int currentRoom;
        int loadedRoom;
        int scaleFactor = 5;
        private StreamReader reader;
        public Level(Texture2D levelSpriteSheet, StreamReader reader)
        {
            this.reader = reader;
            this.levelSpriteSheet = levelSpriteSheet;
            wallsSource = new Rectangle(0, 0, 255, 175);
            wallsDestination = new Rectangle(5, 5, 255 * scaleFactor, 175 * scaleFactor);
            loader = new LevelLoader(levelSpriteSheet);
            numRooms = 19;
            currentRoom = 0;
            loadedRoom = 0;
            loader.Load(reader);
        }
        public void Draw(Texture2D enemySpritesheet, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(levelSpriteSheet, wallsDestination, wallsSource, Color.White);
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
            reader = new StreamReader(@"Content\LinkDungeon1 - Room" + currentRoom + ".csv");
        }
    }
}
