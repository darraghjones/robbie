using System;
using System.Collections.Generic;
using System.Text;

namespace Lib
{
    public static class ExtensionMethods
    {
        public static void Times(this int count, Action<int> action)
        {
            for (int i = 0; i < count; i++)
            {
                action(i); 
            }
        }

        public static void Times(this int count, Action action)
        {
            Times(count, _ => action());
        }

        public static IEnumerable<T> Times<T>(this int count, Func<int, T> func)
        {
            for (int i = 0; i < count; i++)
            {
                yield return func(i);
            }
        }

        public static IEnumerable<T> Times<T>(this int count, Func<T> func)
        {
            return Times<T>(count, _ => func());
        }
    }
}
