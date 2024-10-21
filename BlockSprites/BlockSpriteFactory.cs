using Microsoft.Xna.Framework.Graphics;
using System;

namespace Legend_of_the_Power_Rangers
{
    public class BlockSpriteFactory
    {
        private static BlockSpriteFactory instance = new BlockSpriteFactory();
        private Texture2D blockSpritesheet;

        public static BlockSpriteFactory Instance
        {
            get { return instance; }
        }

        private BlockSpriteFactory() { }

        public void SetBlockSpritesheet(Texture2D spritesheet)
        {
            blockSpritesheet = spritesheet;
        }

        public IBlock CreateBlock(string blockType)
        {
            switch (blockType)
            {
                case "Statue1":
                    return new BlockStatue1();
                case "Statue2":
                    return new BlockStatue2();
                case "Square":
                    return new BlockSquare();
                case "Push":
                    return new BlockPush();
                case "Fire":
                    return new BlockFire();
                case "BlueGap":
                    return new BlockBlueGap();
                case "Stairs":
                    return new BlockStairs();
                case "WhiteBrick":
                    return new BlockWhiteBrick();
                case "Ladder":
                    return new BlockLadder();
                case "BlueFloor":
                    return new BlockBlueFloor();
                case "BlueSand":
                    return new BlockBlueSand();
                case "BombedWall":
                    return new BlockBombedWall();
                case "Diamond":
                    return new BlockDiamond();
                case "KeyHole":
                    return new BlockKeyHole();
                case "OpenDoor":
                    return new BlockOpenDoor();
                case "Wall":
                    return new BlockWall();
                default:
                    throw new ArgumentException($"Block type {blockType} not recognized");
            }
        }

        public Texture2D GetBlockSpritesheet() => blockSpritesheet;
    }
}
