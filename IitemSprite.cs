﻿using Legend_of_the_Power_Rangers.Collision;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Legend_of_the_Power_Rangers.LinkSpritesClasses;


namespace Legend_of_the_Power_Rangers
{
    public interface IitemSprite : ICollision
    {
        LinkItemType LinkItemType { get; }
        void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch);
        bool GetState();
    }
}