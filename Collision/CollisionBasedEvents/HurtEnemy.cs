using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    public class HurtEnemy : IEvent
    {
        public HurtEnemy() { }

        public void Execute(ICollision linkItem, ICollision enemy, CollisionDirection direction)
        {
            if (enemy is IEnemy enemy1)
            {
                if (!enemy1.IsHurt())
                {
                    enemy1.TakeDamage(1);

                    if (!AudioManager.Instance.IsMuted())
                    {
                        string sound = enemy1.EnemyType != EnemyType.DragonBoss ? "Enemy_Hit" : "Boss_Hit";
                        AudioManager.Instance.PlaySound(sound);
                    }

                    Debug.WriteLine("Enemy hurt");
                }
            }
        }
    }
}
