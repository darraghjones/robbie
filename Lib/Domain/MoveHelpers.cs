using System;
using System.Linq;

namespace Lib.Domain
{
    public static class MoveHelpers
    {
        private static readonly Move[] Values = Enum.GetValues(typeof(Move)).Cast<Move>().ToArray();
        
        public static Move GetRandomMove()
        {
            return Values[Rand.Next(Values.Length)];
        }

        public static Move GetRandomDirection()
        {
            return new[] { Move.North, Move.East, Move.South, Move.West }[Rand.Next(4)];
        }
    }
}