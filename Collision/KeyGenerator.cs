using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    public class KeyGenerator
    {
        public static string Generate(ICollision obj1, ICollision obj2, CollisionDirection direction)
        {
            string type1 = GetObjectTypeKey(obj1);
            string type2 = GetObjectTypeKey(obj2);

            return $"{type1}{type2}{direction}";
        }

        private static string GetObjectTypeKey(ICollision obj)
        {
            if (obj is IBlock block)
                return block.BlockType.ToString();
            if (obj is IItem item)
                return $"Type:{item.ItemType}";
            if (obj is IEnemy enemy)
            return $"Type:{enemy.EnemyType}";

            return obj.ObjectType.ToString(); //just in case
        }
    }
}
