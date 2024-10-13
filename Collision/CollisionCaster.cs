using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Legend_of_the_Power_Rangers.Collision
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
                //case ObjectType.Item:
                //  return CastItem((IItem)obj);
                //case ObjectType.Enemy:
                //  return CastEnemy((ISprite)obj);
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
                //add more
                default:
                    throw new InvalidOperationException("An error occurred in casting block type.");
            }
        }
    }
}
