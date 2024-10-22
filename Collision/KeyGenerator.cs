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

            return (type1, type2, direction);
        }

        private static int GetObjectTypeKey(ICollision obj)
        {
            if (obj is IBlock block)
                return block.GetHashCode();
            if (obj is IItem item)
                return item.GetHashCode();
            if (obj is IEnemy enemy)
                return enemy.GetHashCode();

            return obj.GetHashCode(); //just in case
        }
    }
}
