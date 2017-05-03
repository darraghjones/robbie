using System;
using System.Threading;

namespace Lib.Domain
{public static class Rand
    {
        private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

        public static int Next(int maxValue)
        {
            return random.Value.Next(maxValue);
        }

        public static double NextDouble()
        {
            return random.Value.NextDouble();
        }

    }
}