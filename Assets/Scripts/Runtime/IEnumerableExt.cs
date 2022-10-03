using System;
using System.Collections.Generic;
using System.Linq;

namespace GatherCraftDefend
{

    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExt
    {

        public static void Iter<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }

        public static void IterI<T>(this IEnumerable<T> items, Action<int, T> action)
        {
            var arr = items.ToArray();
            for (var i = 0; i < arr.Length; i++) action(i, arr[i]);
        }

    }

}