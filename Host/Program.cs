using System;
using Lib.Domain;
using Lib.Evolution;
using Lib.Simulation;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var score = 0d;
            //while(score < 490)
            {
                var d = new Darwin(x => new Simulator(new DnaCleaner(x)).AverageScore(100), 200);
                var dna = d.Evolve(10000);
                var c = new DnaCleaner(dna);
                var s = new Simulator(c);
                score = s.AverageScore();
                Console.WriteLine($"Score: {score}");
                //break;
            }
            
        }
    }
}