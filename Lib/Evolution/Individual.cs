using System.Collections.Generic;
using Lib.Domain;
using Lib.Simulation;

namespace Lib.Evolution
{
    struct Individual
    {
        private sealed class FitnessRelationalComparer : Comparer<Individual>
        {
            public override int Compare(Individual x, Individual y)
            {
                return x.Fitness.CompareTo(y.Fitness);
            }
        }

        public static Comparer<Individual> FitnessComparer { get; } = new FitnessRelationalComparer();

        public Dna Dna { get; }
        public double Fitness { get; }

        public Individual(Dna dna, double fitness)
        {
            Dna = dna;
            Fitness = fitness;
        }
    }
}
