using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class BoomerangSprite : IitemSprite
    {
        private Texture2D boomerangTexture;
        int totalFrames;
        int currentFrame;
        bool finished;
        Rectangle sourceRectangle;
        Vector2 center;
        Vector2 movement;
        Vector2 offset;
        Vector2 position;

        float rotation;
        float scaleFactor = 3f;


        public BoomerangSprite(Texture2D texture, Vector2 position, LinkDirection direction)
        {
            boomerangTexture = texture;
            finished = false;
            currentFrame = 0;
            totalFrames = 100;
            this.position = position;
            this.sourceRectangle = new Rectangle(285, 4, 4, 7);
            this.center = new Vector2(2f, 3.5f);
            offset = Vector2.Zero;
            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Vector2(0, 20);
                    movement.X = -3;
                    break;
                case LinkDirection.Right:
                    offset = new Vector2(50, 20);
                    movement.X = 3;
                    break;
                case LinkDirection.Up:
                    offset = new Vector2(20, -15);
                    movement.Y = -3;
                    break;
                case LinkDirection.Down:
                    offset = new Vector2(25, 60);
                    movement.Y = 3;
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boomerangTexture, position + offset, sourceRectangle, Color.White, rotation, center, scaleFactor, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame % 10 == 0) {
                rotation += currentFrame;
                rotation %= MathHelper.Pi * 2;
            }
            if (currentFrame < 50)
            {
                offset += movement;
            }
            else
            {
                 offset -= movement;
            }
            if (currentFrame == totalFrames)
            {
                finished = true;
            }
        }
        public bool GetState()
        {
            return finished;
        }

    }

}
