using System;
using System.Diagnostics.SymbolStore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Legend_of_the_Power_Rangers
{
    public class BlockPush : IBlock
    {
        private Rectangle sourceRectangle = new Rectangle(144, 0, 16, 16);
        private Rectangle destinationRectangle = new Rectangle(450, 340, 48, 48);

        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        public ObjectType ObjectType { get { return ObjectType.Block; } }
        public BlockType BlockType { get { return BlockType.Push; } }
        public bool IsPushable { get; set; }
        public CollisionDirection PushableDirection { get; set; }
        private bool IsMoving { get; set; }
        private Vector2 targetPosition = new();
        private const int tileSize = 64;
        private const float movementSpeed = 2f;

        public BlockPush()
        {
            IsPushable = false;
            PushableDirection = CollisionDirection.Left;
            IsMoving = false;
        }

        public void Push()
        {
            IsPushable = false;
            switch (PushableDirection)
            {
                case CollisionDirection.Left:
                    targetPosition = new Vector2(destinationRectangle.X + tileSize, destinationRectangle.Y);
                    break;
                case CollisionDirection.Top:
                    targetPosition = new Vector2(destinationRectangle.X, destinationRectangle.Y + tileSize);
                    break;
                case CollisionDirection.Right:
                    targetPosition = new Vector2(destinationRectangle.X - tileSize, destinationRectangle.Y);
                    break;
                case CollisionDirection.Bottom:
                    targetPosition = new Vector2(destinationRectangle.X, destinationRectangle.Y - tileSize);
                    break;
            }

            IsMoving = true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsMoving)
            {
                Vector2 currentPos = new(destinationRectangle.X, destinationRectangle.Y);
                Vector2 direction = Vector2.Normalize(targetPosition - currentPos);
                Vector2 newPos = currentPos + direction * movementSpeed;

                destinationRectangle.X = (int)newPos.X;
                destinationRectangle.Y = (int)newPos.Y;

                if (Vector2.Distance(newPos, targetPosition) <= movementSpeed)
                {
                    destinationRectangle.X = (int)targetPosition.X;
                    destinationRectangle.Y = (int)targetPosition.Y;
                    IsMoving = false;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlockSpriteFactory.Instance.GetBlockSpritesheet(), destinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);
        }
    }
}