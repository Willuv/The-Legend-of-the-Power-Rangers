﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class LinkItemRightSprite : ILinkSprite
    {
        private Texture2D linkTexture;
        private int currentFrame;
        private int totalFrames;
        private int currentLinkLocation;
        public Rectangle SourceRectangle { get; private set; }


        public LinkItemRightSprite(Texture2D texture)
        {
            linkTexture = texture;
            currentFrame = 0;
            totalFrames = 10;
            currentLinkLocation = 58;
            SourceRectangle = new Rectangle(87, currentLinkLocation, 17, 17);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            spriteBatch.Draw(linkTexture, destinationRectangle, SourceRectangle, color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
        }
        public void Update(GameTime gameTime)
        {

        }
    }
}