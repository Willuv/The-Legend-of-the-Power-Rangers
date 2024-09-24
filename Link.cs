﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class Link
    {
        private Texture2D linkSpriteSheet;
        private LinkStateMachine stateMachine;
        private Vector2 position;

        public Link(Texture2D spriteSheet)
        {
            linkSpriteSheet = spriteSheet;
            stateMachine = new LinkStateMachine(spriteSheet);
            position = new Vector2(200, 200);
        }

        public void Update(GameTime gameTime)
        {
            ISprite currentSprite = stateMachine.GetCurrentSprite();
            currentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ISprite currentSprite = stateMachine.GetCurrentSprite();
            currentSprite.Draw(spriteBatch, position);
        }
    }
}
