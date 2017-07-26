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
                throw new NullReferenceException("The list is null!");

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
    }
}