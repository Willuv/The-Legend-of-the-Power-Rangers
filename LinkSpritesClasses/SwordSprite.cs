using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class SwordSprite : IitemSprite
    {
        private Texture2D swordTexture;
        int totalFrames;
        int currentFrame;
        bool secondStage;
        bool finished;
        Vector2 offset;
        Vector2 movement1;
        Vector2 movement2;
        Vector2 movement3;
        Vector2 movement4;
        Vector2 position;
        Vector2 position1;
        Vector2 position2;
        Vector2 position3;
        Vector2 position4;
        Vector2 movement;
        Rectangle sourceRectangle;
        Rectangle sourceRectangle1;
        Rectangle sourceRectangle2;
        Rectangle sourceRectangle3;
        Rectangle sourceRectangle4;
        float scaleFactor = 3f;


        public SwordSprite(Texture2D texture, Vector2 position, LinkDirection direction)
        {
            swordTexture = texture;
            secondStage = false;
            finished = false;
            currentFrame = 0;
            totalFrames = 100;
            offset = Vector2.Zero;
            movement1 = new Vector2(-1, -1);
            movement2 = new Vector2(1, -1);
            movement3 = new Vector2(-1, 1);
            movement4 = new Vector2(1, 1);
            sourceRectangle1 = new Rectangle(179, 90, 7, 9);
            sourceRectangle2 = new Rectangle(188, 90, 7, 9);
            sourceRectangle3 = new Rectangle(179, 101, 7, 9);
            sourceRectangle4 = new Rectangle(188, 101, 7, 9);
            this.position = position;
            switch (direction)
            {
                case LinkDirection.Left:
                    sourceRectangle = new Rectangle(30 , 7, 16, 7);
                    offset = new Vector2(-50, 15);
                    movement.X = -3;
                    break;
                case LinkDirection.Right:
                    sourceRectangle = new Rectangle(90, 7, 16, 7);
                    offset = new Vector2(50, 15);
                    movement.X = 3;
                    break;
                case LinkDirection.Up:
                    sourceRectangle = new Rectangle(64, 3, 7, 16);
                    offset = new Vector2(15, -50);
                    movement.Y = -3;
                    break;
                case LinkDirection.Down:
                    sourceRectangle = new Rectangle(4, 3, 7, 16);
                    offset = new Vector2(15, 50);
                    movement.Y = -3;
                    break;
            }



        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!secondStage)
            {
                spriteBatch.Draw(swordTexture, position + offset, sourceRectangle, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(swordTexture, position1 + offset, sourceRectangle1, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
                spriteBatch.Draw(swordTexture, position2 + offset, sourceRectangle2, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
                spriteBatch.Draw(swordTexture, position3 + offset, sourceRectangle3, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
                spriteBatch.Draw(swordTexture, position4 + offset, sourceRectangle4, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
            }
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame < 30)
            {
                position += movement;
            }
            else if (currentFrame == 30)
            {
                secondStage = true;
                position1 = position;
                position2 = position;
                position3 = position;
                position4 = position;
            } else if (currentFrame <= totalFrames)
            {
                position1 += movement1;
                position2 += movement2;
                position3 += movement3;
                position4 += movement4;
            }
            else
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
