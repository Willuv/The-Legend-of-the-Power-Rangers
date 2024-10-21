using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Legend_of_the_Power_Rangers.ItemSprites;

namespace Legend_of_the_Power_Rangers
{
    public class CollisionCaster
    {
        public static (ICollision, ICollision) CastObjects(ICollision object1, ICollision object2)
        {
            object1 = CastOneObject(object1);
            object2 = CastOneObject(object2);
            return (object1, object2);
        }

        public static ICollision CastOneObject(ICollision obj)
        {
            switch (obj.ObjectType)
            {
                case ObjectType.Link:
                    return (Link)obj;
                case ObjectType.Block:
                    return CastBlock((IBlock)obj);
                case ObjectType.Item:
                    return CastItem((IItem)obj);
                case ObjectType.Enemy:
                    return CastEnemy((IEnemy)obj);
                default:
                    throw new InvalidOperationException("An error occurred in casting object type.");

            }
        }

        public static IBlock CastBlock(IBlock obj)
        {
            switch (obj.BlockType)
            {
                case BlockType.BlueFloor:
                    return (BlockBlueFloor)obj;
                case BlockType.BlueGap:
                    return (BlockBlueGap)obj;
                case BlockType.BlueSand:
                    return (BlockBlueSand)obj;
                case BlockType.BombedWall:
                    return (BlockBombedWall)obj;
                case BlockType.Diamond:
                    return (BlockDiamond)obj;
                case BlockType.Fire:
                    return (BlockFire)obj;
                case BlockType.KeyHole:
                    return (BlockKeyHole)obj;
                case BlockType.Ladder:
                    return (BlockLadder)obj;
                case BlockType.OpenDoor:
                    return (BlockOpenDoor)obj;
                case BlockType.Push:
                    return (BlockPush)obj;
                case BlockType.Square:
                    return (BlockSquare)obj;
                case BlockType.Stairs:
                    return (BlockStairs)obj;
                case BlockType.Statue1:
                    return (BlockStatue1)obj;
                case BlockType.Statue2:
                    return (BlockStatue2)obj;
                case BlockType.Wall:
                    return (BlockWall)obj;
                case BlockType.WhiteBrick:
                    return (BlockWhiteBrick)obj;
                default:
                    throw new Exception("Not an implemented block");
            }
        }

        public static IItem CastItem(IItem obj)
        {
            switch (obj.ItemType)
            {
                case (ItemType.BlueCandle):
                    return (ItemBlueCandle)obj;
                case (ItemType.BluePotion):
                    return (ItemBluePotion)obj;
                case (ItemType.Bomb):
                    return (ItemBomb)obj;
                case (ItemType.Clock):
                    return (ItemClock)obj;
                case (ItemType.Compass):
                    return (ItemCompass)obj;
                case (ItemType.Fairy):
                    return (ItemFairy)obj;
                case (ItemType.Heart):
                    return (ItemHeart)obj;
                case (ItemType.HeartContainer):
                    return (ItemHeartContainer)obj;
                case (ItemType.Key):
                    return (ItemKey)obj;
                case (ItemType.Map):
                    return (ItemMap)obj;
                case (ItemType.Rupee):
                    return (ItemRupee)obj;
                case (ItemType.Triforce):
                    return (ItemTriforce)obj;
                case (ItemType.WoodBoomerang):
                    return (ItemWoodBoomerang)obj;
                default:
                    throw new Exception("Not an implemented item");
            }
        }

        public static IEnemy CastEnemy(IEnemy obj)
        {
            switch (obj.EnemyType)
            {
                case EnemyType.BatKeese:
                    return (BatKeese)obj;
                case EnemyType.BlueCentaur:
                    return (BlueCentaur)obj;
                case EnemyType.BlueGorya:
                    return (BlueGorya)obj;
                case EnemyType.BlueKnight:
                    return (BlueKnight)obj;
                case EnemyType.BlueOcto:
                    return (BlueOcto)obj;
                case EnemyType.DarkMoblin:
                    return (DarkMoblin)obj;
                case EnemyType.DragonBoss:
                    return (DragonBoss)obj;
                case EnemyType.DragonProjectile:
                    return (DragonProjectile)obj;
                case EnemyType.GelBigGray:
                    return (GelBigGray)obj;
                case EnemyType.GelBigGreen:
                    return (GelBigGreen)obj;
                case EnemyType.GelSmallBlack:
                    return (GelSmallBlack)obj;
                case EnemyType.GelSmallTeal:
                    return (GelSmallTeal)obj;
                case EnemyType.RedCentaur:
                    return (RedCentaur)obj;
                case EnemyType.RedGorya:
                    return (RedGorya)obj;
                case EnemyType.RedKnight:
                    return (RedKnight)obj;
                case EnemyType.RedMoblin:
                    return (RedMoblin)obj;
                case EnemyType.RedOcto:
                    return (RedOcto)obj;
                case EnemyType.Skeleton:
                    return (Skeleton)obj;
                //case EnemyType.Trap:
                //  return (Trap)obj; //out until alex implements traps
                case EnemyType.WallMaster:
                    return (WallMaster)obj;
                default:
                    throw new Exception("Not an implemented enemy");
            }
        }
    }
}
