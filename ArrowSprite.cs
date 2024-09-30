using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
    public class ArrowSprite : IitemSprite
    {
        private Texture2D arrowTexture;
        int totalFrames;
        int currentFrame;
        bool finished;
        Vector2 offset;
        Vector2 position;
        Vector2 movement;
        Rectangle sourceRectangle;
        float scaleFactor = 3f;


        public ArrowSprite(Texture2D texture, Vector2 position, LinkDirection direction)
        {
            arrowTexture = texture;
            finished = false;
            currentFrame = 0;
            totalFrames = 40;
            offset = Vector2.Zero;
            this.position = position;
            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Vector2(-50, 15);
                    movement.X = -4;
                    sourceRectangle = new Rectangle(150, 8, 15, 5);
                    break;
                case LinkDirection.Right:
                    offset = new Vector2(50, 15);
                    movement.X = 4;
                    sourceRectangle = new Rectangle(210, 8, 15, 5);
                    break;
                case LinkDirection.Up:
                    offset = new Vector2(15, -50);
                    movement.Y = -4;
                    sourceRectangle = new Rectangle(185, 3, 5, 15);
                    break;
                case LinkDirection.Down:
                    offset = new Vector2(15, 50);
                    movement.Y = 4;
                    sourceRectangle = new Rectangle(125, 3, 5, 15);
                    break;
            }



        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(arrowTexture, position + offset, sourceRectangle, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            position += movement;
            if (currentFrame > totalFrames)
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
