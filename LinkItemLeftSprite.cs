﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkItemLeftSprite : ILinkSprite
    {
        private Texture2D linkTexture;
        private int currentFrame;
        private int totalFrames;
        private int nextSpriteDistance;
        private int currentLinkLocation;
        private float scaleFactor = 3f;
        public LinkItemLeftSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 10;
            currentLinkLocation = 58;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(28, currentLinkLocation, 17, 17);
            spriteBatch.Draw(linkTexture, position, sourceRectangle, color, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);

        }
        public void Update(GameTime gameTime)
        {

        }
    }
}