using System.Linq;

namespace Lib.Domain
{
    public class DnaCleaner : ICleaner
    {
        public  Dna Dna { get; }

        public DnaCleaner(Dna dna)
        {
            this.Dna = dna;
        }

        public DnaCleaner()
        {
            Dna = new Dna(Enumerable.Range(0, Dna.Length).Select(_ => MoveHelpers.GetRandomMove()).ToArray());
        }

        public Move Move(Neighbourhood neighbourhood)
        {
            var index = (int)neighbourhood.North * 81 + //(int)Math.Pow(3, 4) +
                        (int)neighbourhood.South * 27 + //(int)Math.Pow(3, 3) +
                        (int)neighbourhood.East * 9 + //(int)Math.Pow(3, 2) +
                        (int)neighbourhood.West * 3 + //(int)Math.Pow(3, 1) +
                        (int)neighbourhood.Current;
            return Dna[index];
        }
    }
}