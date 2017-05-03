using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Domain;

namespace Lib.Evolution
{
    public class Darwin
    {
        private readonly Func<Dna, double> fitnessFunc;
        private List<Individual> population;
        public Darwin(Func<Dna, double> fitnessFunc, int populationSize = 200)
        {
            this.fitnessFunc = fitnessFunc;
            population = new List<Individual>(200);
            for (int i = 0; i < populationSize; i++)
            {
                var cleaner = new DnaCleaner();
                population.Add(new Individual(cleaner.Dna, fitnessFunc(cleaner.Dna)));
            }
        }

        public Dna Evolove(int generations = 1000)
        {
            population.Sort(Individual.FitnessComparer);
            for (int i = 0; i < generations; i++)
            {
                population = EvolveOnce();
                population.Sort(Individual.FitnessComparer);
                var best = population.Last();
                Console.Write(string.Format("{0}:\t{1}\n{2}\n\n", i, best.Fitness, best.Dna));
            }
            return population.Last().Dna;
        }

        private List<Individual> EvolveOnce()
        {
            var newPopulation = new ConcurrentBag<Individual>();
            Parallel.For(0, population.Count / 2, _ =>
            //for (var i = 0; i < population.Count / 2; i++)
            {
                var father = GetWeightedChoice();
                var mother = GetWeightedChoice();

                var split = Rand.Next(Dna.Length);
                var brother = new Dna(father.Take(split).Concat(mother.Skip(split))
                .Select(x => Rand.NextDouble() < 0.001 ? MoveHelpers.GetRandomMove() : x)
                .ToArray());
                var sister = new Dna(mother.Take(split).Concat(father.Skip(split))
                .Select(x => Rand.NextDouble() < 0.001 ? MoveHelpers.GetRandomMove() : x)
                .ToArray());
                newPopulation.Add(new Individual(brother, fitnessFunc(brother)));
                newPopulation.Add(new Individual(sister, fitnessFunc(sister)));
            });
            return newPopulation.ToList();
        }

        private Dna GetWeightedChoice()
        {
            var l = population.Count;
            var total = (l + 1) / 2.0 * l;
            var position = Math.Ceiling(Rand.NextDouble() * total);
            var i = 0;
            var so_far = l;
            while (so_far < position)
            {
                l = l - 1;
                so_far += l;
                i++;
            }
            return population[population.Count - 1 - i].Dna;
        }
    }
}
