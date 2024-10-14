using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
    {
        public class EnemySprite : ISprite // Used for DragonBoss Projectiles
{
    private Texture2D texture;
    private Rectangle sourceRectangle;
    public Rectangle destinationRectangle;
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    private float speed = 200f; // Speed
    private float scale = 2.0f; // Scale

    public EnemySprite(Texture2D texture, Rectangle sourceRectangle)
    {
        this.texture = texture;
        this.sourceRectangle = sourceRectangle;
        this.scale = scale;
        UpdateDestinationRectangle();
    }

    public void Update(GameTime gameTime)
    {
        Position += Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        UpdateDestinationRectangle();

    }
    private void UpdateDestinationRectangle()
    {
        int width = (int)(sourceRectangle.Width * scale);
        int height = (int)(sourceRectangle.Height * scale);
        destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);
    }

    public void Draw(Texture2D texture, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);;
    }
}
}
