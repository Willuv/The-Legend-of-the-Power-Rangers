using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class KeyGenerator
    {
        public static (int, int, CollisionDirection) Generate(ICollision obj1, ICollision obj2, CollisionDirection direction)
        {   
            int type1 = GetObjectTypeKey(obj1);
            int type2 = GetObjectTypeKey(obj2);

            if (type1 > type2)
            {
               //ensuring consistent order
                return (type2, type1, direction);
            }

            return (type1, type2, direction);
        }

        private static int GetObjectTypeKey(ICollision obj)
        {
            int blockOffset = 1000; // Any large value to avoid enum overlap
            int itemOffset = 2000;
            int enemyOffset = 3000;
            int linkProjectileOffset = 4000;
            int enemyProjectileOffset = 5000;

            if (obj is IBlock block)
                return (int)block.BlockType + blockOffset;
            if (obj is IItem item)
                return (int)item.ItemType + itemOffset;
            if (obj is IEnemy enemy)
                return (int)enemy.EnemyType + enemyOffset;
            if (obj is ILinkItemSprite linkItem)
                return (int)linkItem.LinkProjectileType + linkProjectileOffset;
            if (obj is IEnemyProjectile enemyProjectile)
                return (int)enemyProjectile.EnemyProjectileType + enemyProjectileOffset;

            return obj.GetHashCode(); //just in case
        }
    }
}
