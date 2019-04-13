using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Domain;
using Random = Lib.Domain.Random;

namespace Lib.Evolution
{
    public class Darwin
    {
        private readonly Func<Dna, double> fitnessFunc;
        private List<Individual> population;
        public Darwin(Func<Dna, double> fitnessFunc, int populationSize = 200)
        {
            this.fitnessFunc = fitnessFunc;
            population = new List<Individual>(populationSize);
            population.AddRange(populationSize.Times(() =>
            {
                var cleaner = new DnaCleaner();
                return new Individual(cleaner.Dna, fitnessFunc(cleaner.Dna));
            }));
        }

        public Dna Evolve(int generations = 1000)
        {
            var best = default(Individual);
            generations.Times(i =>
            {
                population.Sort();
                population = EvolveOnce();
                best = population.Max();
                Console.Write(string.Format("{0}:\t{1}\n{2}\n\n", i, best.Fitness, best.Dna));
            });
            return best.Dna;
        }

        private List<Individual> EvolveOnce()
        {
            var newPopulation = new ConcurrentBag<Individual>();
            Parallel.For(0, population.Count / 2, _ =>
            //(population.Count / 2).Times(_ => 
            {
                var father = GetWeightedChoice();
                var mother = GetWeightedChoice();

                var split = Random.Next(Dna.Length);

                var brother = new Dna(father.Take(split).Concat(mother.Skip(split))
                .Select(x => Random.NextDouble() < 0.001 ? MoveHelpers.GetRandomMove() : x).ToArray());

                var sister = new Dna(mother.Take(split).Concat(father.Skip(split))
                .Select(x => Random.NextDouble() < 0.001 ? MoveHelpers.GetRandomMove() : x).ToArray());

                newPopulation.Add(new Individual(brother, fitnessFunc(brother)));
                newPopulation.Add(new Individual(sister, fitnessFunc(sister)));
            });
            return newPopulation.ToList();
        }

        //private Dna GetWeightedChoice()
        //{
        //    var randomValue = Random.Next((int)population.Sum(x => x.Fitness));
        //    foreach (var p in population)
        //    {
        //        randomValue -= (int) p.Fitness;
        //        if (randomValue <= 0)
        //        {
        //            return p.Dna;
        //        }
        //    }
        //    return population[population.Count - 1].Dna;
        //}

        private Dna GetWeightedChoice()
        {
            var l = population.Count;
            var total = (l + 1) / 2.0 * l;
            var position = Math.Ceiling(Random.NextDouble() * total);
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
