using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class HurtEnemy : IEvent
    {
        public HurtEnemy() { }

        public void Execute(ICollision linkItem, ICollision enemy, CollisionDirection direction)
        {
            if (enemy is IEnemy)
            {
                //enemy.GetHurt(); for alex to implement
                Debug.WriteLine("Enemy hurt");
            }
        }
    }
}
