using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Legend_of_the_Power_Rangers
{
	public interface IitemSprite
	{
			void Update(GameTime gametime);
			void Draw(SpriteBatch spriteBatch);
			bool GetState();
	}
}