namespace Zebble
{
    using System;
    using System.Collections.Generic;

    static class ListExtensions
    {
        public static void ForEach<T>(this IList<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
}
