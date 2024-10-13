using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision
{
    public class KeyGenerator
    {
        public static string Generate(ObjectType type1, ObjectType type2, CollisionDirection direction)
        {
            return $"{type1}-{type2}-{direction}";
        }
        public static string Generate(ObjectType type1, BlockType type2, CollisionDirection direction)
        {
            return $"{"type1"}-{"type2"}-{"direction"}";
        }
        //public static string GenerateKey(ObjectType type1, BlockType type2, CollisionDirection direction)
        //{
        //    return $"{"type1"}-{"type2"}-{"direction"}";
        //}
        //add once items and enemies are implemented
    }
}
