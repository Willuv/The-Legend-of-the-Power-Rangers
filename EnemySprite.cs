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
        private int _frameIndex1;  // First frame index for the current direction
        private int _frameIndex2;  // Second frame index for the current direction (four rows down)
        private float _scale = 2.0f;  // Scale factor for drawing larger sprites
        private double _timeSinceLastToggle;
        private double _millisecondsPerToggle = 500; // Time to toggle between frames

        public EnemySprite(Texture2D texture, int framesCount, int spriteWidth, int spriteHeight)
        {
            _texture = texture;
            _sourceRectangles = new Rectangle[framesCount * 8]; // Enough for both sets of frames across 4 directions

            for (int direction = 0; direction < 4; direction++)
            {
                for (int i = 0; i < framesCount; i++)
                {
                    int baseIndex = direction * framesCount + i;
                    int alternateIndex = baseIndex + framesCount * 4;
                    _sourceRectangles[baseIndex] = new Rectangle(i * spriteWidth, direction * spriteHeight, spriteWidth, spriteHeight);
                    _sourceRectangles[alternateIndex] = new Rectangle(i * spriteWidth, (direction + 4) * spriteHeight, spriteWidth, spriteHeight);
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
            _frameIndex2 = _frameIndex1 + 8; // Frame from four rows down
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
