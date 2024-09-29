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
        Vector2 offset;


        float scaleFactor = 2f;


        public BombSprite(Texture2D texture, LinkDirection direction)
		{
            bombTexture = texture;
            currentFrame = 0;
            totalFrames = 10;
            offset = Vector2.Zero;
            switch (direction)
            {
                case LinkDirection.Left:
                    offset.X = -10;
                    break;
                case LinkDirection.Right:
                    offset.X = 10;
                    break;
                case LinkDirection.Up:
                    offset.Y = -10;
                    break;
                case LinkDirection.Down:
                    offset.Y = 10;
                    break;
            }
            
			

		}
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle sourceRectangle = new Rectangle(203, 0, 9, 15);
            spriteBatch.Draw(bombTexture, position + offset, sourceRectangle, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gametime)
        {
            currentFrame++;
            if (currentFrame > totalFrames)
            {
                
            }
        }

    }

}
