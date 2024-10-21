﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Linq;

namespace Legend_of_the_Power_Rangers.LevelCreation
{
    public class Level
    {
        LevelLoader loader;
        System.Data.DataSet dungeonBook;
        Texture2D levelSpriteSheet;
        Rectangle wallsSource;
        Rectangle wallsDestination;
        int numRooms;
        int currentRoom;
        int loadedRoom;
        int scaleFactor = 4;
        public Level(Texture2D levelSpriteSheet, System.Data.DataSet dungeonBook)
        {
            this.dungeonBook = dungeonBook;
            this.levelSpriteSheet = levelSpriteSheet;
            wallsSource = new Rectangle(0, 0, 255, 175);
            wallsDestination = new Rectangle(15, 15, 255 * scaleFactor, 201 * scaleFactor);
            loader = new LevelLoader(levelSpriteSheet);
            numRooms = dungeonBook.Tables.Count;
            currentRoom = 0;
            loadedRoom = 0;
            loader.Load(dungeonBook.Tables[currentRoom]);
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
            foreach (Enemy enemy in loader.Enemies)
            {
                enemy.Draw(enemySpritesheet, spriteBatch);
            }
        }

        public void Update(GameTime gametime) 
        {
            
            if (currentRoom != loadedRoom)
            {
                loader.Load(dungeonBook.Tables[currentRoom]);
                
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
            foreach (Enemy enemy in loader.Enemies)
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

        }
    }
}