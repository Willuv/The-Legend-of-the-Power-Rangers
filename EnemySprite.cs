using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers
{
    public class EnemySprite : ISprite
    {
        private Texture2D _texture;
        private Rectangle[] _sourceRectangles;
        private int _currentFrameIndex;  // Current frame for drawing
        private int _frameIndex1;  // First frame
        private int _frameIndex2;  // Second frame
        private float _scale = 2.0f;  // Scale factor for drawing larger sprites
        private double _timeSinceLastToggle;
        private double _millisecondsPerToggle = 200; // Time to toggle between frames

        public EnemySprite(Texture2D texture, int framesCount, int spriteWidth, int spriteHeight, int xOffset = 0, int yOffset = 0)
{
    _texture = texture;
    _sourceRectangles = new Rectangle[framesCount * 8]; // Adjust according to your actual layout needs

    for (int direction = 0; direction < 4; direction++) // Assuming 4 directions
    {
        for (int i = 0; i < framesCount; i++)
        {
            int baseIndex = direction * framesCount + i;
            int alternateIndex = baseIndex + framesCount * 4; // Adjust this if the layout is different

            _sourceRectangles[baseIndex] = new Rectangle(
                xOffset + i * spriteWidth,
                yOffset + direction * spriteHeight,
                spriteWidth,
                spriteHeight);

            _sourceRectangles[alternateIndex] = new Rectangle(
                xOffset + i * spriteWidth,
                yOffset + (direction + 4) * spriteHeight,
                spriteWidth,
                spriteHeight);
        }
    }
}


        public void SetDirection(Vector2 direction)
        {
            // Determine direction index (0 = Down, 1 = Left, 2 = Up, 3 = Right)
            int directionIndex = 0; // Down by default
            if (direction.X < 0) directionIndex = 1; // Left
            else if (direction.Y < 0) directionIndex = 2; // Up
            else if (direction.X > 0) directionIndex = 3; // Right

            // Update frame indices for the new direction
            _frameIndex1 = directionIndex * 2;
            _frameIndex2 = _frameIndex1 + 16; // Magic number that works
            _currentFrameIndex = _frameIndex1; // Reset to first frame on direction change
        }

        public void Update(GameTime gameTime)
        {
            _timeSinceLastToggle += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeSinceLastToggle >= _millisecondsPerToggle)
            {
                // Toggle between the two frames
                if (_currentFrameIndex == _frameIndex1)
                    _currentFrameIndex = _frameIndex2;
                else
                    _currentFrameIndex = _frameIndex1;

                _timeSinceLastToggle = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(_texture, location, _sourceRectangles[_currentFrameIndex], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }
}
