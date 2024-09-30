using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers
{
	public class BombSprite : IitemSprite
	{
		private Texture2D bombTexture;
        int totalFrames;
        int currentFrame;
        bool finished;
        Rectangle usedRectangle;
        Rectangle sourceRectangle1;
        Vector2 offset;
        Vector2 offset2;
        Vector2 position;
        float scaleFactor = 3f;
        public BombSprite(Texture2D texture, Vector2 position, LinkDirection direction)
		{
            bombTexture = texture;
            finished = false;
            currentFrame = 0;
            totalFrames = 70;
            this.position = position;
            this.usedRectangle = new Rectangle(203, 0, 9, 15);
            this.sourceRectangle1 = new Rectangle(84, 119, 7, 6);
            switch (direction)
            {
                case LinkDirection.Left:
                    offset = new Vector2(-30, 0);
                    offset2 = new Vector2(-60, -20);
                    break;
                case LinkDirection.Right:
                    offset = new Vector2(50, 0);
                    offset2 = new Vector2(20, -20);
                    break;
                case LinkDirection.Up:
                    offset = new Vector2(5, -50);
                    offset2 = new Vector2(-25, -70);
                    break;
                case LinkDirection.Down:
                    offset = new Vector2(5, 50);
                    offset2 = new Vector2(-25, 25);
                    break;
            }
            
			

		}
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bombTexture, position + offset, usedRectangle, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame == 50) {
                scaleFactor = 12f;
                offset = offset2;
                usedRectangle = sourceRectangle1;
            }
            else if (currentFrame == totalFrames)
            {
                finished = true;
            }
        }
        public bool GetState() {
            return finished;
        }

    }

}
