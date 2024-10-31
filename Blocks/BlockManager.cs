using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Legend_of_the_Power_Rangers
{
    public class BlockManager
    {
        private List<IBlock> blocks = new List<IBlock>();
        private int currentBlockIndex = 0;
        private Texture2D blockTexture;

        public BlockManager(List<string> blockTypes)
        {
            InitializeBlocks(blockTypes);
        }

        private void InitializeBlocks(List<string> blockTypes)
        {
            foreach (var blockType in blockTypes)
            {
                blocks.Add(BlockSpriteFactory.Instance.CreateBlock(blockType));
            }
        }

        public void ChangeBlock(int direction)
        {
            currentBlockIndex += direction;

            if (currentBlockIndex >= blocks.Count)
            {
                currentBlockIndex = 0;
            }
            else if (currentBlockIndex < 0)
            {
                currentBlockIndex = blocks.Count - 1;
            }
        }

        public IBlock GetCurrentBlock()
        {
            return blocks[currentBlockIndex];
        }

        public List<IBlock> GetBlocks()
        {
            return blocks;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var block in blocks)
            {
                block.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            IBlock currentBlock = GetCurrentBlock();
            currentBlock.Draw(spriteBatch);
        }
    }
}
