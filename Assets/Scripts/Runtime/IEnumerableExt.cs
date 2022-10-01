using System;
using System.Collections.Generic;

namespace GatherCraftDefend
{

    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExt
    {

        public static void Iter<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }

    }

}