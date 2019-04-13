using System;
using System.Collections.Generic;
using System.Runtime;
using Lib.Domain;
using Lib.Simulation;

namespace Lib.Evolution
{
    struct Individual : IComparable<Individual>
    {
        private sealed class FitnessRelationalComparer : Comparer<Individual>
        {
            public override int Compare(Individual x, Individual y)
            {
                return x.Fitness.CompareTo(y.Fitness);
            }
        }

        private static Comparer<Individual> FitnessComparer { get; } = new FitnessRelationalComparer();

        public Dna Dna { get; }
        public double Fitness { get; }

        public Individual(Dna dna, double fitness)
        {
            Dna = dna;
            Fitness = fitness;
        }

        public int CompareTo(Individual other)
        {
            return FitnessComparer.Compare(this, other);
        }
    }
}
