using System;

namespace Lib.Domain
{
    public static class Random
    {
        //private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());
        private static readonly System.Random random = new System.Random(0);

        public static int Next(int maxValue)
        {
            lock (random) return random.Next(maxValue);
        }

        public static double NextDouble()
        {
            lock (random) return random.NextDouble();
        }

    }
}