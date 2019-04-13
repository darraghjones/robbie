using System;
using System.Linq;
using Random = Lib.Domain.Random;

namespace Lib.Domain
{
    public static class MoveHelpers
    {
        private static readonly Move[] Values = Enum.GetValues(typeof(Move)).Cast<Move>().ToArray();
        
        public static Move GetRandomMove()
        {
            return Values[Random.Next(Values.Length)];
        }

        public static Move GetRandomDirection()
        {
            return new[] { Move.North, Move.East, Move.South, Move.West }[Random.Next(4)];
        }
    }
}