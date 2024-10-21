 using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class CandleSprite : IitemSprite
    {
        private Texture2D candleTexture;
        int totalFrames;
        int currentFrame;
        bool finished;
        Vector2 offset;
        Vector2 position;
        Vector2 movement;
        Rectangle currentRectangle;
        Rectangle sourceRectangle1;
        Rectangle sourceRectangle2;

        float scaleFactor = 3f;


        public CandleSprite(Texture2D texture, Vector2 position, LinkDirection direction)
        {
            candleTexture = texture;
            finished = false;
            currentFrame = 0;
            totalFrames = 150;
            offset = Vector2.Zero;
            sourceRectangle1 = new Rectangle(160, 32, 15, 15);
            sourceRectangle2 = new Rectangle(176, 32, 15, 15);
            currentRectangle = sourceRectangle1;
            this.position = position;
            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Vector2(-50, 5);
                    offset.X = -50;
                    movement.X = -2;
                    break;
                case LinkDirection.Right:
                    offset = new Vector2(50, 5);
                    offset.X = 50;
                    movement.X = 2;
                    break;
                case LinkDirection.Up:
                    offset.Y = -50;
                    movement.Y = -2;
                    break;
                case LinkDirection.Down:
                    offset = new Vector2(5, 50);
                    movement.Y = 2;
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(candleTexture, position + offset, currentRectangle, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame % 7 == 0)
            {
                if (currentRectangle == sourceRectangle1)
                {
                    currentRectangle = sourceRectangle2;
                } else
                {
                    currentRectangle = sourceRectangle1;
                }
            }
            if (currentFrame < 40)
            {
                offset += movement;
            } else if (currentFrame > totalFrames)
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
