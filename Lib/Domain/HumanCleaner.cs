using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Domain
{
    public class HumanCleaner : ICleaner
    {
        public Move Move(Neighbourhood neighbourhood)
        {
            if (neighbourhood.Current == Cell.Can) return Domain.Move.Pickup;
            if (neighbourhood.North == Cell.Can) return Domain.Move.North;
            if (neighbourhood.South == Cell.Can) return Domain.Move.South;
            if (neighbourhood.East == Cell.Can) return Domain.Move.East;
            if (neighbourhood.West == Cell.Can) return Domain.Move.West;

            if (neighbourhood.North == Cell.Wall) return Domain.Move.South;
            if (neighbourhood.South == Cell.Wall) return Domain.Move.North;
            if (neighbourhood.East == Cell.Wall) return Domain.Move.West;
            if (neighbourhood.West == Cell.Wall) return Domain.Move.East;

            return Domain.Move.Random;
        }

        #region Dna

        public static readonly Dna Dna;

        static HumanCleaner()
        {
            Dna = new Dna(GetDna().ToArray());
        }



        private static IEnumerable<Move> GetDna()
        {
            var humanCleaner = new HumanCleaner();
            var values = new[] { Cell.Empty, Cell.Can, Cell.Wall };
            foreach (var north in values)
            foreach (var south in values)
            foreach (var east in values)
            foreach (var west in values)
            foreach (var current in values)
                yield return humanCleaner.Move(new Neighbourhood(north, south, east, west, current));
        }

        #endregion
    }
}