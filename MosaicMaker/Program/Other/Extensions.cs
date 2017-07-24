using System;
using System.Collections.Generic;

namespace MosaicMaker
{
    public static class Extensions
    {
        /// <summary>
        /// Default: -1
        /// </summary>
        public static int FindIndexOfSmallestElement<T>(this List<T> list)
            where T : IComparable<T>
        {
            if (list == null || list.Count == 0)
                return -1;

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