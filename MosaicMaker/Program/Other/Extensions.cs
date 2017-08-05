using System;
using System.Collections.Generic;

namespace MosaicMakerNS
{
    public static class Extensions
    {
        public static int FindIndexOfSmallestElement<T>(this List<T> list)
            where T : IComparable<T>
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (list.Count == 0)
                throw new InvalidOperationException("The list is empty!");

            T smallest = list[0];
            int index = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CompareTo(smallest) < 0)
                {
                    smallest = list[i];
                    index = i;
                }
            }

            return index;
        }

        public static int FindIndexOfBiggestElement<T>(this List<T> list)
            where T : IComparable<T>
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (list.Count == 0)
                throw new InvalidOperationException("The list is empty!");

            T biggest = list[0];
            int index = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CompareTo(biggest) > 0)
                {
                    biggest = list[i];
                    index = i;
                }
            }

            return index;
        }
    }
}