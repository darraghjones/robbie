using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Domain
{
    public class Dna : IEnumerable<Move>
    {
        private readonly Move[] moves;

        public const int Length = 243;

        public Dna(Move[] moves)
        {
            if (moves.Length != Length)
            {
                throw new ArgumentOutOfRangeException(nameof(moves), moves, "Moves must be 253 length");
            }
            this.moves = moves;
        }

        public Move this [int index] => moves[index];

        public IEnumerator<Move> GetEnumerator()
        {
            return moves.OfType<Move>().GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("", moves.Select(x => (char)x));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}