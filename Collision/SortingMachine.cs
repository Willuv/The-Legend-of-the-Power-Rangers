using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision
{
    /*
     * SortingMachine is a class dedicating to sorting collidable
     * based on their X position, left to right.
     */
    public class SortingMachine
    {   
        public static List<ICollision> QuickSort(List<ICollision> list)
        {
            if (list.Count < 2) return list;

            int pivotIndex = list.Count / 2;
            int xOfPivot = list[pivotIndex].DestinationRectangle.X;
            List<ICollision> left = new List<ICollision>();
            List<ICollision> right = new List<ICollision>();

            for (int i = 0; i < list.Count; i++)
            {
                if (i == pivotIndex) continue;

                if (list[i].DestinationRectangle.X < xOfPivot)
                {
                    left.Add(list[i]);
                }
                else
                {
                    right.Add(list[i]);
                }
            }

            List<ICollision> sortedList = QuickSort(left);
            sortedList.Add(list[pivotIndex]);
            sortedList.AddRange(QuickSort(right));

            return sortedList;
        }

        public static void BubbleSort(List<Object> list)
        {

        }
    }
}
